using UnityEngine;
using System.Collections;

public class RotateOnEnable : MonoBehaviour 
{
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;

    void OnEnable()
    {
        this.transform.localRotation = Quaternion.Euler(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}
