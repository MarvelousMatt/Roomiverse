using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float maxSpeed;
    public float jumpForce;
    public float movementSmoothing;

    [Header("Rotation")]
    public float rotationSpeed;
    public float groundedRotationSpeed;
    public float cubeTolerance;
    bool groundTurn = false;

    [Header("Gravity")]
    public GravityObject gravityObject;
    int currentGravPriority = -1;
    public float gravityForce;
    public float groundedDistance;
    public GravityObject defaultGravityObject;

    //Inputs
    decimal xInput;
    decimal zInput;
    bool isJumping;

    //Components
    Rigidbody r;

    //Internals
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        gravityObject = defaultGravityObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        //gravityObject = GravityCheck();
       
    }

    void GetInputs()
    {
        xInput = (decimal)Input.GetAxis("Horizontal");
        zInput = (decimal)Input.GetAxis("Vertical");
        isJumping = Input.GetKey(KeyCode.Space);
    }

    /*
    private void LateUpdate()
    {
        
    }
    */
    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, -transform.up, groundedDistance);

        Debug.DrawLine(transform.position, transform.position - transform.up * groundedDistance);

        if (groundTurn)
        {
            transform.up = Vector3.Lerp(transform.up, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        DoMovement();
        DoRotation();
    }

    /*
    //Checks proximity to gravity bodies
    //returns the closest gravity object
    GameObject GravityCheck()
    {
        /*


        Ray ray = new Ray(transform.position, -transform.up);

        if (!Physics.SphereCast(ray, 10, maxGravityDistance))
        {
            gravityObject = defaultGravityObject;
        }

        /*
        Collider[] colliders = Physics.OverlapSphere(transform.position, 500);

        GameObject grav = gravityObject.gameObject;

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.CompareTag("GravityObject") && 
                Vector3.Distance(colliders[i].ClosestPoint(transform.position),transform.position) < Vector3.Distance(grav.GetComponent<Collider>().ClosestPoint(transform.position), transform.position))
            {
                grav = colliders[i].gameObject;
            }
        }

        Debug.Log(grav.name);
        
        return grav;
    }
    */

    //Rotates the player towards whichever planet they are standing on
    void DoRotation()
    {
        if(gravityObject != null)
        {
            //Check if the object is a cube or not
            //If it is a cube, use normals, if not use rotation
            if(gravityObject.GetComponent<GravityObject>().isCube)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, (gravityObject.transform.position - transform.position).normalized,out hit, 20);

                if (!grounded)
                {
                    transform.up = Vector3.Lerp(transform.up, hit.normal, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.up = Vector3.Lerp(transform.up, hit.normal, groundedRotationSpeed * Time.deltaTime);
                }

            }
            else
            {
                Vector3 gravityUp = (transform.position - gravityObject.transform.position).normalized;

                Vector3 localUp = transform.up;

                if (!grounded)
                {
                    transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.up = Vector3.Lerp(transform.up, gravityUp, groundedRotationSpeed * Time.deltaTime);
                }
            }

          
        }
    }

    void DoMovement()
    {
        float xIntent;
        float yIntent;
        float zIntent;

        //Gather player input
        xIntent = (float)xInput * speed * Time.deltaTime;
        zIntent = (float)zInput * speed * Time.deltaTime;
        yIntent = 0;

        //If jump is input, add upward momentum
        if (isJumping && grounded)
        {
            yIntent += jumpForce;
            Debug.Log("Jumped");
        }

        //Setting vectors for directions
        //Also includes force pushing player down to make sure they stick to the gravity object
        Vector3 xVec = transform.right.normalized * xIntent;// + -transform.up * Mathf.Abs(xIntent);
        Vector3 yVec = transform.up.normalized * yIntent;
        Vector3 zVec = transform.forward.normalized * zIntent;// + -transform.up * Mathf.Abs(zIntent);

        //Apply gravity to the player if they are not on the ground

        if(gravityObject.isCube)
        {
            yVec += -transform.up.normalized * gravityForce;
        }

        if (!gravityObject.isCube)
        {
            yVec += (gravityObject.transform.position - transform.position).normalized * gravityForce;
        }

        //Summing up the vectors 
        Vector3 movementIntent = xVec + yVec + zVec;

        //Lerping into the next motion smoothly
        r.velocity = Vector3.Lerp(r.velocity, r.velocity + movementIntent, movementSmoothing * Time.deltaTime);

        //Contain velocity by maxSpeed
        r.velocity = Vector3.ClampMagnitude(r.velocity, maxSpeed);

        //Debug.Log("yintent " + yIntent + "  " + "yVec " + yVec + "  " + "Move intent " + movementIntent);
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
                groundTurn = false;
                gravityObject = other.gameObject.GetComponent<GravityObject>();
                currentGravPriority = other.GetComponent<GravityObject>().priority;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(gravityObject == other.gameObject.GetComponent<GravityObject>())
        {
           // Debug.Log("Exiting current gravity");
            gravityObject = defaultGravityObject;
            currentGravPriority = 0;
            groundTurn = true;
        }
    }


}
