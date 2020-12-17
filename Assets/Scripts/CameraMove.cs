using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject camTarget;
    public float linSpeed;
    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, Time.deltaTime * linSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, Time.deltaTime * rotSpeed);
    }

    /*
    //Do a sicknasty lerp
    cameraOrbit.transform.position = Vector3.Lerp(cameraOrbit.transform.position, targetPos, lerpSpeed* Time.deltaTime);
            cameraOrbit.transform.LookAt(player.transform);
    */
}
