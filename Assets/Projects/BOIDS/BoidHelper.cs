using UnityEngine;
using System.Collections;

public class BoidHelper : MonoBehaviour
{

    // /*
    //  Create a sphere of points around the sphere at start time,
    //  then just cast rays from an origin, to one of these points.

    // This is done to reduce memory usage and also to spare proccesing time
    // so this isn't calculated at run-time everytime a new bird is created,
    // and the birds can just call this class to get the points.
    //  */

    // [SerializeField]
    // public int pointQuantity;

    // [HideInInspector]
    // public Vector3[] points;



    // public void GenerateSpherePoints(int numPoints, float radius = 1f, Vector3 center = default)
    // {
    //     Vector3[] points = new Vector3[numPoints];

    //     const float goldenAngle = Mathf.PI * (3f - 2.2360679f); // π(3 - √5)

    //     for (int i = 0; i < numPoints; i++)
    //     {
    //         float y = 1f - (i / (float)(numPoints - 1)) * 2f;

    //         float ringRadius = Mathf.Sqrt(1f - y * y);

    //         float theta = goldenAngle * i;

    //         float x = Mathf.Cos(theta) * ringRadius;
    //         float z = Mathf.Sin(theta) * ringRadius;

    //         points[i] = center + new Vector3(x, y, z) * radius;
    //     }
    // }
}