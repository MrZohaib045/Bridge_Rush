//using System;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using ChartboostSDK;
//using UnityEngine.UI;
//using DG.Tweening;
//using System.Reflection;

//public class AdsManager : MonoBehaviour
//{
//    string bannerId, interstitialId, rewardedId;
//    private BannerView bannerView;
//    private InterstitialAd interstitial;
//    private RewardedAd rewardedAd;
//    private static AdsManager _instance;
//    [SerializeField] GameObject adsPanel;
//    [SerializeField] DOTweenAnimation noAds, noNet, loadingAd;

//    public static AdsManager Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = GameObject.FindObjectOfType<AdsManager>();
//                DontDestroyOnLoad(_instance.gameObject);
//            }
//            return _instance;
//        }
//    }
//    public void SetIds(string banner_Id, string inters_Id, string reward_Id)
//    {
//        bannerId = banner_Id;
//        interstitialId = inters_Id;
//        rewardedId = reward_Id;
//    }
//    // Start is called before the first frame update
//    // /*
//    public void InitializeSDKandLoadAds()
//    {
//        RequestConfiguration requestConfiguration =
//            new RequestConfiguration.Builder()
//            .SetSameAppKeyEnabled(true).build();
//        MobileAds.SetRequestConfiguration(requestConfiguration);

//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(initStatus => { });
//        // Initialize an InterstitialAd.
//        RequestInterstitial();
//        //Initialize Rewarded
//        RequestRewarded();
//        //Initialize Banner
//        RequestBanner(bannerId);
//    }
//    // */
//    private void Start()
//    {
//        Chartboost.cacheInterstitial(CBLocation.Default);
//        Chartboost.didCompleteRewardedVideo += RewardedComplete;
//        Chartboost.cacheRewardedVideo(CBLocation.Default);
//    }
//    public void ShowChartBoostInterstitial()
//    {
//        if (Chartboost.hasInterstitial(CBLocation.Default))
//            Chartboost.showInterstitial(CBLocation.Default);
//        //  else
//        //  ShowInterstitial();
//    }
//    void RewardedComplete(CBLocation location, int reward)
//    {
//        GiveReward();
//    }
//    public void ShowChartBoostRewarded()
//    {
//        if (Chartboost.hasRewardedVideo(CBLocation.Default))
//            Chartboost.showRewardedVideo(CBLocation.Default);
//    }
//    // /*
//    private void RequestRewarded()
//    {
//        rewardedAd = new RewardedAd(rewardedId);
//        // Called when an ad request has successfully loaded.
//        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//        // Called when an ad request failed to load.
//        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//        // Called when an ad is shown.
//        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//        // Called when an ad request failed to show.
//        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//        // Called when the user should be rewarded for interacting with the ad.
//        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        // Called when the ad is closed.
//        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

//        // Create an empty ad request.
//        AdRequest request = AdRequestBuild();
//        // Load the rewarded ad with the request.
//        this.rewardedAd.LoadAd(request);
//    }

//    private void RequestBanner(string admobBannerId)
//    {
//        AdSize adaptive = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
//        bannerView = new BannerView(admobBannerId, adaptive, AdPosition.Bottom);
//        AdRequest request = AdRequestBuild();
//        bannerView.LoadAd(request);
//        HideAdMobBanner();
//    }

//    public void DestroyBannerAd()
//    {
//        if (bannerView != null)
//            bannerView.Destroy();
//    }
//    public void DestroyInterstitialAd()
//    {
//        if (interstitial != null)
//        {
//            interstitial.Destroy();
//        }
//    }

//    private void RequestInterstitial()
//    {
//        // Initialize an InterstitialAd.
//        this.interstitial = new InterstitialAd(interstitialId);

//        // Called when an ad request has successfully loaded.
//        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//        // Called when an ad request failed to load.
//        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//        // Called when an ad is shown.
//        this.interstitial.OnAdOpening += HandleOnAdOpened;
//        // Called when the ad is closed.
//        this.interstitial.OnAdClosed += HandleOnAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        this.interstitial.LoadAd(request);
//    }
//    public void ShowAdMobInterstitial()
//    {
//        if (this.interstitial.IsLoaded())
//        {
//            this.interstitial.Show();
//        }
//        else
//            RequestInterstitial();
//    }

//    public void ShowAdMobBanner()
//    {
//        bannerView.Show();
//    }
//    public void HideAdMobBanner()
//    {
//        bannerView.Hide();
//    }

//    public void ShowAdmobRewarded()
//    {
//        if (this.rewardedAd.IsLoaded())
//        {
//            this.rewardedAd.Show();
//        }
//        else
//            RequestRewarded();
//    }
//    AdRequest AdRequestBuild()
//    {
//        return new AdRequest.Builder().Build();
//    }

