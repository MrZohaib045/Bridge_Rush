using UnityEngine;
using GoogleMobileAds.Api;
using System;
using ChartboostSDK;
public class AdsManager : MonoBehaviour
{
    // Start is called before the first frame update

    BannerView bannerView;
    InterstitialAd interstitial;
    private bool showAdmobInsterstitial = false;
    private RewardBasedVideoAd rewardBasedVideo;
    private static AdsManager _instance;

    public static AdsManager Instance
    {

        get
        {

            if (_instance == null)
            {
                //#if !UNITY_EDITOR
                _instance = GameObject.FindObjectOfType<AdsManager>();
                DontDestroyOnLoad(_instance.gameObject);
                //#endif
            }
            return _instance;

        }



    }
    void Awake()
    {


        //#if !UNITY_EDITOR

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            if (this != _instance)
                Destroy(this.gameObject);
        }

        //	#endif



    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) != 1)
        {
            Chartboost.cacheInterstitial(CBLocation.Default);
        }
        Chartboost.didCompleteRewardedVideo += RewardedComplete;
        Chartboost.cacheRewardedVideo(CBLocation.Default);

    }
    public void ShowChartBoostInterstitial()
    {
        //if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
        //    return;
        if (Chartboost.hasInterstitial(CBLocation.Default))
            Chartboost.showInterstitial(CBLocation.Default);
    }
    void RewardedComplete(CBLocation location, int reward)
    {
        GiveReward();
    }
    public void ShowChartBoostRewarded()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.Default))
            Chartboost.showRewardedVideo(CBLocation.Default);
    }
    public void initAdmobBanner(string admobID)
    {
#if UNITY_ANDROID
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;

        bannerView = new BannerView(admobID, AdSize.Banner, AdPosition.Bottom);//AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        hideBanner();
#endif

    }
    public void showBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;
#if UNITY_ANDROID
        bannerView.Show();
#endif
    }
    public void hideBanner()
    {
#if UNITY_ANDROID
        bannerView.Hide();
#endif
    }
    public void initAdmobInterstitial(string interstitialID)
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;
#if UNITY_ANDROID

        interstitial = new InterstitialAd(interstitialID);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        interstitial.OnAdClosed += admobClosed;
        interstitial.OnAdLoaded += delegate (object sender, EventArgs args)
        {
            if (showAdmobInsterstitial)
            {
                showAdmobInsterstitial = false;
                interstitial.Show();
            }
        };
#endif
    }
    void admobClosed(object sender, EventArgs args)
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;
#if UNITY_ANDROID
        //print("interstitial closed.");
        requestInterstitial();
#endif
        // Handle the ad loaded event.
    }
    void requestInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;
#if UNITY_ANDROID
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
#endif
    }
    public void ShowPriorityInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;
        if (Chartboost.hasInterstitial(CBLocation.Default))
        {
            showAdmobInsterstitial = true;
            ShowChartBoostInterstitial();
            requestInterstitial();
        }
        else
            interstitial.Show();
    }
    public void ShowInterstitial()
    {
        //    if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
        //        return;
#if UNITY_ANDROID

        if (interstitial.IsLoaded() == false)
        {
            showAdmobInsterstitial = true;

            requestInterstitial();
        }
        else
            interstitial.Show();

#endif
    }
    string rewardedVideoID;
    public void RequestRewardBasedVideo(string rewardedID)
    {

#if UNITY_ANDROID

        rewardedVideoID = rewardedID;
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.LoadAd(createAdRequest(), rewardedID);

#endif
    }
    private AdRequest createAdRequest()
    {
#if UNITY_ANDROID
        return new AdRequest.Builder().Build();
#endif
    }
    public void ShowRewardBasedVideo()
    {
#if UNITY_ANDROID
        if (Chartboost.hasRewardedVideo(CBLocation.Default))
        {
            ShowChartBoostRewarded();
        }
        else
        {
            rewardBasedVideo.Show();

        }
#endif
    }
    int Reward_No;
    public void CoinReward()
    {
        Reward_No = 1;
        ShowRewardBasedVideo();
    }
    public void CoinRewardX()
    {
        Reward_No = 2;
        ShowRewardBasedVideo();
    }
    bool reward_ready = true;
    void GiveReward()
    {
        // Give Reward Here
        if (reward_ready)
        {
            reward_ready = false;

            switch (Reward_No)
            {
                case 1:
                    FindObjectOfType<ui>().CoinRewarded();
                    FindObjectOfType<ui>().Afterwatch();
                    break;
                case 2:
                    FindObjectOfType<ui>().Xrewarded();
                    FindObjectOfType<ui>().Afterwatch();
                    break;
            }
            Invoke("ReadyAgainReward", 0.3f);


        }
    }
    void ReadyAgainReward()
    {
        reward_ready = true;
    }
    //*************************************************In App String***************************************************************//
    public void RemoveAdsPurchased()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }
    //************************************************* event Handling *************************************************************



    //***************************** admob rewarded events *****************************************************************************
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoLoaded event received.");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoClosed event received");
        RequestRewardBasedVideo(rewardedVideoID);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        GiveReward();

        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " +
                  type);
        PlayerPrefs.SetString("GetReward", "true");
        PlayerPrefs.Save();

    }
}
