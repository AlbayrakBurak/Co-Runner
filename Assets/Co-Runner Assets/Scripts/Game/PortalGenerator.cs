using UnityEngine;

public class PortalGenerator : MonoBehaviour
{
    public GameObject addPortal;
    public GameObject multipPortal;
    public float obstacleYPosition = 0.5f;
    public float obstacleScale = 7.2f;
    public float firstObstacleXPosition = 3.75f;
    public float secondObstacleXPosition = -3.75f;
    // public float minDistanceBetweenObstaclesZ = 50f;
    // public float maxDistanceBetweenObstaclesZ = 100f;

     
    private void Start()
    {
        
        
        Vector3 firstObstaclePosition = new Vector3(firstObstacleXPosition, obstacleYPosition, Random.Range(30f,200f));
        Vector3 secondObstaclePosition = new Vector3(secondObstacleXPosition, obstacleYPosition, firstObstaclePosition.z);

        GameObject firstObstacle = Instantiate(addPortal, firstObstaclePosition, Quaternion.identity);
        firstObstacle.transform.localScale = new Vector3(obstacleScale, firstObstacle.transform.localScale.y, firstObstacle.transform.localScale.z);
        GameObject secondObstacle = Instantiate(multipPortal, secondObstaclePosition, Quaternion.identity);
        secondObstacle.transform.localScale = new Vector3(obstacleScale, secondObstacle.transform.localScale.y, secondObstacle.transform.localScale.z);
        
    }  
}
