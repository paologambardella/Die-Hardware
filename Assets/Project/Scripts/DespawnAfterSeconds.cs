using UnityEngine;
using System.Collections;

public class DespawnAfterSeconds : MonoBehaviour 
{
    [SerializeField] string objectPool;
    [SerializeField] float despawnAfterSeconds = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(despawnAfterSeconds);

        if (!string.IsNullOrEmpty(objectPool))
        {
            ObjectPooler.instance.ReturnItem(objectPool, this.transform);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
