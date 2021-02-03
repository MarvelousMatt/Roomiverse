using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFly : MonoBehaviour
{
    GravityMovement move;
    FlyMove fly;

    bool isSummoning;
    Rigidbody r;
    public float summonTime;
    public float jumpForce;
    public GameObject cutsceneFly;
    public GameObject flySpawn;

    public GameObject model;

    bool isFlying;

    public GameObject flyCamTarget;
    public GameObject groundCamTarget;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<GravityMovement>();
        fly = GetComponent<FlyMove>();
        r = GetComponent<Rigidbody>();
        CameraMove.instance.SwitchCamTarget(groundCamTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isSummoning)
        {
            if (isFlying)
            {
                fly.enabled = false;
                move.enabled = true;
                isFlying = false;
                model.SetActive(false);
                CameraMove.instance.SwitchCamTarget(groundCamTarget);

                //Animation and or toward planet launching
                //Include invoke when this action has an animation

            }
            else
            {
                isSummoning = true;
                move.enabled = false;
                r.velocity += transform.up * jumpForce;
                isFlying = true;
                Invoke("SpawnTakeoffFly", summonTime);
                CameraMove.instance.SwitchCamTarget(flyCamTarget);
            }



        }
    }

    void SpawnTakeoffFly()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Instantiate(cutsceneFly, flySpawn.transform.position, flySpawn.transform.rotation);
    }

    public void SwitchControls(bool isPlayerStanding)
    {
        if (isPlayerStanding)
        {
            move.enabled = true;
            fly.enabled = false;
        }
        else
        {
            fly.enabled = true;
            move.enabled = false;
            model.SetActive(true);
            isSummoning = false;
        }
    }


}
