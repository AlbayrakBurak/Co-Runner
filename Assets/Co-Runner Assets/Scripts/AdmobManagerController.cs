using System;
using UnityEngine;
using GoogleMobileAds.Api;
using DataManager;
using UnityEngine.UI;


    public class AdmobManagerController : MonoBehaviour
    {   
        [SerializeField] private string _adMobAppIdAndroid = "";
        [SerializeField] private string _adMobAppIdiOS;
        [SerializeField] private string _adMobAppInterstitialAndroid = "";
        [SerializeField] private string _adMobAppInterstitialiOS;
        [SerializeField] private string _adMobAppRewardAndroid = "";
        [SerializeField] private string _adMobAppRewardiOS;

        private RewardedAd _rewardedAd;
        private InterstitialAd _interstitialAd;
        public Button RewardedButton;
        public GameObject Text250;
        public GameObject Text100;

        Data _data = new Data();

        private void Start()
        {
            Text250.SetActive(false);
            MobileAds.Initialize(initStatus => { });
            RequestInterstitialAd();
            RequestRewardedAd();


            if (_data.readData_i("isBuyNoAds") == 0)
            {

                int xcount = UnityEngine.Random.Range(1, 3);
                Debug.Log("Sayı= " + xcount);
                if (xcount >= 2)
                {
                    ShowInterstitialAd();
                    Debug.Log("Gösterime girdi");
                }
            }
            else if (_data.readData_i("isBuyNoAds") == 1)
            {
                Debug.Log("Reklam Kullanılmıyor" + " " + _data.readData_i("isBuyNoAds"));
            }

        }



        private void RequestInterstitialAd()
        {
            // string _adUnitId = GetInterstitialAdUnitId();
            // _interstitialAd = new InterstitialAd(_adUnitId);
            // AdRequest request = new AdRequest.Builder().Build();
            // _interstitialAd.LoadAd(request);
            // _interstitialAd.OnAdClosed += HandleOnAdClosed;
            // Clean up the old ad before loading a new one.
      if (_interstitialAd != null)
      {
            _interstitialAd.Destroy();
            _interstitialAd = null;
      }

      Debug.Log("Loading the interstitial ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest.Builder()
              .Build();

      // send the request to load the ad.
      InterstitialAd.Load(_adMobAppInterstitialAndroid, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              _interstitialAd = ad;
          });
        }

        private void RequestRewardedAd()
        {
            // string _adUnitId = GetRewardedAdUnitId();
            // _rewardedAd = new RewardedAd(_adUnitId);
            // AdRequest request = new AdRequest.Builder().Build();
            // _rewardedAd.LoadAd(request);
            // _rewardedAd.OnUserEarnedReward += HandleOnUserEarnedReward;
            // _rewardedAd.OnAdClosed += HandleOnAdClosed;
            // _rewardedAd.OnAdLoaded += HandleOnAdLoaded;
            // Clean up the old ad before loading a new one.
      if (_rewardedAd != null)
      {
            _rewardedAd.Destroy();
            _rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest.Builder().Build();

      // send the request to load the ad.
      RewardedAd.Load(_adMobAppRewardAndroid, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              _rewardedAd = ad;
          });
        }

        public void ShowInterstitialAd()
        {
            if (_data.readData_i("isBuyNoAds") == 0)
            {
                if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
                else
                {
                    RequestInterstitialAd();
                }
            }
            else
            {
                Debug.Log("Gecis Reklam Gosterilmedi");
            }
        }
        public void ShowRewardedAd()
        {

            // if (_rewardedAd.IsLoaded())
            // {
            //     _rewardedAd.Show();

            //     RewardedButton.interactable = false;
            //     _data.saveData_int("Coins", _data.readData_i("Coins") + 200);
            //     Text250.SetActive(true);
            //     Text100.SetActive(false);
            // }
            const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (_rewardedAd != null && _rewardedAd.CanShowAd())
    {
        _rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
                RewardedButton.interactable = false;
                _data.saveData_int("Coins", _data.readData_i("Coins") + 200);
                Text250.SetActive(true);
                Text100.SetActive(false);
            Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
        });
    }
            else
            {
                RequestRewardedAd();
            }
        }
    

        private void HandleOnUserEarnedReward(object sender, Reward args)
        {
            Debug.Log("Ödül alındı: " + args.Amount + " " + args.Type);
        }

        private void HandleOnAdClosed(object sender, EventArgs args)
        {
            if (sender is InterstitialAd)
            {
                _interstitialAd.Destroy();
                RequestInterstitialAd();
            }
            else if (sender is RewardedAd)
            {
                _rewardedAd = null;
                RequestRewardedAd();
            }
        }

        private void HandleOnAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("Reklam Yüklendi");
        }

        private string GetInterstitialAdUnitId()
        {
            return IsAndroid() ? _adMobAppInterstitialAndroid : _adMobAppInterstitialiOS;
        }

        private string GetRewardedAdUnitId()
        {
            return IsAndroid() ? _adMobAppRewardAndroid : _adMobAppRewardiOS;
        }

        private bool IsAndroid()
        {
#if UNITY_ANDROID
            return true;
#else
        return false;
#endif
        }
    }
