using UnityEngine;
using System.Collections;

public class ScaleOverTime : MonoBehaviour 
{
    [SerializeField] Vector3 a;
    [SerializeField] Vector3 b;
    [SerializeField] float duration = 1f;
    [SerializeField] EasingType easing = EasingType.Linear;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        float eased = Easing.EaseInOut(Mathf.Clamp01(timer / duration), easing);

        this.transform.localScale = Vector3.Lerp(a,  b, eased);

    }
}
