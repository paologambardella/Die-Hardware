using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour 
{
    [SerializeField] float secondsPerShot = 0.5f;
    [SerializeField] FireBullet gun;

    bool shooting = false;
    float secondsUntilNextShot;

    public void SetShooting(bool shooting)
    {
        if (this.shooting != shooting)
        {
            this.shooting = shooting;
            secondsUntilNextShot = 0f;
        }
    }

    void Update()
    {
        if (shooting)
        {
            secondsUntilNextShot -= Time.deltaTime;

            if (secondsUntilNextShot < 0f)
            {
                Shoot();
                secondsUntilNextShot += secondsPerShot;
            }
        }
    }

    void Shoot()
    {
        gun.Shoot();
    }
}
