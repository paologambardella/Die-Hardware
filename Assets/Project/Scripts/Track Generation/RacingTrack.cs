using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RacingTrack : MonoBehaviour 
{
    [SerializeField] LineRenderer simple;
    [SerializeField] LineRenderer path;

    List<Vector3> controlPoints = new List<Vector3>();
    List<Vector3> pathPoints = new List<Vector3>();

    void Start()
    {
        Vector3 p = Vector3.zero;

        controlPoints.Add(p);

        for (int i = 0; i < 100; ++i)
        {
            p = p + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(5f, 30f));
            controlPoints.Add(p);
        }


    }

    Vector3 CatmullRom(float t, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        return (
            (b * 2.0f) +
            (-a + c) * t +
            (a * 2.0f - b * 5.0f + c * 4.0f - d) * t * t +
            (-a + b * 3.0f - c * 3.0f + d) * t * t * t
        ) * 0.5f;
    }
}
