/*
    ███╗░░░███╗░█████╗░███╗░░██╗██████╗░░█████╗░██╗░░░░░░█████╗░
    ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗██║░░░░░██╔══██╗
    ██╔████╔██║███████║██╔██╗██║██║░░██║███████║██║░░░░░███████║
    ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██║██║░░░░░██╔══██║
    ██║░╚═╝░██║██║░░██║██║░╚███║██████╔╝██║░░██║███████╗██║░░██║
    ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝
    triple_groove - meow btw...if you even care

    MandalaTool.cs

    This script provides a custom editor tool for generating a mandala structure in Unity,
    consisting of multiple arrays of bars arranged in a circular pattern.
    The tool allows the user to configure various parameters to control the appearance
    and structure of the mandala, as well as its position in 3D space.

    Usage:
    1. In the Unity Editor, navigate to Tools -> Mandala Tool to open the tool window.
    2. Adjust the parameters for the mandala generation, including:
    - `numArrays`: Number of bar arrays to be generated around the circle.
    - `circleWidth`: Radius of the circle on which the bar arrays are placed.
    - `barCount`: Number of bars in each array.
    - `groupLength`: Length of each bar array.
    - `verticalScale`: Vertical scaling factor for the bars in the array.
    - `barWidth`: Width of each individual bar.
    - `barHeight`: Height of each individual bar.
    - `barDepth`: Depth of each individual bar.
    - `positionX`: X-coordinate of the mandala's position.
    - `positionY`: Y-coordinate of the mandala's position.
    - `positionZ`: Z-coordinate of the mandala's position.
    3. Click the "Generate Mandala" button to create the mandala structure in the scene.

    Features:
    - Dynamically generates a customizable mandala with the specified number of bar arrays.
    - Allows for detailed configuration of the mandala's size, shape, bar dimensions, and position.
    - Utilizes the BarArrayGenerator class to create evenly spaced bar arrays in a circular pattern.

    Dependencies:
    - BarArrayGenerator.cs: Used for generating individual bar arrays along the sinusoidal curve.

    Implementation Notes:
    - The mandala is constructed by placing bar arrays at regular intervals along a circle.
    - Each bar array is rotated to face outwards from the center of the circle, creating a radial symmetry.
    - The mandala's position in 3D space can be controlled using the `positionX`, `positionY`, and `positionZ` parameters.
    - The tool provides a user-friendly interface in the Unity Editor for easy customization and generation.
 */
 
using UnityEngine;
using UnityEditor;

public class MandalaTool : EditorWindow
{
    private int numArrays = 16;
    private float circleWidth = 10f;
    private int barCount = 9;
    private float groupLength = 20f;
    private float verticalScale = 3f;
    private float barWidth = 40f;
    private float barHeight = 0.5f;
    private float barDepth = 0.5f;
    private float positionX = 0f;
    private float positionY = 1.5f;
    private float positionZ = 45f;
    private Material barMaterial;

    [MenuItem("Tools/Mandala Tool")]
    public static void ShowWindow()
    {
        GetWindow<MandalaTool>("Mandala Tool");
    }

    private void OnEnable()
    {
        // Load the default material
        barMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Mandala.mat");
    }

    private void OnGUI()
    {
        numArrays = EditorGUILayout.IntField("Number of Arrays", numArrays);
        circleWidth = EditorGUILayout.FloatField("Circle Width", circleWidth);
        barCount = EditorGUILayout.IntField("Bar Count", barCount);
        groupLength = EditorGUILayout.FloatField("Group Length", groupLength);
        verticalScale = EditorGUILayout.FloatField("Vertical Scale", verticalScale);
        barWidth = EditorGUILayout.FloatField("Bar Width", barWidth);
        barHeight = EditorGUILayout.FloatField("Bar Height", barHeight);
        barDepth = EditorGUILayout.FloatField("Bar Depth", barDepth);
        positionX = EditorGUILayout.FloatField("Position X", positionX);
        positionY = EditorGUILayout.FloatField("Position Y", positionY);
        positionZ = EditorGUILayout.FloatField("Position Z", positionZ);
        barMaterial = EditorGUILayout.ObjectField("Bar Material", barMaterial, typeof(Material), false) as Material;

        if (GUILayout.Button("Generate Mandala"))
        {
            GenerateMandala();
        }
    }

    private void GenerateMandala()
    {
        GameObject mandalaParent = new GameObject("Mandala");
        mandalaParent.transform.position = new Vector3(positionX, positionY, positionZ);
        float angleStep = 360f / numArrays;

        for (int i = 0; i < numArrays; i++)
        {
            string barArrayName = "Bar Array " + (i + 1);
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians) * circleWidth;
            float y = Mathf.Sin(radians) * circleWidth;

            GameObject barArray = new GameObject(barArrayName);
            barArray.transform.SetParent(mandalaParent.transform);
            barArray.transform.localPosition = new Vector3(x, y, 0f);
            float rotationAngle = 90f - angle;
            BarArrayGenerator.GenerateBarArray(barCount, groupLength, verticalScale, barWidth, barHeight, barDepth, barArray.transform, rotationAngle, 90f, 0f, barMaterial);
        }
    }
}