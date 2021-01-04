using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifetime;
    public float gravity;

    public Color lightColor;


    public GravityObject gravityObject;

    public GravityObject defaultGravObject;

    int currentGravPriority = 0;

    Rigidbody r;

    public void SetupBullet()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        r.AddForce((
            gravityObject.transform.position - 
            transform.position).normalized * gravity);
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
            gravityObject = defaultGravObject;
            currentGravPriority = 0;
        }
    }

    //Handle gravity somewhere around here
}
