using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DataManager;
using System;

public class GameManager : MonoBehaviour
{
    private FollowerCreator followerCreator;
    public FinishLine finishLine;
    public GameObject failPanel;
    [Header("UI References :")]
    public Image fillImage;
    //public TextMeshProUGUI tapToStartText;
    [Header("Transform References :")]
    public Transform player;
    public Transform end;
    public GameObject StartPanel;
    public GameObject StartButton;
    public bool isStart = false;

    private float fullDistance;
    [HideInInspector] public bool gameStart = false;
    private ObstacleGenerator obstacleGeneratorFinish;
    Data data = new Data();
    Store_Manager store = new Store_Manager();
    [Header("-Heads")]
    public GameObject[] Heads;
    [Header("-Glasses")]
    public GameObject[] Glasses;
    [Header("-Hands")]
    public GameObject[] Hands;
    [Header("-Theme")]
    public Material[] Theme;
    public Material DefaultTheme;
    public SkinnedMeshRenderer _Renderer;
    [Header("-StartPanel")]
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Diamond;
    public AudioSource ButtonSound;
    public GameObject BuyADS;
    
    private void Awake()
    {
        CheckItem();
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
        finishLine=GameObject.FindGameObjectWithTag("FinishLine").GetComponent<FinishLine>();
        fullDistance = GetDistance();
        Coins.text = data.readData_i("Coins").ToString();
        Diamond.text = data.readData_i("Diamond").ToString();
        data.readData_i("isBuyNoAds");

        Debug.Log("Reklam Değeri= " + data.readData_i("isBuyNoAds"));
        if (data.readData_i("isBuyNoAds") == 1)
        {
            BuyADS.SetActive(false);
        }

    }
    private void Start()
    {
        Debug.Log("Değerler" + PlayerPrefs.GetInt("ActiveHead") +
        PlayerPrefs.GetInt("ActiveHands") +
        PlayerPrefs.GetInt("ActiveGlasses") +
        PlayerPrefs.GetInt("ActiveTheme") +
        PlayerPrefs.GetInt("isBuyNoAds") +
        PlayerPrefs.GetInt("Coins"));
    }


    private void Update()
    {
        if (isStart == true)
        {
            gameStart = true;
            StartPanel.gameObject.SetActive(false);
            Time.timeScale = 1;

        }
        

        if (followerCreator.players.Count == 0)
        {
            gameStart = false;
            failPanel.SetActive(true);
        }

        float newDistance = GetDistance();
        float progressValue = Mathf.InverseLerp(fullDistance, 0, newDistance);
        UpdateProgressFill(progressValue);
    }

    private float GetDistance()
    {
        Vector3 zPlayer = new Vector3(0, 0, player.position.z);

        Vector3 zEnd = new Vector3(0, 0, end.position.z);
        return Vector3.Distance(zPlayer, zEnd);
    }

    public void UpdateProgressFill(float value)
    {
        if (gameStart)
        {
            fillImage.fillAmount = value;
        }
    }


    public void RestartGame()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }


    public void ContinueGame()
    {
       ButtonSound.Play();
       
        data.saveData_int("Coins", data.readData_i("Coins") + 100);
        finishLine.LevelCompleted=false;
        SceneManager.LoadScene(0);
    }
    public void Customize()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(1);
    }

    public void Daily()
    {
        ButtonSound.Play();
        // data.saveData_int("Coins",data.readData_i("Coins")+10);

    }
    public void Bonus()
    {
        ButtonSound.Play();
    }
    public void Shop()
    {
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        ButtonSound.Play();
        isStart=true;
           

        
        //  gameObject.GetComponent<AudioSource>().Play();

    }

    public void buyNoADS()
    {
        SceneManager.LoadScene(2);
    }

    public void CheckItem()
    {
        if (data.readData_i("ActiveHead") != -1)
            Heads[data.readData_i("ActiveHead")].SetActive(true);
        if (data.readData_i("ActiveGlasses") != -1)
            Glasses[data.readData_i("ActiveGlasses")].SetActive(true);
        if (data.readData_i("ActiveHands") != -1)
            Hands[data.readData_i("ActiveHands")].SetActive(true);

        if (data.readData_i("ActiveTheme") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Theme[data.readData_i("ActiveTheme")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefaultTheme;
            _Renderer.materials = mats;
        }
     }

}
