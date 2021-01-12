using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public int knockBackForce;

    [Header("Offence")]
    public int damage;
    public float fireDelay;
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public float bulletLifetime;
    public float bulletSpeed;
    public float bulletGravity;

    [Header("Misc")]
    public float gravity;
    public GravityObject defaultGravityObject;
    public GravityObject gravityObject;
    public float rotationSpeed;

    GameObject player;

    Rigidbody r;

    float nextFire;

    int currentGravPriority = 0;

    void Start()
    {
        r = GetComponent<Rigidbody>();
        player = GameManager.player;
        currentHealth = maxHealth;
        defaultGravityObject = GameManager.defaultGravityObject;
        gravityObject = defaultGravityObject;
    }

    public void TakeDamage(int damage, Vector3 hitPos)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            EnemyDeath();
        }

        //Do knockback here later

    }

    public virtual void Move(Vector3 direction)
    {
        
    }

    public void FaceDirection()
    {

    }


    public void FireTimer()
    {
        //if (pool == null)
        //    pool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>();

        if (Time.time < nextFire)
            return;

        Fire(bulletPrefab, transform.forward, gameObject);
        nextFire = Time.time + fireDelay;
    }

    //Finds a projectile in the pool, gives it velocity and sets it up according to the parameters on this object
    void Fire(GameObject projectile, Vector3 fireDirection, GameObject firedFrom)
    {
        //Find the pool for this object
        //pool = FindPool();

        //pool.RequestObject(transform.position + transform.forward + fireOffset).GetComponent<Projectile>();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        //Possibly more effects on destroy?
        Destroy(bullet, bulletLifetime);


        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        //projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);


        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().gravity = bulletGravity;

        bullet.GetComponent<Bullet>().gravityObject = gravityObject;
        bullet.GetComponent<Bullet>().defaultGravityObject = defaultGravityObject;

    }

    private void FixedUpdate()
    {
        r.AddForce((gravityObject.transform.position - transform.position).normalized * gravity);

        RaycastHit hit;
        Physics.Raycast(transform.position, (gravityObject.transform.position - transform.position).normalized, out hit, 20);

        transform.up = Vector3.Lerp(transform.up, hit.normal, rotationSpeed * Time.deltaTime);

    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Stayed within " + other.gameObject.name);
        if (other.gameObject.CompareTag("GravityObject") && other.GetComponent<GravityObject>() && other.gameObject.GetComponent<GravityObject>() != gravityObject)
        {
            //Debug.Log("Had tag and component");

            if (other.GetComponent<GravityObject>().priority >= currentGravPriority)
            {
                //Debug.Log("Swapped object");
                gravityObject = other.gameObject.GetComponent<GravityObject>();
                currentGravPriority = other.GetComponent<GravityObject>().priority;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gravityObject == other.gameObject.GetComponent<GravityObject>())
        {
            // Debug.Log("Exiting current gravity");
            gravityObject = defaultGravityObject;
            currentGravPriority = 0;
        }
    }



}
