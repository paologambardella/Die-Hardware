using UnityEngine;
using System.Collections;

public class MultiGun : EnemyGun 
{
    [SerializeField] FireBullet[] guns;

    public override void Shoot()
    {
        for (int i = 0; i < guns.Length; ++i)
        {
            if (guns[i] != null)
            {
                guns[i].Shoot();
            }
        }
    }
}
