using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    
    public GameObject ObjToFollow;
    public GameObject ChildObj;
    [HideInInspector]
    public Vector3 ChildObjStartingPos;
    
}
