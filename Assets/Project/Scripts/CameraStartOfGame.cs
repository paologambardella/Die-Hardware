using UnityEngine;
using System.Collections;

public class CameraStartOfGame : MonoBehaviour 
{
    void Update()
    {
        if (GameController.instance.IsGameStarted())
        {
            Vector3 pos = this.transform.localPosition;
            Vector3 rot = this.transform.localRotation.eulerAngles;

            rot = Vector3.MoveTowards(rot, Vector3.zero, Time.deltaTime * 200f);
            pos = Vector3.MoveTowards(pos, Vector3.zero, Time.deltaTime * 5f);

            this.transform.localPosition=  pos;
            this.transform.localRotation = Quaternion.Euler(rot);
        }
    }
}
