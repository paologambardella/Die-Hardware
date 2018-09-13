using UnityEngine;
using System.Collections;

public class KillDistance : MonoBehaviour 
{
    [SerializeField] string poolName;

    public bool EvaluateDistance(float minX, float maxX, float minZ, float maxZ)
    {
        Vector3 pos = this.transform.position;

        if (pos.x < minX || pos.x > maxX || pos.z < minZ || pos.z > maxZ)
        {
            ObjectPooler.instance.ReturnItem(poolName, this.transform);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnEnable()
    {
        KillDistanceManager.Add(this);
    }

    void OnDisable()
    {
        KillDistanceManager.Remove(this);
    }
}
