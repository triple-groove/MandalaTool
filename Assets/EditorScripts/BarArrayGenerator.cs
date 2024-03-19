/*
    ███╗░░░███╗░█████╗░███╗░░██╗██████╗░░█████╗░██╗░░░░░░█████╗░
    ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗██║░░░░░██╔══██╗
    ██╔████╔██║███████║██╔██╗██║██║░░██║███████║██║░░░░░███████║
    ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██║██║░░░░░██╔══██║
    ██║░╚═╝░██║██║░░██║██║░╚███║██████╔╝██║░░██║███████╗██║░░██║
    ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝
    triple_groove - meow btw...if you even care

    BarArrayGenerator.cs

    This script provides a static class for generating an array of bars along a sinusoidal curve in Unity.
    The bars are positioned evenly along the curve based on the specified number of bars and group length.
    The positions and rotations of the bars are calculated using numerical integration and the elliptic integral of the second kind.

    Usage:
    1. Call the `GenerateBarArray` method with the desired parameters to generate the array of bars.
    - `barCount`: The number of bars to generate.
    - `groupLength`: The length of the sinusoidal curve.
    - `verticalScale`: The vertical scale of the sinusoidal curve.
    - `barWidth`: The width of each bar.
    - `barHeight`: The height of each bar.
    - `barDepth`: The depth of each bar.
    - `mandalaTransform`: The parent transform for the generated bar array.
    - `rotX`: The rotation around the X-axis for the entire bar array.
    - `rotY`: The rotation around the Y-axis for the entire bar array.
    - `rotZ`: The rotation around the Z-axis for the entire bar array.

    Dependencies:
    - UnityEngine

    Note:
    - The bars are generated as child objects of the specified parent transform.
    - The positions and rotations of the bars are calculated to ensure even spacing along the sinusoidal curve.
    - The elliptic integral of the second kind is approximated using numerical integration.
 */

using UnityEngine;

public static class BarArrayGenerator
{
    public static void GenerateBarArray(int barCount, float groupLength, float verticalScale, float barWidth, float barHeight, float barDepth, Transform mandalaTransform, float rotX, float rotY, float rotZ)
    {
        float totalLength = CalculateCurveLength(groupLength, verticalScale);
        float distanceStep = totalLength / (barCount - 1);

        float currentArcLength = 0f;
        float accumulatedX = 0f;

        for (int i = 0; i < barCount; i++)
        {
            float targetLength = i * distanceStep;
            float x = accumulatedX;
            float y = Mathf.Sin(x * Mathf.PI * 2f / groupLength) * verticalScale;

            while (currentArcLength < targetLength)
            {
                float nextX = x + 0.001f;
                float nextY = Mathf.Sin(nextX * Mathf.PI * 2f / groupLength) * verticalScale;

                float dx = nextX - x;
                float dy = nextY - y;
                float segmentLength = Mathf.Sqrt(dx * dx + dy * dy);

                if (currentArcLength + segmentLength > targetLength)
                {
                    float ratio = (targetLength - currentArcLength) / segmentLength;
                    x += dx * ratio;
                    y += dy * ratio;
                    currentArcLength = targetLength;
                }
                else
                {
                    x = nextX;
                    y = nextY;
                    currentArcLength += segmentLength;
                }
            }

            GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bar.transform.localScale = new Vector3(barDepth, barHeight, barWidth);

            Vector3 position = new Vector3(x - groupLength * 0.5f, y, 0f);
            bar.transform.position = position;

            float angle = Mathf.Atan(CalculateSineDerivative(x / groupLength, groupLength, verticalScale)) * Mathf.Rad2Deg;
            bar.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            bar.transform.SetParent(mandalaTransform, false);
            bar.name = "Bar " + (i + 1);

            accumulatedX = x;
        }

        // Apply the specified rotation to the entire bar array
        mandalaTransform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
    }

    private static float FindNextDistance(int index, float distanceStep, float groupLength, float verticalScale, float totalLength)
    {
        float t = (index + 1) * distanceStep / totalLength;
        return t * totalLength;
    }

    private static float CalculateCurveLength(float groupLength, float verticalScale)
    {
        float m = verticalScale * Mathf.PI * 2f / groupLength;
        return 4f * verticalScale * EllipticIntegralSecondKind(m);
    }

    private static float EllipticIntegralSecondKind(float m)
    {
        int iterations = 100;
        float step = Mathf.PI / 2f / iterations;
        float sum = 0f;

        for (int i = 0; i <= iterations; i++)
        {
            float theta = i * step;
            float sinTheta = Mathf.Sin(theta);
            float sinSquared = sinTheta * sinTheta;
            float factor = Mathf.Sqrt(1 - m * sinSquared);
            float contribution = factor != 0 ? sinSquared / factor : 0;

            if (i == 0 || i == iterations)
            {
                sum += contribution / 2;
            }
            else
            {
                sum += contribution;
            }
        }

        return sum * step;
    }

    private static Vector3 CalculatePositionOnCurve(float t, float groupLength, float verticalScale)
    {
        float x = Mathf.Lerp(-groupLength * 0.5f, groupLength * 0.5f, t);
        float y = Mathf.Sin(t * Mathf.PI * 2f) * verticalScale;
        return new Vector3(x, y, 0f);
    }

    private static float CalculateSineDerivative(float t, float groupLength, float verticalScale)
    {
        return verticalScale * (Mathf.PI * 2f) / groupLength * Mathf.Cos(t * Mathf.PI * 2f);
    }
}