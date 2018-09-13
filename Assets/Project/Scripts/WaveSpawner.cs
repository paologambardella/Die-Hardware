using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour 
{
    [SerializeField] float minTimePerWave = 2f;
    [SerializeField] float maxTimePerWave = 5f;
    [SerializeField] bool enableWaves = true;

    [System.Serializable]
    public class WaveGroup
    {
        public string[] enemiesToSpawn;
        public int chancesToSpawn = 1;
    }

    [SerializeField] WaveGroup[] waveGroups;

    ShuffleBag<WaveGroup> waveGroupsBag;

    float timeUntilNextWave;
    bool waveActive = false;

    List<WaveEnemy> currWave = new List<WaveEnemy>();

    void Start()
    {
        waveGroupsBag = new ShuffleBag<WaveGroup>();

        for (int i = 0; i < waveGroups.Length; ++i)
        {
            for (int j = 0; j < waveGroups[i].chancesToSpawn; ++j)
            {
                waveGroupsBag.Add(waveGroups[i]);
            }
        }

        timeUntilNextWave = Random.Range(minTimePerWave, maxTimePerWave);
    }

    void Update()
    {
        if (!enableWaves || !GameController.instance.IsGameStarted()) return;

        if (!waveActive)
        {
            timeUntilNextWave -= Time.deltaTime;

            if (timeUntilNextWave < 0f)
            {
                StartNextWave();
            }
        }
        else
        {
            bool allDead = true;

            for (int i = 0; i < currWave.Count && allDead; ++i)
            {
                if (currWave[i].IsAlive() && currWave[i].gameObject.activeInHierarchy)
                {
                    allDead = false;
                }
            }

            if (allDead)
            {
                timeUntilNextWave = Random.Range(minTimePerWave, maxTimePerWave);
                waveActive = false;
            }
        }
    }

    void StartNextWave()
    {
        waveActive = true;

        //WaveEnemy waveEnemy = ObjectPooler.instance.GetRandomItem<WaveEnemy>("Wave Enemies");
        WaveGroup waveGroup = waveGroupsBag.Next();

        for (int i = 0; i < waveGroup.enemiesToSpawn.Length; ++i)
        {
            SpawnEnemy(waveGroup.enemiesToSpawn[i]);
        }
    }

    void SpawnEnemy(string enemyName)
    {
        WaveEnemy waveEnemy = ObjectPooler.instance.GetItem<WaveEnemy>("Wave Enemies", enemyName);

        Vector3 pos = this.transform.position;
        pos.x = Random.Range(-10f, 10f);
        waveEnemy.transform.position = pos;

        currWave.Add(waveEnemy);

        waveEnemy.AttackPlayer();
    }
}
