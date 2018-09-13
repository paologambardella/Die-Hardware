using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour 
{
    [SerializeField] string bulletPool = "Bullets";
    [SerializeField] string bulletType = "Basic Bullet";

    public void Shoot()
    {
//        Debug.Log(this.name + "Shooting " + this.transform.forward);

        Bullet bullet = ObjectPooler.instance.GetItem<Bullet>(bulletPool, bulletType);
        bullet.transform.position = this.transform.position;
        bullet.transform.forward = this.transform.forward;
        bullet.Fire();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2f);
    }
}
