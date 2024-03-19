using UnityEngine;
using UnityEditor;

public class CubeCircleGenerator : EditorWindow
{
    private int numCubes = 10;
    private float circleWidth = 5f;

    [MenuItem("Tools/Cube Circle Generator")]
    public static void ShowWindow()
    {
        GetWindow<CubeCircleGenerator>("Cube Circle Generator");
    }

    private void OnGUI()
    {
        // Draw the number of cubes field
        numCubes = EditorGUILayout.IntField("Number of Cubes", numCubes);

        // Draw the circle width field
        circleWidth = EditorGUILayout.FloatField("Circle Width", circleWidth);

        // Create button
        if (GUILayout.Button("Generate Cube Circle"))
        {
            GenerateCubeCircle();
        }
    }

    private void GenerateCubeCircle()
    {
        // Create a new empty GameObject to hold the cubes
        GameObject cubeCircleParent = new GameObject("Cube Circle");

        // Calculate the angle step based on the number of cubes
        float angleStep = 360f / numCubes;

        // Generate cubes in a circle
        for (int i = 0; i < numCubes; i++)
        {
            // Calculate the angle for the current cube
            float angle = i * angleStep;

            // Convert the angle from degrees to radians
            float radians = angle * Mathf.Deg2Rad;

            // Calculate the position of the cube on the circle
            float x = Mathf.Cos(radians) * circleWidth;
            float y = Mathf.Sin(radians) * circleWidth;

            // Create a new cube GameObject
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Set the cube's position
            cube.transform.position = new Vector3(x, y, 0f);

            // Calculate the rotation angle for the cube
            float rotationAngle = angle - 90f; // Subtract 90 degrees to align with the circle line

            // Set the cube's rotation
            cube.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            // Set the cube's parent to the cube circle parent
            cube.transform.SetParent(cubeCircleParent.transform);

            // Rename the cube based on its index
            cube.name = "Cube " + (i + 1);
        }
    }
}