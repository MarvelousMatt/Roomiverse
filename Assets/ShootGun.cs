using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public float projectileLifetime;
    public float projectileSpeed;
    public GameObject bulletSpawn;
    public GameObject bulletPrefab;
    public int damage;
    public float gravity;
    public GravityObject defaultGravObject;
    public Color lightColor;

    //I hate this whole script so far. I will clean it up later when it comes to pooling
    //Areas to look at specifically:
    //Gravity object passing
    //Default gravity object passing
    //pooling
    //using psuedo constructor from bullet

    public void Shoot()
    {
        //Pull from pool eventually

        //Find the pool for this object
        //pool = FindPool();

        //Projectile proj = pool.RequestObject(transform.position + transform.forward + fireOffset).GetComponent<Projectile>();

        //proj.pool = pool;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        //Possibly more effects on destroy?
        Destroy(bullet, projectileLifetime);


        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * projectileSpeed;
        //projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);


        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().gravity = gravity;

        bullet.GetComponent<Bullet>().gravityObject = GetComponent<GravityMovement>().gravityObject;
        bullet.GetComponent<Bullet>().defaultGravityObject = defaultGravObject;
       
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

}
