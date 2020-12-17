using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    //Defines what level this gravity field is. If it is above the previous field it is taken as the new object
    public int priority;

    //Defines whether the object is a cube or not
    public bool isCube;
}
