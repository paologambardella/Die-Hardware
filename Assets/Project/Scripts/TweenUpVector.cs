using UnityEngine;
using System.Collections;

public class TweenUpVector : MonoBehaviour 
{
    [SerializeField] Vector3 a = Vector3.up;
    [SerializeField] Vector3 b = Vector3.up;
    [SerializeField] float pauseTime = 1f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] float holdTime = 1f;
    [SerializeField] float releaseTime = 1f;
    [SerializeField] EasingType easing = EasingType.Cubic;

    float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float pasr = PasrEasing.GetValue(timer, pauseTime, attackTime, holdTime, releaseTime);
        float eased = Easing.EaseInOut(pasr, easing);

        this.transform.up = Vector3.Lerp(a, b, eased);
    }
}
