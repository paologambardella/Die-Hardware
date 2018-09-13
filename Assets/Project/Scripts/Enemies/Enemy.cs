using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [SerializeField] Vector3 size = Vector3.one;
    [SerializeField] string objectPool = "Obstacles";
    [SerializeField] bool invincible = false;

    bool isAlive = false;

    public Vector3 GetSize()
    {
        return size;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    virtual public void Kill()
    {
        if (!invincible && isAlive)
        {
            isAlive = false;
            Transform explosion = ObjectPooler.instance.GetRandomItem("Explosions");
            explosion.position = this.transform.position;

            GameController.instance.EnemyKilled(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, (isAlive) ? 1f : 0.5f);
        Gizmos.DrawWireCube(this.transform.position, size);
    }

    virtual protected void Update()
    {
        if (!isAlive)
        {
            if (ObjectPooler.instance.IsItemPooled(objectPool, this.transform))
            {
                ObjectPooler.instance.ReturnItem(objectPool, this.transform);
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    virtual protected void OnEnable()
    {
        isAlive = true;
        GameController.instance.worldManager.AddEnemy(this);
    }

    virtual protected void OnDisable()
    {
        GameController.instance.worldManager.RemoveEnemy(this);
    }
}
