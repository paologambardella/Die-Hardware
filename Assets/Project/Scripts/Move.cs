using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{
    [SerializeField] Vector3 speed;

    void Update()
    {
        this.transform.position = this.transform.position + speed * Time.deltaTime;
    }
}
