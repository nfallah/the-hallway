using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGenerator
{
    public static GameObject GenerateHallway(Vector3 position, Vector3 direction, Vector2 dimensions)
    {
        Mesh floor = Generator.Generate(position, Vector3.up, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh ceiling = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.up, Vector3.down, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh leftWall = Generator.Generate(position, Vector3.right, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh rightWall = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.right, Vector3.left, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        return Combine(new Mesh[] { floor, ceiling, leftWall, rightWall });
    }

    public static GameObject GenerateRightTurn(Vector3 position, Vector3 direction, Vector2 dimensions)
    {
        Mesh floor = Generator.Generate(position, Vector3.up, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh ceiling = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.up, Vector3.down, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh leftWall = Generator.Generate(position, Vector3.right, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh frontWall = Generator.Generate(position + dimensions.x * Vector3.forward, Vector3.back, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        return Combine(new Mesh[] { floor, ceiling, leftWall, frontWall });
    }

    public static GameObject GenerateLeftTurn(Vector3 position, Vector3 direction, Vector2 dimensions)
    {
        Mesh floor = Generator.Generate(position, Vector3.up, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh ceiling = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.up, Vector3.down, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh rightWall = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.right, Vector3.left, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh frontWall = Generator.Generate(position + dimensions.x * Vector3.forward, Vector3.back, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        return Combine(new Mesh[] { floor, ceiling, rightWall, frontWall });
    }

    public static GameObject GenerateSideDoors(Vector3 position, Vector3 direction, Vector2 dimensions)
    {
        Mesh floor = Generator.Generate(position, Vector3.up, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh ceiling = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.up, Vector3.down, dimensions, new Vector4(dimensions.x, 0, 0, 0));
        Mesh leftWall = Generator.Generate(position, Vector3.right, dimensions, new Vector4(0, 1.125f, 1.5f, 1.125f));
        Mesh rightWall = Generator.Generate(position + dimensions.x * Vector3.forward + dimensions.y * Vector3.right, Vector3.left, dimensions, new Vector4(0, 1.125f, 1.5f, 1.125f));
        return Combine(new Mesh[] { floor, ceiling, leftWall, rightWall });
    }

    public static GameObject Combine(Mesh[] meshes)
    {
        CombineInstance[] combine = new CombineInstance[meshes.Length];

        for (int i = 0; i < combine.Length; i++)
        {
            combine[i].mesh = meshes[i];
            combine[i].transform = Matrix4x4.identity;
        }

        Mesh m = new Mesh();
        m.CombineMeshes(combine);
        GameObject obj = new GameObject("Mesh", typeof(MeshRenderer));
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshCollider mc = obj.AddComponent<MeshCollider>();
        mf.mesh = m;
        mc.sharedMesh = m;
        return obj;
    }
}