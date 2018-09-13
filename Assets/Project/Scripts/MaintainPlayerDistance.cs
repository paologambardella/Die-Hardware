using UnityEngine;
using System.Collections;

public class MaintainPlayerDistance : MonoBehaviour 
{
    float distanceFromPlayer;

    void OnEnable()
    {
        StartCoroutine(GetDistance());
    }

    IEnumerator GetDistance()
    {
        yield return null;
        distanceFromPlayer = GameController.instance.player.transform.position.z - this.transform.position.z;
    }

    void Update()
    {
        //this.transform.position = GameController.instance.player.transform.position
        Vector3 pos = this.transform.position;
        pos.z = GameController.instance.player.transform.position.z + distanceFromPlayer;
        this.transform.position = pos;
    }
}
