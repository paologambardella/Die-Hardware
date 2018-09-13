using UnityEngine;
using System.Collections;

public class SkullMonster : MonoBehaviour 
{
//    [SerializeField] Vector3 fromRot = new Vector3(0f, 0f, 0f);
//    [SerializeField] Vector3 toRot = new Vector3(0f, 0f, 0f);
//    [SerializeField] Vector3 speed = new Vector3(1f, 1f, 1f);
//
//    float timer = 0f;
//    Vector3 rot;
//
//    void OnEnable()
//    {
//        timer = 0f;
//    }
//
//    void Update()
//    {
//        timer += Time.deltaTime;
//
//        rot.x = Tween(fromRot.x, toRot.x, speed.x);
//        rot.y = Tween(fromRot.y, toRot.y, speed.y);
//        rot.z = Tween(fromRot.z, toRot.z, speed.z);
//
//        this.transform.localRotation = Quaternion.Euler(rot);
//    }
//
//    float Tween(float a, float b, float duration)
//    {
//        return Mathf.Lerp(a, b, Easing.EaseInOut(PasrEasing.GetValue(timer, 0f, duration / 2f, 0f, duration / 2f), EasingType.Cubic));
//    }

    void Update()
    {
        this.transform.LookAt(GameController.instance.player.transform, Vector3.up);
        this.transform.forward = -this.transform.forward; //look backwards!
    }
}
