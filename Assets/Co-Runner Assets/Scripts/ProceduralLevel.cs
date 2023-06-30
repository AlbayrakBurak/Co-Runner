using UnityEngine;

public class ProceduralLevel : MonoBehaviour
{
    public int platformLength = 20;
    public GameObject floorPrefab;
    public GameObject obstaclePrefab;
    public GameObject startPoint;
    public GameObject endPoint;
    public TextMesh progressText;

    private int progress = 0;
    private bool levelFinished = false;

    private void Start()
    {
        GenerateLevel();
    }

    private void Update()
    {
        if (levelFinished) return;

        if (progress >= platformLength)
        {
            levelFinished = true;
            progressText.text = "Level Completed!";
        }
        else
        {
            float completionPercentage = (float)progress / platformLength * 100;
            progressText.text = string.Format("{0:0}% Completed", completionPercentage);
        }
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < platformLength; i++)
        {
            float chance = Random.Range(0f, 1f);
            GameObject floor = Instantiate(floorPrefab, new Vector3(i, 0, 0), Quaternion.identity);
            floor.transform.parent = transform;

            if (chance < 0.1f)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(i, 1, 0), Quaternion.identity);
                obstacle.transform.parent = transform;
            }
        }

        Instantiate(startPoint, new Vector3(0, 1, 0), Quaternion.identity);
        Instantiate(endPoint, new Vector3(platformLength - 1, 1, 0), Quaternion.identity);
    }

    public void IncreaseProgress()
    {
        progress++;
    }
}
