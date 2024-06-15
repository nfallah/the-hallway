using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        HallwayGenerator.Combine(new Mesh[] { Generator.Generate(Vector3.right * 4, Vector3.forward, Vector2.one * 4, new Vector4(4, 0, 0, 0)) });
        HallwayGenerator.GenerateHallway(Vector3.zero, Vector3.forward, Vector2.one * 4);
        HallwayGenerator.GenerateSideDoors(Vector3.forward * 4, Vector3.forward, Vector2.one * 4);
        HallwayGenerator.GenerateRightTurn(Vector3.forward * 8, Vector3.forward, Vector2.one * 4);
    }
}