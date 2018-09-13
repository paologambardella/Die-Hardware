using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour 
{
    List<Enemy> enemies = new List<Enemy>();
    List<Bullet> playerBullets = new List<Bullet>();
    List<Bullet> enemyBullets = new List<Bullet>();

    public void AddEnemy(Enemy enemy) { enemies.Add(enemy); }
    public void RemoveEnemy(Enemy enemy) { enemies.Remove(enemy); }
    public void AddPlayerBullet(Bullet bullet) { playerBullets.Add(bullet); }
    public void RemovePlayerBullet(Bullet bullet) { playerBullets.Remove(bullet); }
    public void AddEnemyBullet(Bullet bullet) { enemyBullets.Add(bullet); }
    public void RemoveEnemyBullet(Bullet bullet) { enemyBullets.Remove(bullet); }

    void Update()
    {
        HitTestEnemiesAgainstPlayerBullets();
        HitTestPlayerAgainstEnemyBullets();
        HitTestPlayerAgainstEnemies();
    }

    void HitTestEnemiesAgainstPlayerBullets()
    {
        //intersect bullets and enemies
        bool enemyDead;
        Vector3 enemyPos, enemySize, bulletPos, bulletSize;

        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i].IsAlive())
            {
                enemyDead = false;
                enemyPos = enemies[i].transform.position;
                enemySize = enemies[i].GetSize();

                for (int j = 0; j < playerBullets.Count && !enemyDead; ++j)
                {
                    if (playerBullets[j].IsAlive())
                    {
                        bulletPos = playerBullets[j].transform.position;
                        bulletSize = playerBullets[j].GetSize();

                        if (PhysicsUtil.DoBoxesIntersect(enemyPos, enemySize, bulletPos, bulletSize))
                        {
                            enemyDead = true;
                            enemies[i].Kill();
                            playerBullets[j].Kill();
                        }
                    }
                }
            }
        }
    }

    void HitTestPlayerAgainstEnemyBullets()
    {
        PlayerController player = GameController.instance.player;

        if (player.IsAlive() && !player.IsInvincible())
        {
            //intersect player with each bullet
            Vector3 playerPos = player.transform.position;
            Vector3 playerSize = player.GetSize();

            Vector3 bulletSize, bulletPos;

            for (int i = 0; i < enemyBullets.Count; ++i)
            {
                bulletPos = enemyBullets[i].transform.position;
                bulletSize = enemyBullets[i].GetSize();

                if (PhysicsUtil.DoBoxesIntersect(playerPos, playerSize, bulletPos, bulletSize))
                {
                    //kill da player
                    Debug.Log("Player killed by bullet: " + enemyBullets[i] + " " + bulletPos, enemyBullets[i]);

                    player.Kill();
                    enemyBullets[i].Kill();
                }
            }
        }
    }

    void HitTestPlayerAgainstEnemies()
    {
        PlayerController player = GameController.instance.player;

        if (player.IsAlive() && !player.IsInvincible())
        {
            //intersect player with each enemy
            Vector3 playerPos = player.transform.position;
            Vector3 playerSize = player.GetSize();

            Vector3 enemySize, enemyPos;

            for (int i = 0; i < enemies.Count; ++i)
            {
                if (enemies[i].IsAlive())
                {
                    enemyPos = enemies[i].transform.position;
                    enemySize = enemies[i].GetSize();

                    if (PhysicsUtil.DoBoxesIntersect(enemyPos, enemySize, playerPos, playerSize))
                    {
                        Debug.Log("Player killed by enemy: " + enemies[i] + " " + enemyPos, enemies[i]);
                        player.Kill();
                    }
                }
            }
        }
    }
}
