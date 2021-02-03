using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public GameObject bulletSpawn;
    public GameObject bulletPrefab;
    public int damage;
    public float gravity;
    public GravityObject defaultGravObject;
    public Color lightColor;
    float nextFire;
    public float fireDelay;
    public int minDamage;
    public int maxDamage;

    //I hate this whole script so far. I will clean it up later when it comes to pooling
    //Areas to look at specifically:
    //Gravity object passing
    //Default gravity object passing
    //pooling
    //using psuedo constructor from bullet

    public void FireTimer()
    {
        //if (pool == null)
        //    pool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>();

        if (Time.time < nextFire)
            return;

        Shoot();
        nextFire = Time.time + fireDelay;
    }



    public void Shoot()
    {
        //Pull from pool eventually

        //Find the pool for this object
        //pool = FindPool();

        //Projectile proj = pool.RequestObject(transform.position + transform.forward + fireOffset).GetComponent<Projectile>();

        //proj.pool = pool;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        bullet.GetComponent<Bullet>().SetupBullet(damage, gravity, GetComponent<GravityMovement>().gravityObject, bullet.transform.forward * speed, lifetime,minDamage,maxDamage);

        GetComponent<AudioSource>().pitch = Random.Range(1f, 1.1f);
        GetComponent<AudioSource>().Play();

        /*
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * projectileSpeed;
        //projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);


        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().gravity = gravity;

        bullet.GetComponent<Bullet>().gravityObject = GetComponent<GravityMovement>().gravityObject;
        bullet.GetComponent<Bullet>().defaultGravityObject = defaultGravObject;

        */


    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireTimer();
        }
    }

}
