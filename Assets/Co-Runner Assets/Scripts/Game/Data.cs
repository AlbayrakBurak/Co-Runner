using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;

namespace DataManager
{
    public class Data
    {

        public void saveData_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        public void saveData_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public void saveData_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string readData_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int readData_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float readData_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void CheckAndDefine()
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemsData.gd"))
            {
                PlayerPrefs.SetInt("ActiveHead", -1);
                PlayerPrefs.SetInt("ActiveHands", -1);
                PlayerPrefs.SetInt("ActiveGlasses", -1);
                PlayerPrefs.SetInt("ActiveTheme", -1);
                PlayerPrefs.SetInt("isBuyNoAds", 0);
                PlayerPrefs.SetInt("Coins", 100);
                SceneManager.LoadScene(0);
            }
        }


    }
    [Serializable]
    public class ItemInfo
    {
        public int GroupIndex;
        public int Item_Index;
        public string Item_name;
        public int Item_coin;
        public bool isBuy;
    }
    public class dataManager
    {
        public void Save(List<ItemInfo> _itemInfo)
        {
 BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemsData.gd");
            bf.Serialize(file, _itemInfo);
            file.Close();

        }
        List<ItemInfo> _itemList;


        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemsData.gd"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemsData.gd", FileMode.Open);
                _itemList = (List<ItemInfo>)bf.Deserialize(file);
                file.Close();

                // Debug.Log(myData.Coin);

            }
            else
            {
                Debug.Log("Kayıt bulunamadı.");
            }
        }
        public List<ItemInfo> returnList()
        {
            return _itemList;

        }

        public void firstSave(List<ItemInfo> _itemInfo)
        {

            if (!File.Exists(Application.persistentDataPath + "/ItemsData.gd"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemsData.gd");
                bf.Serialize(file, _itemInfo);
                file.Close();
            }

        }

    }
}