//    //  */
//    //*************************** Handlers **********************************//
//    // /*
//    public void HandleOnAdLoaded(object sender, EventArgs args)
//    {
//        Debug.Log("HandleAdLoaded event received");
//    }
//    public void OnApplicationPause(bool paused)
//    {
//        MobileAds.SetApplicationVolume(paused ? 0f : 0.5f);
//        adsPanel.SetActive(false);
//    }

//    public void HandleOnAdOpened(object sender, EventArgs args)
//    {
//        Debug.Log("HandleAdOpened event received");
//    }

//    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log("Banner failed to load: " + args);
//        // Handle the ad failed to load event.
//    }

//    public void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        Debug.Log("HandleAdClosed event received");
//    }

//    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {
//        Debug.Log("HandleAdLeavingApplication event received");
//    }


//    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log("Native ad failed to load: " + args);
//    }


//    public void HandleRewardedAdLoaded(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardedAdLoaded event received");
//    }

//    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log(
//            "HandleRewardedAdFailedToLoad event received with message: "
//                             + args);
//    }

//    public void HandleRewardedAdOpening(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardedAdOpening event received");
//    }

//    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        Debug.Log(
//            "HandleRewardedAdFailedToShow event received with message: "
//                             + args.Message);
//    }

//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardedAdClosed event received");
//        RequestRewarded();
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        GiveReward();
//    }

//    int Reward_No;
//    public void CoinReward()
//    {
//        Reward_No = 1;
//        ShowPriorityRewarded();
//    }
//    public void CoinRewardX()
//    {
//        Reward_No = 2;
//        ShowPriorityRewarded();
//    }
//    public void UnlockCharacter()
//    {
//        Reward_No = 3;
//        ShowPriorityRewarded();
//    }
//    bool reward_ready = true;
//    void GiveReward()
//    {
//        // Give Reward Here
//        if (reward_ready)
//        {
//            reward_ready = false;

//            switch (Reward_No)
//            {
//                case 1:
//                    ui.instance.CoinRewarded();
//                    break;
//                case 2:
//                    ui.instance.Xrewarded();
//                    break;
//                case 3:
//                    ui.instance.PurchaseOnAd();
//                    break;
//            }
//            Invoke(nameof(ReadyAgainReward), 0.3f);


//        }
//    }
//    void ReadyAgainReward()
//    {
//        reward_ready = true;
//    }

//    public void ShowPriorityInterstitial()
//    {
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//        if (this.interstitial.IsLoaded())
//        {
//            LoadingAdAnim();
//            Invoke(nameof(ShowAdmobInterWithMsg), 1.3f);
//        }
//        else if (Chartboost.hasInterstitial(CBLocation.Default))
//        {
//            LoadingAdAnim();
//            Invoke(nameof(ShowCbInterWithMsg), 1.3f);
//        }
//        if (!this.interstitial.IsLoaded())
//        {
//            RequestInterstitial();
//        }
//        if (!Chartboost.hasInterstitial(CBLocation.Default))
//        {
//            Chartboost.cacheInterstitial(CBLocation.Default);
//        }
//    }
//    void ShowAdmobInterWithMsg()
//    {
//        this.interstitial.Show();
//    }
//    void ShowCbInterWithMsg()
//    {
//        Chartboost.showInterstitial(CBLocation.Default);
//    }
//    public void ShowPriorityRewarded()
//    {
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//        {
//            NoNetAnim();
//            return;
//        }
//        if (this.rewardedAd.IsLoaded())
//        {
//            LoadingAdAnim();
//            Invoke(nameof(ShowAdmobRewardWithMsg), 1.3f);
//        }
//        else if (Chartboost.hasRewardedVideo(CBLocation.Default))
//        {
//            LoadingAdAnim();
//            Invoke(nameof(ShowCbRewardWithMsg), 1.3f);
//        }
//        else
//        {
//            NoAdsAnim();
//        }
//        if (!this.rewardedAd.IsLoaded())
//        {
//            RequestRewarded();
//        }
//        if (!Chartboost.hasRewardedVideo(CBLocation.Default))
//        {
//            Chartboost.cacheRewardedVideo(CBLocation.Default);
//        }
//    }

//    void ShowAdmobRewardWithMsg()
//    {
//        this.rewardedAd.Show();
//    }
//    void ShowCbRewardWithMsg()
//    {
//        Chartboost.showRewardedVideo(CBLocation.Default);
//    }

//    void NoAdsAnim()
//    {
//        adsPanel.SetActive(true);
//        noAds.DORestart();
//    }
//    void NoNetAnim()
//    {
//        adsPanel.SetActive(true);
//        noNet.DORestart();
//    }
//    void LoadingAdAnim()
//    {
//        adsPanel.SetActive(true);
//        loadingAd.DORestart();
//    }

//}
