using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : Enemy
{

    private void Update()
    {
        ModelFace(GameManager.player);
        FireTimer();
    }
}
