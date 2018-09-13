#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessRoad : MonoBehaviour 
{
    [SerializeField] Transform editorRoadChunk; //for display in editor only
    [SerializeField] float spawnAheadDistance = 600f;
    [SerializeField] float killBehindDistance = 300f;

    List<RoadChunk> chunks = new List<RoadChunk>();

    float nextChunkZ = -100f;

    void Start()
    {
        if (editorRoadChunk != null)
        {
            editorRoadChunk.gameObject.SetActive(false);
        }
        SpawnMoreChunks(spawnAheadDistance);
    }

    void LateUpdate()
    {
        Vector3 playerPos = GameController.instance.player.transform.position;

        float killDistance = playerPos.z - killBehindDistance;
        float spawnToDistance = playerPos.z + spawnAheadDistance;

        RemoveDeadChunks(killDistance);
        SpawnMoreChunks(spawnToDistance);
    }

    void SpawnMoreChunks(float spawnToDistance)
    {
        while (nextChunkZ < spawnToDistance)
        {
            SpawnChunk();
        }

    }

    void SpawnChunk()
    {
        RoadChunk chunk = ObjectPooler.instance.GetRandomItem<RoadChunk>("Road Chunks");
        chunk.transform.position = new Vector3(0, 0f, nextChunkZ);
        nextChunkZ += chunk.GetSize().z;

        chunks.Add(chunk);
    }

    void RemoveDeadChunks(float killDistance)
    {
        int removeCount = 0;

        for (int i = 0; i < chunks.Count; ++i)
        {
            if (chunks[i].transform.position.z < killDistance)
            {
                ObjectPooler.instance.ReturnItem("Road Chunks", chunks[i].transform);
                ++removeCount;
            }
            else
            {
                break;
            }
        }

        chunks.RemoveRange(0, removeCount);
    }
}
