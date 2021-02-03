using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelRotation : MonoBehaviour
{
    Rigidbody r;
    public GameObject model;
    public float modelRotationSpeed;
    public float modelRotationTime;

    Vector3 giz;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 point = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        giz = point;

        Vector3 store = model.transform.localRotation.eulerAngles;
        model.transform.LookAt(point, transform.up);
        model.transform.localEulerAngles = new Vector3(store.x, model.transform.localEulerAngles.y, store.z);

        /*
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;

        model.transform.up = transform.up;

        model.transform.rotation *= Quaternion.Euler(new Vector3(0, targetAngle, 0));
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(giz, 0.5f);
    }


}
