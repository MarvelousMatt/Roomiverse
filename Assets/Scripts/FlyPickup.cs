using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyPickup : MonoBehaviour
{
    UnityEvent pickup;

    private void Awake()
    {
        

        pickup = new UnityEvent();

        pickup.AddListener(() =>
        {
            GameManager.player.GetComponent<SummonFly>().SwitchControls(false);
        });

    }

    public void PickupEvent()
    {
        pickup.Invoke();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        pickup.RemoveAllListeners();
    }
}
