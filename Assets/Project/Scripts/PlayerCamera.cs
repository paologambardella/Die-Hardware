using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
    [SerializeField] float targetPosXMultiplier = 0.8f;
    [SerializeField] float smoothTime = 0.4f;

    PlayerController player;
    Vector3 smoothVelocity;

    void OnEnable()
    {
        player = GameController.instance.player;
    }

    void LateUpdate()
    {
        Vector3 myPos = this.transform.position;
        Vector3 targetPos = player.transform.position;

        //targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.x *= targetPosXMultiplier;
        myPos.z = targetPos.z;
        myPos = Vector3.SmoothDamp(myPos, targetPos, ref smoothVelocity, smoothTime);

        this.transform.position = myPos;
    }
}
