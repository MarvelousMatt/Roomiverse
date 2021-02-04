using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapValues;

public class Bullet : MonoBehaviour
{
    int damage;
    float lifetime;
    float gravity;
    GravityObject gravityObject;
    GravityObject defaultGravityObject;
    bool isEnemy;
    float liveTime;
    int minDamage;
    int maxDamage;

    public Color lightColor;

    int currentGravPriority = 0;

    Rigidbody r;

    public void SetupBullet(int damageIn, float gravityIn, GravityObject gravObjectIn, Vector3 velocityIn, float lifetimeIn)
    {
        damage = damageIn;
        gravity = gravityIn;
        gravityObject = gravObjectIn;
        r.velocity = velocityIn;
        lifetime = lifetimeIn;
        isEnemy = true;
        //Colour stuff here
    }

    public void SetupBullet(int damageIn, float gravityIn, GravityObject gravObjectIn, Vector3 velocityIn, float lifetimeIn, int minDamageIn, int maxDamageIn)
    {
        damage = damageIn;
        gravity = gravityIn;
        gravityObject = gravObjectIn;
        r.velocity = velocityIn;
        lifetime = lifetimeIn;
        isEnemy = false;
        minDamage = minDamageIn;
        maxDamage = maxDamageIn;
        //Colour stuff here
    }


    void Awake()
    {
        r = GetComponent<Rigidbody>();
        liveTime = Time.time;
    }


    void FixedUpdate()
    {

        if ((Time.time - liveTime) > lifetime)
        {
            Destroy(gameObject);
        }

        if (!isEnemy)
        {
            damage = (int)MapValuesExtension.Map(Time.time, liveTime, liveTime + lifetime, minDamage, maxDamage);
        }
        

        if (gravityObject != null)
        {
            r.AddForce((gravityObject.transform.position - transform.position).normalized * gravity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && !isEnemy)
        {
            Debug.Log(damage);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, transform.position);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //Deal damage to player
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    //Handle gravity somewhere around here
}
