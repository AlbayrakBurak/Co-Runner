using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManager;

public class FinishLine : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] private GameObject successPanel;
    public bool LevelCompleted = false;



    Data data = new Data();

    [System.Obsolete]
    public void Update()
    {
        if (LevelCompleted == true)
        {
            successPanel.SetActive(true);
            gm.gameStart = false;
            Time.timeScale = 0;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        gm.UpdateProgressFill(1);
    }


}
