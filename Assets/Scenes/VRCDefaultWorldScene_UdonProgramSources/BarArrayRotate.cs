/*
    ███╗░░░███╗░█████╗░███╗░░██╗██████╗░░█████╗░██╗░░░░░░█████╗░
    ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗██║░░░░░██╔══██╗
    ██╔████╔██║███████║██╔██╗██║██║░░██║███████║██║░░░░░███████║
    ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██║██║░░░░░██╔══██║
    ██║░╚═╝░██║██║░░██║██║░╚███║██████╔╝██║░░██║███████╗██║░░██║
    ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝
    triple_groove - meow btw...if you even care

    BarArrayRotate.cs

    This script rotates a "BarArray" GameObject in Unity around a specified axis at a given speed.
    It provides a simple way to add rotation to a bar array object and customize its behavior.

    Usage:
    1. Attach this script to empty BarArrayRotate GameObject in your Unity scene.
    2. Ensure that there is a GameObject named "BarArray" in the scene that you want to rotate.
    3. Adjust the following parameters in the script's public variables:
       - `rpm`: Rotation speed in revolutions per minute.
       - `rotationAxisIndex`: Index of the rotation axis (0 for X-axis, 1 for Y-axis, 2 for Z-axis).

    Features:
    - Rotates the "BarArray" GameObject around the specified axis.
    - Allows customization of the rotation speed and axis.
    - Provides a simple way to add dynamic rotation to a bar array object.

    Dependencies:
    - Udon Sharp
    - VRChat SDK
*/

using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

public class BarArrayRotate : UdonSharpBehaviour
{
    public float rpm = 5f; // User-specified rotation speed in RPM (default: 5)
    public int rotationAxisIndex = 1; // User-specified axis of rotation (default: 1 for Y-axis)

    private GameObject barArray;
    private float rotationSpeed;

    private void Start()
    {
        // Find the GameObject named "BarArray"
        barArray = GameObject.Find("BarArray");

        // Convert RPM to degrees per second
        rotationSpeed = rpm * 360f / 60f;
    }

    private void Update()
    {
        if (barArray != null)
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

            // Rotate the BarArray GameObject around the specified axis based on the rotation speed
            barArray.transform.Rotate(axis, rotationSpeed * Time.deltaTime);
        }
    }
}