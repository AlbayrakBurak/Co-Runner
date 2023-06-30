using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using DataManager;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Store_Manager : MonoBehaviour, IStoreListener
{

   
    public Text coinText;


    private static IStoreController s_StoreController;
    private static IExtensionProvider s_ExtensionProvider;

    private static string Coins_1000 = "coin1000";
    private static string Coins_5000 = "coin5000";

    private static string Coins_10000 = "coin10000";

    private static string Coins_50000 = "coin50000";

    private static string NoAds="noads";

     Data _data = new Data();



    void Start()
    {
        coinText.text = _data.readData_i("Coins").ToString();

        if (s_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (isInitialize())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Coins_1000, ProductType.Consumable);
        builder.AddProduct(Coins_5000, ProductType.Consumable);

        builder.AddProduct(Coins_10000, ProductType.Consumable);

        builder.AddProduct(Coins_50000, ProductType.Consumable);
        builder.AddProduct(NoAds, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void ProductBuy_1000()
    {
        BuyProductID(Coins_1000);
        coinText.text = _data.readData_i("Coins").ToString();
    }
    public void ProductBuy_5000()
    {
        BuyProductID(Coins_5000);
        coinText.text = _data.readData_i("Coins").ToString();
    }
    public void ProductBuy_10000()
    {
        BuyProductID(Coins_10000);
        coinText.text = _data.readData_i("Coins").ToString();
    }
    public void ProductBuy_50000()
    {
        BuyProductID(Coins_50000);
        coinText.text = _data.readData_i("Coins").ToString();
    }
    public void ProductBuy_NoAds(){
        BuyProductID(NoAds);
    }


    void BuyProductID(string productId)
    {
        if (isInitialize())
        {
            Product product = s_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                s_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Error Buy");
            }
        }
        else
        {
            Debug.Log("Not find product");
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        s_StoreController = controller;
        s_ExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_1000, StringComparison.Ordinal))
        {

            _data.saveData_int("Coins", _data.readData_i("Coins") + 1000);

        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_5000, StringComparison.Ordinal))
        {

            _data.saveData_int("Coins", _data.readData_i("Coins") + 5000);

        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_10000, StringComparison.Ordinal))
        {

            _data.saveData_int("Coins", _data.readData_i("Coins") + 10000);

        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_50000, StringComparison.Ordinal))
        {

            _data.saveData_int("Coins", _data.readData_i("Coins") + 50000);

        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, NoAds, StringComparison.Ordinal))
        {

            _data.saveData_int("isBuyNoAds",1);

        }
        return PurchaseProcessingResult.Complete;
    }

    private bool isInitialize()
    {
        return s_StoreController != null && s_ExtensionProvider != null;
    }


    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
