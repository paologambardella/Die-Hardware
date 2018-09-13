using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class ObjectPooler : MonoBehaviour 
{
    public static ObjectPooler instance;

    [System.Serializable]
    public class PooledItem
    {
        public string name;
        public GameObject prefab;
    }

    [SerializeField] PooledItem[] items;

    Dictionary<string, PooledItem> lookup = new Dictionary<string, PooledItem>();

    public Transform GetItem(string poolName, string prefabName)
    {
        return PoolManager.Pools[poolName].Spawn(prefabName);
    }

    public Transform GetRandomItem(string poolName)
    {
        return PoolManager.Pools[poolName].SpawnRandom();
    }

    public bool IsItemPooled(string poolName, Transform obj)
    {
        return PoolManager.Pools[poolName].IsSpawned(obj);
    }

    public T GetRandomItem<T>(string poolName) where T : MonoBehaviour
    {
        Transform obj = GetRandomItem(poolName);
        return obj.GetComponent<T>();
    }

    public T GetItem<T>(string poolName, string prefabName) where T : MonoBehaviour
    {
        Transform inst = GetItem(poolName, prefabName);

        if (instance != null)
        {
            T monobehavior = inst.GetComponent<T>();

            if (monobehavior != null)
            {
                return monobehavior;
            }
            else
            {
                Debug.LogError("Pooled object [" + name + "] does not contain component " + typeof(T));
                GameObject.Destroy(instance);
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void ReturnItem(string poolName, Transform item)
    {
        PoolManager.Pools[poolName].Despawn(item);
    }

    void Awake()
    {
        instance = this;

        for (int i = 0; i < items.Length; ++i)
        {
            lookup.Add(items[i].name, items[i]);
        }
    }
}
