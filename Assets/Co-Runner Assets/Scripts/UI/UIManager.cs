using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManager;

public class UIManager : MonoBehaviour
{
    
    Data _data =new Data();
    dataManager _dataManager =new dataManager();

    public List<ItemInfo> _itemInfo= new List<ItemInfo>();

    [System.Obsolete]
    void Start()
    {
        
       _data.CheckAndDefine();
        _dataManager.firstSave(_itemInfo);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
