using UnityEngine;
using System.Collections;

public class ShootCommand : WaveEnemyCommand
{
    [SerializeField] int numShots = 1;
    [SerializeField] float secondsBetweenShots = 0.2f;

    WaveEnemy enemy;
    int shotsRemaining;
    float secondsUntilNextShot;

    protected override void Setup(WaveEnemy enemy)
    {
        base.Setup(enemy);

        secondsUntilNextShot = 0;
        shotsRemaining = numShots;
        this.enemy = enemy;
    }

    protected override bool DoUpdate()
    {
        secondsUntilNextShot -= Time.deltaTime;

        if (secondsUntilNextShot < 0f)
        {
            enemy.Shoot();

            --shotsRemaining;

            if (shotsRemaining <= 0)
            {
                return true; //we are now complete
            }
            else
            {
                secondsUntilNextShot = secondsBetweenShots;
            }
        }

        return false;
    }
}
