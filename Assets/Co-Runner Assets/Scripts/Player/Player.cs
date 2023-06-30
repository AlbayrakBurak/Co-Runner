using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FollowerCreator followerCreator;
    private Transform Center;
    [SerializeField] private float speed;


    private void Awake()
    {
        
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
        Center = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        
    }

    void FixedUpdate()
    {
        if (!followerCreator.holdoff)
        {
            transform.position = Vector3.MoveTowards(transform.position, Center.position, Time.fixedDeltaTime * speed);
           
        }
    }


    
}
