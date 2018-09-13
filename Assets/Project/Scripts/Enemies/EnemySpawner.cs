using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField] float chanceToSpawnEnemy = 0.05f;
    [SerializeField] string objectPool;

    void Update()
    {
        if (GameController.instance.IsGameStarted()
            && Random.value < chanceToSpawnEnemy)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Transform enemy = ObjectPooler.instance.GetRandomItem(objectPool);
        Vector3 pos = this.transform.position;
        pos.x = Random.Range(-12f, 12f);
        enemy.transform.position = pos;
        enemy.transform.forward = -Vector3.forward;
    }
}
