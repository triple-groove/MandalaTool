/*
    ███╗░░░███╗░█████╗░███╗░░██╗██████╗░░█████╗░██╗░░░░░░█████╗░
    ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗██║░░░░░██╔══██╗
    ██╔████╔██║███████║██╔██╗██║██║░░██║███████║██║░░░░░███████║
    ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██║██║░░░░░██╔══██║
    ██║░╚═╝░██║██║░░██║██║░╚███║██████╔╝██║░░██║███████╗██║░░██║
    ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝
    triple_groove - meow btw...if you even care

    BarArrayTool.cs
*/

using UnityEngine;
using UnityEditor;

public class BarArrayTool : EditorWindow
{
    private int barCount = 9; // Default bar count
    private float groupLength = 20f; // Default group length
    private float verticalScale = 3f; // Default vertical scale
    private float barWidth = 10f; // Default bar width
    private float barHeight = 1f; // Default bar height
    private float barDepth = 1f; // Default bar depth
    private float rotX = 0f; // Default x rotation
    private float rotY = 0f; // Default y rotation
    private float rotZ = 0f; // Default z rotation

    [MenuItem("Tools/Bar Array Tool")]
    public static void ShowWindow()
    {
        GetWindow<BarArrayTool>("Bar Array Tool");
    }

    private void OnGUI()
    {
        barCount = EditorGUILayout.IntField("Bar Count", barCount);
        groupLength = EditorGUILayout.FloatField("Group Length", groupLength);
        verticalScale = EditorGUILayout.FloatField("Vertical Scale", verticalScale);
        barWidth = EditorGUILayout.FloatField("Bar Width", barWidth);
        barHeight = EditorGUILayout.FloatField("Bar Height", barHeight);
        barDepth = EditorGUILayout.FloatField("Bar Depth", barDepth);
        rotX = EditorGUILayout.FloatField("Rotation X", rotX);
        rotY = EditorGUILayout.FloatField("Rotation Y", rotY);
        rotZ = EditorGUILayout.FloatField("Rotation Z", rotZ);

        if (GUILayout.Button("Create Bar Array"))
        {
            CreateBarArray();
        }
    }

    private void CreateBarArray()
    {
        GameObject barArrayObject = new GameObject("BarArray");

        // Use the BarArrayGenerator to create bars under this new BarArray object
        BarArrayGenerator.GenerateBarArray(barCount, groupLength, verticalScale, barWidth, barHeight, barDepth, barArrayObject.transform, rotX, rotY, rotZ);
    }
}
