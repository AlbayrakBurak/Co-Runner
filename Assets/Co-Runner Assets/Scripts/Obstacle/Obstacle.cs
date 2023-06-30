using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private FollowerCreator followerCreator;
    private Vector3 boostPosition;
    private float elapsedTime;
    AudioSource deathSound;
   

    private void Awake()
    {
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
      
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(gameObject.CompareTag("Boost")){
            boostPosition=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z);
           collision.gameObject.transform.position=Vector3.Lerp(collision.gameObject.transform.position,boostPosition,1f);
        }

        if(gameObject.CompareTag("Obstacle") ){
            gameObject.GetComponent<AudioSource>().Play();
        collision.gameObject.SetActive(false);
        followerCreator.players.Remove(collision.gameObject);

        collision.transform.parent = null;
        }
        
    StartCoroutine(HoldOff());
    }

    IEnumerator HoldOff()
    {
        followerCreator.holdoff = true;
        yield return new WaitForSeconds(0.25f);
    followerCreator.holdoff = false;
    }

    
}
