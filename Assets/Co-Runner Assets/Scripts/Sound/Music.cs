using System.Collections;
using System.Collections.Generic;
using DataManager;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource MainMusic;
    private static GameObject instance;
    public bool Sound=true;
    public GameObject[] SoundButtons;

    Data _data=new Data();
    void Start()
    {
    MainMusic.volume=0.5f;
        

        DontDestroyOnLoad(gameObject);

        if(instance==null){
            instance=gameObject;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundController (int index){
        
        if(index==0){
       

        SoundButtons[1].SetActive(true);
        SoundButtons[0].SetActive(false);
        instance.gameObject.SetActive(false);
        }
        if(index==1){
        

        SoundButtons[1].SetActive(false);
        SoundButtons[0].SetActive(true);
        instance.gameObject.SetActive(true);
        }

       
    }
}
