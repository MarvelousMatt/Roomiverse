using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelRotation : MonoBehaviour
{
    Rigidbody r;
    public GameObject model;
    public float modelRotationSpeed;
    public float modelRotationTime;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;

        model.transform.up = transform.up;

        model.transform.rotation *= Quaternion.Euler(new Vector3(0, targetAngle, 0));
    }
}
