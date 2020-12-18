using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject camTarget;
    public float linSpeed;
    public float rotSpeed;

    public static CameraMove instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, Time.deltaTime * linSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, Time.deltaTime * rotSpeed);
    }

    public void SwitchCamTarget(GameObject gameObject)
    {
        camTarget = gameObject;
    }

    /*
    //Do a sicknasty lerp
    cameraOrbit.transform.position = Vector3.Lerp(cameraOrbit.transform.position, targetPos, lerpSpeed* Time.deltaTime);
            cameraOrbit.transform.LookAt(player.transform);
    */
}
