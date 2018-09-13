using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillDistanceManager : MonoBehaviour 
{
    public static KillDistanceManager instance;

    public List<KillDistance> checks = new List<KillDistance>();

    [SerializeField] float leftLimit = -35f;
    [SerializeField] float rightLimit = 35f;
    [SerializeField] float behindLimit = -10f;
    [SerializeField] float aheadLimit = 120f;

    int index = -1;

    public static void Add(KillDistance check)
    {
        instance.checks.Add(check);
    }

    public static void Remove(KillDistance check)
    {
        instance.checks.Remove(check);
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        int numChecks = Mathf.Min(200, checks.Count);

        Vector3 playerPos = GameController.instance.player.transform.position;

        float minX = playerPos.x + leftLimit;
        float maxX = playerPos.x + rightLimit;
        float minZ = playerPos.z + behindLimit;
        float maxZ = playerPos.z + aheadLimit;

        for (int i = 0; i < numChecks; ++i)
        {
            index += 1;
            index = (index >= checks.Count) ? 0 : index;

            checks[index].EvaluateDistance(minX, maxX, minZ, maxZ);
        }
    }
}
