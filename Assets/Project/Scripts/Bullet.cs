using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField] bool playerBullet = true;
    [SerializeField] float bulletSpeed;
    [SerializeField] Vector3 size;
    [SerializeField] string spawnPool = "Bullets";

    [SerializeField] float lifetimeSeconds = 1000f;
    [SerializeField] float lifetimeDistance = 1000f; //distance ahead of player

    bool isAlive = false;

    float timeAlive;

    public void Fire()
    {
        
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Kill()
    {
        isAlive = false;
    }

    public Vector3 GetSize()
    {
        return size;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, (isAlive) ? 1f : 0.5f);
        Gizmos.DrawWireCube(this.transform.position, size);
    }

    void OnEnable()
    {
        timeAlive = 0f;

        isAlive = true;

        if (playerBullet)
        {
            GameController.instance.worldManager.AddPlayerBullet(this);
        }
        else
        {
            GameController.instance.worldManager.AddEnemyBullet(this);
        }
    }

    void OnDisable()
    {
        if (playerBullet)
        {
            GameController.instance.worldManager.RemovePlayerBullet(this);
        }
        else
        {
            GameController.instance.worldManager.RemoveEnemyBullet(this);
        }
    }

    void Update()
    {
        if (isAlive)
        {
            float distanceAheadOfPlayer = this.transform.position.z - GameController.instance.player.transform.position.z;   
            float moveDistance = bulletSpeed * Time.deltaTime;

            timeAlive += Time.deltaTime;

            this.transform.position = this.transform.position + this.transform.forward * moveDistance;

            if (timeAlive > lifetimeSeconds
                || distanceAheadOfPlayer > lifetimeDistance)
            {
                isAlive = false;
                ObjectPooler.instance.ReturnItem(spawnPool, this.transform);
            }
        }
        else
        {
            ObjectPooler.instance.ReturnItem(spawnPool, this.transform);
        }
    }
}
