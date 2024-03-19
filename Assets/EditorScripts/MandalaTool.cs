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
    and structure of the mandala.

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
    3. Click the "Generate Mandala" button to create the mandala structure in the scene.

    Features:
    - Dynamically generates a customizable mandala with the specified number of bar arrays.
    - Allows for detailed configuration of the mandala's size, shape, and bar dimensions.
    - Utilizes the BarArrayGenerator class to create evenly spaced bar arrays in a circular pattern.

    Dependencies:
    - BarArrayGenerator.cs: Used for generating individual bar arrays along the sinusoidal curve.

    Implementation Notes:
    - The mandala is constructed by placing bar arrays at regular intervals along a circle.
    - Each bar array is rotated to face outwards from the center of the circle, creating a radial symmetry.
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
    private float barWidth = 30f;
    private float barHeight = 1f;
    private float barDepth = 1f;

    [MenuItem("Tools/Mandala Tool")]
    public static void ShowWindow()
    {
        GetWindow<MandalaTool>("Mandala Tool");
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

        if (GUILayout.Button("Generate Mandala"))
        {
            GenerateMandala();
        }
    }

    private void GenerateMandala()
    {
        GameObject mandalaParent = new GameObject("Mandala");
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
            barArray.transform.localRotation = Quaternion.Euler(rotationAngle, 90f, 0f);

            BarArrayGenerator.GenerateBarArray(barCount, groupLength, verticalScale, barWidth, barHeight, barDepth, barArray.transform);
        }
    }

    private void GenerateMandala()
    {
        GameObject mandalaParent = new GameObject("Mandala");
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

            // Assuming you want the bars to rotate around the y-axis and tilt along the z-axis
            float rotationAngle = angle;
            barArray.transform.localRotation = Quaternion.Euler(0f, rotationAngle, 90f);

            BarArrayGenerator.GenerateBarArray(barCount, groupLength, verticalScale, barWidth, barHeight, barDepth, barArray.transform);
        }
    }

}
