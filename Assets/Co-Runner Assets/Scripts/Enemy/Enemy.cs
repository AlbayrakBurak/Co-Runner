using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerDetect playerDetect;
    private EnemyCreator enemyCreator;
    private FollowerCreator followerCreator;
    private Transform playerBase;
    private bool kill = false;
    private Animator goblinAnimator;

    public GameObject DestroyParticle;    

    private void Awake()
    {
        playerDetect = FindObjectOfType<PlayerDetect>();
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        enemyCreator = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<EnemyCreator>();
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
        
        

    }
    void Start(){
        
        
    }

    private void FixedUpdate()
    {
        if (playerDetect.rush)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerBase.position, Time.fixedDeltaTime * 4f);


         }
       
    } 

    private void OnCollisionEnter(Collision collision)
    
    {
        if (collision.gameObject.CompareTag("Player") && !kill)
        {
            kill = true;
           
            gameObject.GetComponent<AudioSource>().Play();
            followerCreator.players.Remove(collision.gameObject);
            
            collision.transform.parent = null;
            collision.gameObject.SetActive(false);
           
          

            enemyCreator.enemys.Remove(gameObject);
            
            gameObject.SetActive(false);
       
              if(!collision.gameObject.activeInHierarchy){
                 
                Instantiate(DestroyParticle,new Vector3 (transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
            }
            
           
            
           
        }
    }
}
