using UnityEngine;
using System.Collections;

public class SnapToEdges : MonoBehaviour 
{
    [SerializeField] Renderer[] renderers;

    void OnEnable()
    {
        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].enabled = false;
        }

        StartCoroutine(Snap());
    }

    IEnumerator Snap()
    {
        yield return null;
        Vector3 pos = this.transform.position;
        pos.x = Random.Range(11f, 14f) * (Random.value > 0.5f ? 1f : -1f);
        this.transform.position = pos;

        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].enabled = true;
        }
    }
}
