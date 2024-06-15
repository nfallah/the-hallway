using System.Collections.Generic;
using UnityEngine;

public class Generator
{
    public static Mesh Generate(Vector3 position, Vector3 direction, Vector2 dimensions, Vector4 sizes)
    {
        Vector3 localRight;
        Vector3 localUp;

        if (direction == Vector3.left)
        {
            localRight = Vector3.back;
            localUp = Vector3.up;
        }
        else if (direction == Vector3.right)
        {
            localRight = Vector3.forward;
            localUp = Vector3.up;
        }
        else if (direction == Vector3.down)
        {
            localRight = Vector3.right;
            localUp = Vector3.back;
        }
        else if (direction == Vector3.up)
        {
            localRight = Vector3.right;
            localUp = Vector3.forward;
        }
        else if (direction == Vector3.back)
        {
            localRight = Vector3.right;
            localUp = Vector3.up;
        }
        else if (direction == Vector3.forward)
        {
            localRight = Vector3.left;
            localUp = Vector3.up;
        }
        else
        {
            return null;
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        int currentVertex = 0;

        if (sizes.x > 0)
        {
            vertices.Add(position);
            vertices.Add(position + sizes.x * localUp);
            vertices.Add(position + dimensions.x * localRight + sizes.x * localUp);
            vertices.Add(position + dimensions.x * localRight);
            triangles.Add(currentVertex);
            triangles.Add(currentVertex + 1);
            triangles.Add(currentVertex + 2);
            triangles.Add(currentVertex + 2);
            triangles.Add(currentVertex + 3);
            triangles.Add(currentVertex);
            currentVertex += 4;
        }

        if (sizes.y > 0)
        {
            vertices.Add(position + dimensions.y * localUp);
            vertices.Add(position + sizes.y * localRight + dimensions.y * localUp);

            if (sizes.x <= 0)
            {
                vertices.Add(position + sizes.y * localRight);
                vertices.Add(position);
                triangles.Add(currentVertex);
                triangles.Add(currentVertex + 1);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 3);
                triangles.Add(currentVertex);
                currentVertex += 4;
            }
            else
            {
                vertices.Add(position + sizes.y * localRight + sizes.x * localUp);
                triangles.Add(currentVertex);
                triangles.Add(currentVertex + 1);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex - 3);
                triangles.Add(currentVertex);
                currentVertex += 3;
            }
        }

        if (sizes.z > 0)
        {
            vertices.Add(position + dimensions.x * localRight + dimensions.y * localUp);
            vertices.Add(position + dimensions.x * localRight + (dimensions.y - sizes.z) * localUp);

            if (sizes.y <= 0)
            {
                vertices.Add(position + (dimensions.y - sizes.z) * localUp);
                vertices.Add(position + dimensions.y * localUp);
                triangles.Add(currentVertex);
                triangles.Add(currentVertex + 1);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 3);
                triangles.Add(currentVertex);
                currentVertex += 4;
            }
            else
            {
                vertices.Add(position + sizes.y * localRight + (dimensions.y - sizes.z) * localUp);
                triangles.Add(currentVertex);
                triangles.Add(currentVertex + 1);
                triangles.Add(currentVertex + 2);
                triangles.Add(currentVertex + 2);
                
                if (sizes.x <= 0)
                {
                    triangles.Add(currentVertex - 3);
                }
                else
                {
                    triangles.Add(currentVertex - 2);
                }

                triangles.Add(currentVertex);
                currentVertex += 3;
            }
        }

        if (sizes.w > 0)
        {
            int v0, v1, v2, v3;

            if (sizes.x <= 0)
            {
                vertices.Add(position + dimensions.x * localRight);
                vertices.Add(position + (dimensions.x - sizes.w) * localRight);
                v0 = currentVertex;
                v1 = currentVertex + 1;
            }
            else
            {
                vertices.Add(position + (dimensions.x - sizes.w) * localRight + sizes.x * localUp);
                v0 = 2;
                v1 = currentVertex;
            }

            if (sizes.z <= 0)
            {
                vertices.Add(position + (dimensions.x - sizes.w) * localRight + dimensions.y * localUp);
                vertices.Add(position + dimensions.x * localRight + dimensions.y * localUp);

                if (sizes.x <= 0)
                {
                    v2 = currentVertex + 2;
                    v3 = currentVertex + 3;
                }
                else
                {
                    v2 = currentVertex + 1;
                    v3 = currentVertex + 2;
                }
            }
            else
            {
                vertices.Add(position + (dimensions.x - sizes.w) * localRight + (dimensions.y - sizes.z) * localUp);
                
                if (sizes.x <= 0)
                {
                    v2 = currentVertex + 2;
                }
                else
                {
                    v2 = currentVertex + 1;
                }
                
                if (sizes.y <= 0)
                {
                    v3 = currentVertex - 3;
                }
                else
                {
                    v3 = currentVertex - 2;
                }
            }

            triangles.Add(v0);
            triangles.Add(v1);
            triangles.Add(v2);
            triangles.Add(v2);
            triangles.Add(v3);
            triangles.Add(v0);
        }

        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < vertices.Count; i++)
        {
            normals.Add(direction);
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
        return mesh;
    }
}