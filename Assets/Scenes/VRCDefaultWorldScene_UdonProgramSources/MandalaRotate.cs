/*
    ███╗░░░███╗░█████╗░███╗░░██╗██████╗░░█████╗░██╗░░░░░░█████╗░
    ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗██║░░░░░██╔══██╗
    ██╔████╔██║███████║██╔██╗██║██║░░██║███████║██║░░░░░███████║
    ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██║██║░░░░░██╔══██║
    ██║░╚═╝░██║██║░░██║██║░╚███║██████╔╝██║░░██║███████╗██║░░██║
    ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝
    triple_groove - meow btw...if you even care

    MandalaRotate.cs

    This script rotates each "Bar Array" within a "Mandala"

    Usage:
    1. Attach this script to empty MandalaRotate GameObject in Unity scene.
    2. Ensure that there is a "Mandala" GameObject in the scene with the desired number of "Bar Array" child objects.
    3. Adjust the following parameters in the script's public variables:
       - `rpm`: Rotation speed in revolutions per minute.
       - `rotationAxisIndex`: Index of the rotation axis (0 for X-axis, 1 for Y-axis, 2 for Z-axis).
       - `numArrays`: Number of bar arrays in the mandala.

    Features:
    - Rotates each "Bar Array" within the "Mandala" object independently.
    - Allows customization of the rotation speed and axis.
    - Provides a mesmerizing visual effect for the mandala structure.

    Dependencies:
    - Udon Sharp
    - VRChat SDK
*/

using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

public class MandalaRotate : UdonSharpBehaviour
{
    public float rpm = 5f; // User-specified rotation speed in RPM (default: 5)
    public int rotationAxisIndex = 1; // User-specified axis of rotation (default: 1 for Y-axis)
    public int numArrays = 4; // Number of bar arrays in the mandala

    private GameObject[] barArrays;
    private float rotationSpeed;

    private void Start()
    {
        // Find the "Mandala" GameObject
        GameObject mandala = GameObject.Find("Mandala");

        if (mandala != null)
        {
            // Find all the "Bar Array" GameObjects within the "Mandala" GameObject
            barArrays = new GameObject[numArrays];
            for (int i = 0; i < numArrays; i++)
            {
                string barArrayName = "Bar Array " + (i + 1);
                Transform barArrayTransform = mandala.transform.Find(barArrayName);
                if (barArrayTransform != null)
                {
                    barArrays[i] = barArrayTransform.gameObject;
                }
            }
        }

        // Convert RPM to degrees per second
        rotationSpeed = rpm * 360f / 60f;
    }

    private void Update()
    {
        Vector3 axis = Vector3.zero;

        // Set the rotation axis based on the user-specified index
        switch (rotationAxisIndex)
        {
            case 0:
                axis = Vector3.right; // X-axis
                break;
            case 1:
                axis = Vector3.up; // Y-axis
                break;
            case 2:
                axis = Vector3.forward; // Z-axis
                break;
            default:
                axis = Vector3.up; // Default to Y-axis if an invalid index is provided
                break;
        }

        // Rotate each "Bar Array" GameObject around the specified axis based on the rotation speed
        foreach (GameObject barArray in barArrays)
        {
            if (barArray != null)
            {
                barArray.transform.Rotate(axis, rotationSpeed * Time.deltaTime);
            }
        }
    }
}