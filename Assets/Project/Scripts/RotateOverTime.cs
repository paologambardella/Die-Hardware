using UnityEngine;
using System.Collections;

public class RotateOverTime : MonoBehaviour 
{
    [SerializeField] Vector3 rotspeed;

    void Update()
    {
        Vector3 rot = this.transform.localRotation.eulerAngles;
        rot += rotspeed * Time.deltaTime;
        this.transform.localRotation = Quaternion.Euler(rot);
    }
}
