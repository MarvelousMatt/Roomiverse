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

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<GravityMovement>();
        fly = GetComponent<FlyMove>();
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSummoning)
        {
            isSummoning = true;
            move.enabled = false;
            r.velocity += transform.up * jumpForce;
            Invoke("SpawnAndMoveFly", summonTime);
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
        }
    }


}
