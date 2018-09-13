using UnityEngine;
using System.Collections;

public class ChargingEnemy : MonoBehaviour 
{
    [SerializeField] float speed = 10f;

    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * speed;
    }
}
