using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsInitializer : MonoBehaviour
{
    [SerializeField] private string BannerId, InterstitialId, admobRewardedVideo;

    public static bool initializeOnce = true;

    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1;

        if (initializeOnce)
        {
            initializeOnce = false;

            AdsManager.Instance.initAdmobBanner(BannerId);

            AdsManager.Instance.initAdmobInterstitial(InterstitialId);

            AdsManager.Instance.RequestRewardBasedVideo(admobRewardedVideo);

        }

    }

  //  public void hideInterstitial()
  //  {
      //  AdsManager.Instance.HideAdmobInterstitial();
  //  }



   // public void requestAdmobRewarded()
  //  {
      //  AdsManager.Instance.RequestRewardBasedVideo(admobRewardedVideo);

   // }



    public void showAdmobRewarded()
    {
        AdsManager.Instance.ShowRewardBasedVideo();

    }



    public void showAdmobBanner()
    {
        AdsManager.Instance.showBanner();
    }
    public void hideAdmobBanner()
    {
        AdsManager.Instance.hideBanner();
    }
    public void showAdmobInterstitial()
    {
        AdsManager.Instance.ShowInterstitial();
    }
    public void ShowChartInter()
    {
        AdsManager.Instance.ShowChartBoostInterstitial();
    }
    public void ShowChartRewarded()
    {
        AdsManager.Instance.ShowChartBoostRewarded();
    }
}
