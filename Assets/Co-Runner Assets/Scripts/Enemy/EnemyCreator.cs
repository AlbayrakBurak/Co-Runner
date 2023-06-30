using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private Portal portal;
    public GameObject enemy;
    public FollowerCreator followerCreator;
    public PlayerController playerBase;
    public List<GameObject> enemys = new List<GameObject>();
    [HideInInspector] public bool isEnemyAlive = true;

    public FinishLine finishLine;
    public  GameObject chest;



    void Awake(){
        followerCreator=GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
       finishLine=GameObject.FindGameObjectWithTag("FinishLine").GetComponent<FinishLine>();
       playerBase=GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerController>();
       
        
        
    }
    void Start()
    {
       SpawnEnemy();
        
    }

    private void Update()
    {
         
        if (enemys.Count == 0 && followerCreator.players.Count>=1)
        {
            isEnemyAlive = false;
            chest.SetActive(true);
            playerBase.speed=3f;
            StartCoroutine(waitSeconds());
            playerBase.swipeSpeed=5f;

        }

    }
    IEnumerator waitSeconds(){

yield return new WaitForSeconds(2.9f);

finishLine.LevelCompleted=true;

}
   

    public void SpawnEnemy()
    {
        for (int i = 0; i < Random.Range(12,30); i++)
        {
            GameObject newEnemy = Instantiate(enemy, EnemyPosition(), Quaternion.identity, transform);
            enemys.Add(newEnemy);
        }
    }

    public Vector3 EnemyPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;

        return newPos;
    }
    
}
