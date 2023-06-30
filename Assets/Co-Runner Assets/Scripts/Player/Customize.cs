using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataManager;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public class Customize : MonoBehaviour
{
    public Text CoinText;

    public GameObject[] panelManagement;
    public GameObject MenuCanvas;
    public GameObject[] GeneralPanels;
    public Button[] ButtonManagement;
    public TextMeshProUGUI BuyText;

    public GameObject CurrentProcessText;


    int ActivePanelIndex;

    [Header("-Heads")]
    public GameObject[] Heads;
    public Button[] HeadButtons;
    public Text HeadsText;

    [Header("-Glasses")]
    public GameObject[] Glasses;
    public Button[] GlassesButtons;
    public Text GlassesText;

    [Header("-Hands")]
    public GameObject[] Hands;
    public Button[] HandsButtons;
    public Text HandsText;
    [Header("-Theme")]
    public Material[] Theme;
    public Material DefaultTheme;
    public Button[] ThemeButtons;
    public Text ThemeText;
    public SkinnedMeshRenderer _Renderer;

    public Animator SavedAnimator;


    int HeadsIndex = -1;
    int GlassesIndex = -1;
    int HandsIndex = -1;
    int ThemeIndex = -1;

    dataManager _dataManager = new dataManager();
    Data _data = new Data();

    [Header("-Item Info")]
    public List<ItemInfo> _itemInfo = new List<ItemInfo>();

    public AudioSource[] Sounds;


    private void Awake()
    {




    }
    void Start()
    {
 

        // _data.saveData_int("Coins",1500);
        CoinText.text = _data.readData_i("Coins").ToString();
        Debug.Log("Kaydedilen Dosya yeri: " + Application.persistentDataPath);
        _dataManager.Load();
        _itemInfo = _dataManager.returnList();
        CurrentCheck(0, true);
        CurrentCheck(1, true);
        CurrentCheck(2, true);
        CurrentCheck(3, true);
        //Save();

    }

    public void CurrentCheck(int Stage, bool process = false)
    {

        if (Stage == 0)
        {
            //HEADS
            if (_data.readData_i("ActiveHead") == -1)
            {
                foreach (var item in Heads)
                {
                    item.SetActive(false);
                }
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = false;


                if (!process)
                {

                    HeadsIndex = -1;
                    HeadsText.text = "No Item";
                }
            }

            else
            {

                foreach (var item in Heads)
                {
                    item.SetActive(false);
                }
                HeadsIndex = _data.readData_i("ActiveHead");
                Heads[HeadsIndex].SetActive(true);

                HeadsText.text = _itemInfo[HeadsIndex].Item_name;
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = true;

            }
        }
        else if (Stage == 1)
        {
            //GLASSES

            if (_data.readData_i("ActiveGlasses") == -1)
            {
                foreach (var item in Glasses)
                {
                    item.SetActive(false);
                }
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = false;
                if (!process)
                {
                    GlassesIndex = -1;
                    GlassesText.text = "No Item";
                }
            }

            else
            {
                foreach (var item in Glasses)
                {
                    item.SetActive(false);
                }
                GlassesIndex = _data.readData_i("ActiveGlasses");
                Glasses[GlassesIndex].SetActive(true);
                GlassesText.text = _itemInfo[GlassesIndex + 14].Item_name;
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = true;

            }
        }


        else if (Stage == 2)
        {
            //HANDS


            if (_data.readData_i("ActiveHands") == -1)
            {
                foreach (var item in Hands)
                {
                    item.SetActive(false);
                }
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = false;
                if (!process)
                {
                    HandsIndex = -1;
                    HandsText.text = "No Item";
                }
            }

            else
            {
                foreach (var item in Hands)
                {
                    item.SetActive(false);
                }
                HandsIndex = _data.readData_i("ActiveHands");
                Hands[HandsIndex].SetActive(true);
                HandsText.text = _itemInfo[HandsIndex + 17].Item_name;
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = true;

            }
        }

        else
        {

            //THEME
            if (_data.readData_i("ActiveTheme") == -1)
            {

                if (!process)
                {
                    ThemeIndex = -1;
                    ThemeText.text = "Black";
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = false;
                }


                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultTheme;
                    _Renderer.materials = mats;
                    BuyText.text = "Owned";
                }


            }

            else
            {
                ThemeIndex = _data.readData_i("ActiveTheme");
                Material[] mats = _Renderer.materials;
                mats[0] = Theme[ThemeIndex];
                _Renderer.materials = mats;
                ThemeText.text = _itemInfo[ThemeIndex + 30].Item_name;
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
                ButtonManagement[1].interactable = true;
            }
        }
    }



    public void Buy()
    {
         Sounds[1].Play();
        if (ActivePanelIndex != -1)
        {
            switch (ActivePanelIndex)
            {
                case 0:

                    _itemInfo[HeadsIndex].isBuy = true;
                    _data.saveData_int("Coins", _data.readData_i("Coins") - _itemInfo[HeadsIndex].Item_coin);
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;
                    CoinText.text = _data.readData_i("Coins").ToString();


                    break;

                case 1:
                    _itemInfo[GlassesIndex + 14].isBuy = true;
                    _data.saveData_int("Coins", _data.readData_i("Coins") - _itemInfo[GlassesIndex + 14].Item_coin);
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;
                    CoinText.text = _data.readData_i("Coins").ToString();

                    break;

                case 2:
                    _itemInfo[HandsIndex + 17].isBuy = true;
                    _data.saveData_int("Coins", _data.readData_i("Coins") - _itemInfo[HandsIndex + 17].Item_coin);
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;
                    CoinText.text = _data.readData_i("Coins").ToString();
                    break;

                case 3:
                    _itemInfo[ThemeIndex + 30].isBuy = true;
                    _data.saveData_int("Coins", _data.readData_i("Coins") - _itemInfo[ThemeIndex + 30].Item_coin);
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;
                    CoinText.text = _data.readData_i("Coins").ToString();
                    break;
            }
        }
    }

    public void Save()
    {
 Sounds[2].Play();
        if (ActivePanelIndex != -1)
        {
            switch (ActivePanelIndex)
            {
                case 0:
                    _data.saveData_int("ActiveHead", HeadsIndex);
                    ButtonManagement[1].interactable = false;
                    // if (!SavedAnimator.GetBool("ok"))
                    // {
                    //     SavedAnimator.SetBool("ok", true);
                    // }

                    break;

                case 1:
                    _data.saveData_int("ActiveGlasses", GlassesIndex);
                    ButtonManagement[1].interactable = false;

                    // if (!SavedAnimator.GetBool("ok"))
                    // {
                    //     SavedAnimator.SetBool("ok", true);
                    // }
                    break;

                case 2:
                    _data.saveData_int("ActiveHands", HandsIndex);
                    ButtonManagement[1].interactable = false;

                    // if (!SavedAnimator.GetBool("ok"))
                    // {
                    //     SavedAnimator.SetBool("ok", true);
                    // }
                    break;

                case 3:
                    _data.saveData_int("ActiveTheme", ThemeIndex);
                    ButtonManagement[1].interactable = false;

                    // if (!SavedAnimator.GetBool("ok"))
                    // {
                    //     SavedAnimator.SetBool("ok", true);
                    // }
                    break;
            }
        }



    }






    public void Head_Buttons(string process)
    {
         Sounds[0].Play();
        if (process == "forward")
        {

            if (HeadsIndex == -1)
            {
                HeadsIndex = 0;
                Heads[HeadsIndex].SetActive(true);
                HeadsText.text = _itemInfo[HeadsIndex].Item_name;

                if (!_itemInfo[HeadsIndex].isBuy)
                {
                    BuyText.text = _itemInfo[HeadsIndex].Item_coin + " Coin";

                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[HeadsIndex].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            else
            {
                Heads[HeadsIndex].SetActive(false);
                HeadsIndex++;
                Heads[HeadsIndex].SetActive(true);
                HeadsText.text = _itemInfo[HeadsIndex].Item_name;

                if (!_itemInfo[HeadsIndex].isBuy)
                {
                    BuyText.text = _itemInfo[HeadsIndex].Item_coin + " Coin";

                    ButtonManagement[1].interactable = false;

                    if (_data.readData_i("Coins") < _itemInfo[HeadsIndex].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;



                }
            }
            if (HeadsIndex == Heads.Length - 1)
            {
                HeadButtons[1].interactable = false;
            }
            else
            {
                HeadButtons[1].interactable = true;
            }

            if (HeadsIndex != -1)
            {
                HeadButtons[0].interactable = true;
            }

        }

        else
        {
            if (HeadsIndex != -1)
            {
                Heads[HeadsIndex].SetActive(false);
                HeadsIndex--;


                if (HeadsIndex != -1)
                {
                    Heads[HeadsIndex].SetActive(true);
                    HeadButtons[0].interactable = true;
                    HeadsText.text = _itemInfo[HeadsIndex].Item_name;

                    if (!_itemInfo[HeadsIndex].isBuy)
                    {
                        BuyText.text = _itemInfo[HeadsIndex].Item_coin + " Coin";
                        ButtonManagement[1].interactable = false;

                        if (_data.readData_i("Coins") < _itemInfo[HeadsIndex].Item_coin)
                        {
                            ButtonManagement[0].interactable = false;
                        }
                        else
                        {
                            ButtonManagement[0].interactable = true;
                        }

                    }
                    else
                    {
                        BuyText.text = "Owned";
                        ButtonManagement[0].interactable = false;
                        ButtonManagement[1].interactable = true;

                    }
                }
                else
                {
                    HeadButtons[0].interactable = false;
                    HeadsText.text = "No Item";
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;
                }
            }

            else
            {
                HeadButtons[0].interactable = false;

                HeadsText.text = "No Item";
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;

            }
            if (HeadsIndex != Heads.Length - 1)
            {
                HeadButtons[1].interactable = true;

            }

        }

    }

    public void Glasses_Buttons(string process)
    {
         Sounds[0].Play();
        if (process == "forward")
        {

            if (GlassesIndex == -1)
            {
                GlassesIndex = 0;
                Glasses[GlassesIndex].SetActive(true);
                GlassesText.text = _itemInfo[GlassesIndex + 14].Item_name;

                if (!_itemInfo[GlassesIndex + 14].isBuy)
                {
                    BuyText.text = _itemInfo[GlassesIndex + 14].Item_coin + " Coin";
                    ButtonManagement[1].interactable = false;

                    if (_data.readData_i("Coins") < _itemInfo[GlassesIndex + 14].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            else
            {
                Glasses[GlassesIndex].SetActive(false);
                GlassesIndex++;
                Glasses[GlassesIndex].SetActive(true);
                GlassesText.text = _itemInfo[GlassesIndex + 14].Item_name;
                if (!_itemInfo[GlassesIndex + 14].isBuy)
                {
                    BuyText.text = _itemInfo[GlassesIndex + 14].Item_coin + " Coin";

                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[GlassesIndex + 14].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }


                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            if (GlassesIndex == Glasses.Length - 1)
            {
                GlassesButtons[1].interactable = false;
            }
            else
            {
                GlassesButtons[1].interactable = true;
            }

            if (GlassesIndex != -1)
            {
                GlassesButtons[0].interactable = true;
            }

        }

        else
        {
            if (GlassesIndex != -1)
            {
                Glasses[GlassesIndex].SetActive(false);
                GlassesIndex--;


                if (GlassesIndex != -1)
                {
                    Glasses[GlassesIndex].SetActive(true);
                    GlassesButtons[0].interactable = true;
                    GlassesText.text = _itemInfo[GlassesIndex + 14].Item_name;

                    if (!_itemInfo[GlassesIndex + 14].isBuy)
                    {
                        BuyText.text = _itemInfo[GlassesIndex + 14].Item_coin + " Coin";
                        ButtonManagement[1].interactable = false;
                        if (_data.readData_i("Coins") < _itemInfo[GlassesIndex + 14].Item_coin)
                        {
                            ButtonManagement[0].interactable = false;
                        }
                        else
                        {
                            ButtonManagement[0].interactable = true;
                        }

                    }
                    else
                    {
                        BuyText.text = "Owned";
                        ButtonManagement[0].interactable = false;
                        ButtonManagement[1].interactable = true;

                    }
                }
                else
                {
                    GlassesButtons[0].interactable = false;
                    GlassesText.text = "No Item";

                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                }
            }

            else
            {
                GlassesButtons[0].interactable = false;
                GlassesText.text = "No Item";
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
            }
            if (GlassesIndex != Glasses.Length - 1)
            {
                GlassesButtons[1].interactable = true;
            }
        }

    }


    public void Hands_Buttons(string process)
    {   
         Sounds[0].Play();

        if (process == "forward")
        {

            if (HandsIndex == -1)
            {
                HandsIndex = 0;
                Hands[HandsIndex].SetActive(true);
                HandsText.text = _itemInfo[HandsIndex + 17].Item_name;

                if (!_itemInfo[HandsIndex + 17].isBuy)
                {
                    BuyText.text = _itemInfo[HandsIndex + 17].Item_coin + " Coin";

                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[HandsIndex + 17].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            else
            {
                Hands[HandsIndex].SetActive(false);
                HandsIndex++;
                Hands[HandsIndex].SetActive(true);
                HandsText.text = _itemInfo[HandsIndex + 17].Item_name;

                if (!_itemInfo[HandsIndex + 17].isBuy)
                {
                    BuyText.text = _itemInfo[HandsIndex + 17].Item_coin + " Coin";
                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[HandsIndex + 17].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            if (HandsIndex == Hands.Length - 1)
            {
                HandsButtons[1].interactable = false;
            }
            else
            {
                HandsButtons[1].interactable = true;
            }

            if (HandsIndex != -1)
            {
                HandsButtons[0].interactable = true;
            }

        }

        else
        {
            if (HandsIndex != -1)
            {
                Hands[HandsIndex].SetActive(false);
                HandsIndex--;


                if (HandsIndex != -1)
                {
                    Hands[HandsIndex].SetActive(true);
                    HandsButtons[0].interactable = true;
                    HandsText.text = _itemInfo[HandsIndex + 17].Item_name;

                    if (!_itemInfo[HandsIndex + 17].isBuy)
                    {
                        BuyText.text = _itemInfo[HandsIndex + 17].Item_coin + " Coin";
                        ButtonManagement[1].interactable = false;
                        if (_data.readData_i("Coins") < _itemInfo[HandsIndex + 17].Item_coin)
                        {
                            ButtonManagement[0].interactable = false;
                        }
                        else
                        {
                            ButtonManagement[0].interactable = true;
                        }
                    }
                    else
                    {
                        BuyText.text = "Owned";
                        ButtonManagement[0].interactable = false;
                        ButtonManagement[1].interactable = true;

                    }
                }
                else
                {
                    HandsButtons[0].interactable = false;
                    HandsText.text = "No Item";

                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                }
            }

            else
            {
                HandsButtons[0].interactable = false;
                HandsText.text = "No Item";

                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
            }
            if (HandsIndex != Hands.Length - 1)
            {
                HandsButtons[1].interactable = true;
            }
        }

    }

    public void Theme_Buttons(string process)
    {   
         Sounds[0].Play();

        if (process == "forward")
        {

            if (ThemeIndex == -1)
            {
                ThemeIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Theme[ThemeIndex];
                _Renderer.materials = mats;

                ThemeText.text = _itemInfo[ThemeIndex + 30].Item_name;

                if (!_itemInfo[ThemeIndex + 30].isBuy)
                {
                    BuyText.text = _itemInfo[ThemeIndex + 30].Item_coin + " Coin";
                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[ThemeIndex + 30].Item_coin)
                    { //+30 index numarasına göre manuel kontrol
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            else
            {

                ThemeIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Theme[ThemeIndex];
                _Renderer.materials = mats;
                ThemeText.text = _itemInfo[ThemeIndex + 30].Item_name;

                if (!_itemInfo[ThemeIndex + 30].isBuy)
                {
                    BuyText.text = _itemInfo[ThemeIndex + 30].Item_coin + " Coin";
                    ButtonManagement[1].interactable = false;
                    if (_data.readData_i("Coins") < _itemInfo[ThemeIndex + 30].Item_coin)
                    {
                        ButtonManagement[0].interactable = false;
                    }
                    else
                    {
                        ButtonManagement[0].interactable = true;
                    }

                }
                else
                {
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                    ButtonManagement[1].interactable = true;

                }
            }
            if (ThemeIndex == Theme.Length - 1)
            {
                ThemeButtons[1].interactable = false;
            }
            else
            {
                ThemeButtons[1].interactable = true;
            }

            if (ThemeIndex != -1)
            {
                ThemeButtons[0].interactable = true;
            }

        }

        else
        {
            if (ThemeIndex != -1)
            {
                ThemeIndex--;


                if (ThemeIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Theme[ThemeIndex];
                    _Renderer.materials = mats;
                    ThemeButtons[0].interactable = true;
                    ThemeText.text = _itemInfo[ThemeIndex + 30].Item_name;

                    if (!_itemInfo[ThemeIndex + 30].isBuy)
                    {
                        BuyText.text = _itemInfo[ThemeIndex + 30].Item_coin + " Coin";
                        ButtonManagement[1].interactable = false;
                        if (_data.readData_i("Coins") < _itemInfo[ThemeIndex + 30].Item_coin)
                        {
                            ButtonManagement[0].interactable = false;
                        }
                        else
                        {
                            ButtonManagement[0].interactable = true;
                        }

                    }
                    else
                    {
                        BuyText.text = "Owned";
                        ButtonManagement[0].interactable = false;
                        ButtonManagement[1].interactable = true;

                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultTheme;
                    _Renderer.materials = mats;
                    ThemeButtons[0].interactable = false;
                    ThemeText.text = "Black";
                    BuyText.text = "Owned";
                    ButtonManagement[0].interactable = false;
                }
            }

            else
            {

                Material[] mats = _Renderer.materials;
                mats[0] = DefaultTheme;
                _Renderer.materials = mats;

                ThemeButtons[0].interactable = false;
                ThemeText.text = "Black";
                BuyText.text = "Owned";
                ButtonManagement[0].interactable = false;
            }
            if (ThemeIndex != Theme.Length - 1)
            {
                ThemeButtons[1].interactable = true;
            }
        }

    }

    public void PanelManager(int Index)
    {   
        Sounds[0].Play();

        CurrentCheck(Index);
        GeneralPanels[0].SetActive(true);

        ActivePanelIndex = Index;
        panelManagement[Index].SetActive(true);

        GeneralPanels[1].SetActive(true);

        MenuCanvas.SetActive(false);

    }

    public void BackMenu()
    {
        Sounds[0].Play();

        GeneralPanels[0].SetActive(false);
        MenuCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        panelManagement[ActivePanelIndex].SetActive(false);
        CurrentCheck(ActivePanelIndex, true);
        ActivePanelIndex = -1;


    }
    


    public void MainMenuTurn()
    {

        Sounds[0].Play();
        CurrentProcessText.SetActive(true);
        _dataManager.Save(_itemInfo);
        StartCoroutine(LoadAsync());
       
        
       



    }
    IEnumerator LoadAsync(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("MainScene");


}
    



}



