using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player { get; private set; }
    public static GravityObject defaultGravityObject;

    public GravityObject setDefaultGravityObject;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        defaultGravityObject = setDefaultGravityObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
