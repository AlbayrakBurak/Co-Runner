using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markFreezeScript : MonoBehaviour
{

    public Transform freeze;
    public Transform Player;
    public void Update(){
        
        freeze.rotation=Player.rotation;
        freeze.position=Player.transform.position;
        
    }
}
