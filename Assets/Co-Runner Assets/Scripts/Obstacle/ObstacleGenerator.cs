using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles; // Engellerin listesi

    public float minDistance = 2f; // Oluşturulan engeller arasındaki minimum mesafe
    public float maxDistance = 3f; // Oluşturulan engeller arasındaki maksimum mesafe
    // public int obstacleCount = 5; // Oluşturulacak engel sayısı

    public float startZ = 25f; // Engellerin başlangıç z pozisyonu
    private string previousObstacleTag; // Önceki engelin tag'ını tutmak için   
    private string secondPreviousObstacleTag; // Bir önceki engelin tag'ını tutmak için
    // public GameObject finishLinePrefab;

    public GameObject finisLine;
     public GameObject Portal;

     public GameObject EnemyBase;

     public GameObject Boss;
    public GameObject GameHouse;
     public GameObject Chest;

    


    void Start()
    {
        
       
        float createObstacleRandomCount =Random.Range(8f,16f);

        float zPos =startZ ; // Engellerin z pozisyonunu izlemek için bir değişken

         

        // Engelleri oluştur
        for (int i = 0; i < createObstacleRandomCount; i++)

        {

            GameObject obstacle;
            do
            {
                
                obstacle = obstacles[Random.Range(0, obstacles.Length)];
            } while (obstacle.tag == previousObstacleTag || obstacle.tag == "EnemyBase" && (previousObstacleTag == "EnemyBase" || secondPreviousObstacleTag == "EnemyBase") || obstacle.tag=="EnemyBase" );

            


            // Rastgele bir engel seç
            //GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];

            // Engeli oluştur
            GameObject newObstacle = Instantiate(obstacle);

            // Engelin pozisyonunu ve rotasyonunu belirle
            if (newObstacle.CompareTag("Knife"))
            {
                // Knife engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(Random.Range(minDistance, maxDistance), 1, Random.Range(-maxDistance, maxDistance));
                float randomX = Random.Range(-8.5f, 8.5f) <=0 ? -8.5f : 8.5f;
                newPosition.x = randomX;
                if (newPosition.x < 0) {
                    newPosition.x = -8.5f;
                    newObstacle.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else {
                    newPosition.x = 8.5f;
                    newObstacle.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            }
            else if (newObstacle.CompareTag("RockingBall"))
            {
                // RockingBall engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(0, 11.5f, Random.Range(-maxDistance, maxDistance));
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            }
            else if (newObstacle.CompareTag("Hammer"))
            {
                // Hammer engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(Random.Range(minDistance, maxDistance), 4, Random.Range(-maxDistance, maxDistance));
                float randomX = Random.Range(-7f, 7f) <=0 ? -7f : 7f;
                newPosition.x = randomX;
                if (newPosition.x < 0) {
                    newPosition.x = -7;
                    newObstacle.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else {
                    newPosition.x = 7f;
                    newObstacle.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            }
            else if (newObstacle.CompareTag("Sawbench"))
            {
                // Sawbench engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(Random.Range(minDistance, maxDistance), 0.7f, Random.Range(-maxDistance, maxDistance));
                float randomX = Random.Range(-5f, 5f) <=0 ? -4.65f : 4.65f;
                newPosition.x = randomX;
                if (newPosition.x < 0) {
                    newPosition.x = -4.65f;
                    newObstacle.transform.rotation = Quaternion.Euler(0, 180, 0);
                } else {
                    newPosition.x = 4.65f;
                    newObstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            }
            else if (newObstacle.CompareTag("SpearTrap"))
            {
                // SpearTrap engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(Random.Range(-4.85f, 4.85f), 0.87f, Random.Range(-maxDistance, maxDistance));
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            }

            else if (newObstacle.CompareTag("EnemyBase"))
            {
                 
                // EnemyBase engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(0, 0.5f, Random.Range(-maxDistance, maxDistance));
                newObstacle.transform.rotation = Quaternion.Euler(0, 180, 0);
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;
            
            }

            else if (newObstacle.CompareTag("Portal"))
            {
                // Portal engelinin pozisyonu belirle
                Vector3 newPosition = transform.position + new Vector3(0f, 0.87f, Random.Range(-maxDistance, maxDistance));
                newPosition.z = zPos;
                newObstacle.transform.position = newPosition;

                // Instantiate(EnemyBase, new Vector3(0, 0.5f,zPos+20),Quaternion.Euler(0, 180, 0));
            }



            // Engeller arasındaki mesafeyi belirle
            zPos += Random.Range(minDistance, maxDistance);
            previousObstacleTag = obstacle.tag; // Önceki engelin tag'ını güncelle
            secondPreviousObstacleTag=previousObstacleTag;
           
            // Oluşturulan son engelin pozisyonunu alalım
            
        }
        

        // // Finish Line prefabını +50 z pozisyonunda güncelleyelim

       finisLine.GetComponent<Transform>().gameObject.transform.position= new Vector3(0,0.5f,zPos+8);
        Boss.GetComponent<Transform>().gameObject.transform.position=
        new Vector3(
            finisLine.GetComponent<Transform>().gameObject.transform.position.x,
            finisLine.GetComponent<Transform>().gameObject.transform.position.y+0.5f,
            finisLine.GetComponent<Transform>().gameObject.transform.position.z+12);
            Instantiate(Portal, new Vector3(0f, 0.87f,zPos),Quaternion.identity);
             
        GameHouse.GetComponent<Transform>().gameObject.transform.position= new Vector3(0,0.5f,zPos+23);
        Chest.GetComponent<Transform>().gameObject.transform.position= new Vector3(0,0.5f,zPos+27);
          
   
     }
   
}

    
