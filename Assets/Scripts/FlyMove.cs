using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    Vector3 flyCamTargetPostion;

    public GameObject lookTarget;

    public float speed;
    public float boostSpeed;
    public float turnSpeed;
    public float rotationSmoothing;

    float xRotation;
    float yRotation;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoRotation();
        DoMovement();
    }

    void DoRotation()
    {
        xRotation = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        yRotation = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        Quaternion yRot = Quaternion.Euler(-yRotation, 0, 0);
        Quaternion xRot = Quaternion.Euler(0, xRotation, 0);

        transform.rotation *= yRot * xRot;
        transform.LookAt(lookTarget.transform, Vector3.up);
    }

    void DoMovement()
    {
        float xIntent;
        float zIntent;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            xIntent = Input.GetAxis("Horizontal") * boostSpeed * Time.deltaTime;
            zIntent = Input.GetAxis("Vertical") * boostSpeed * Time.deltaTime;
        }
        else
        {
            xIntent = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            zIntent = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }

        

        transform.position += (xIntent * transform.right) + (zIntent * transform.forward);
    }
}
