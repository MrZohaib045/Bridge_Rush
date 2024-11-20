using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class DailyRewards : MonoBehaviour
{
    [Header("UI")]
    private DateTime lastClaimTime;
    public Button giftButton;        // Reference to the gift button in the UI
    public GameObject rewardPanel;  // Reference to the reward panel in the UI
    public GameObject giftAnim;
   // public GameObject get2x;
    public GameObject reward;
    public GameObject coinsAnim;
   // public GameObject openImage;
  //  public GameObject closeImage;
    public GameObject anim;
    public GameObject mainMenuPanel;
  //  public GameObject target;
  //  public GameObject lightEffect;
    public GameObject coins;
  //  public Text getCoins;
    private void Start()
    {
        //float coin = PlayerPrefs.GetFloat("TotalCoins");
        //coins.text = coin.ToString();
        // Load last claim time from PlayerPrefs or set to current time if not found
        if (PlayerPrefs.HasKey("LastClaimTime"))
        {
            long ticks = Convert.ToInt64(PlayerPrefs.GetString("LastClaimTime"));
            lastClaimTime = new DateTime(ticks);
        }
        else
        {
            lastClaimTime = DateTime.Now.AddHours(-25);
            PlayerPrefs.SetString("LastClaimTime", lastClaimTime.Ticks.ToString());
        }

        // Check if enough time has passed to make the button interactable
        CheckInteractable();
        coinsAnim.gameObject.GetComponent<DOTweenAnimation>().DOPause();
    }

    private void CheckInteractable()
    {
        TimeSpan timeSinceLastClaim = DateTime.Now - lastClaimTime;

        if (timeSinceLastClaim.TotalHours >= 24)
        {
            giftButton.interactable = true;          
            giftAnim.gameObject.GetComponent<DOTweenAnimation>().DORestart();
        }
        else
        {
            giftButton.interactable = false;         
            giftAnim.gameObject.GetComponent<DOTweenAnimation>().DOPause();

            // Calculate the time remaining until the button becomes interactable again
            TimeSpan timeRemaining = TimeSpan.FromHours(24) - timeSinceLastClaim;
            Debug.Log("Button will be interactable in: " + timeRemaining.ToString("hh\\:mm\\:ss"));
        }
    }
    public void Gift()
    {
        // Show the reward panel
        rewardPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        Invoke(nameof(Open), 1f);
        giftAnim.gameObject.GetComponent<DOTweenAnimation>().DOPause();       
    }
     void Open()
    {
        StartCoroutine(AnimatedOpen());
    }
    IEnumerator AnimatedOpen()
    {
        lastClaimTime = DateTime.Now;
        PlayerPrefs.SetString("LastClaimTime", lastClaimTime.Ticks.ToString());
        giftButton.interactable = false;
        yield return new WaitForSecondsRealtime(0.5f);
        coinsAnim.gameObject.GetComponent<DOTweenAnimation>().DORestart();
        yield return new WaitForSecondsRealtime(0.5f);
        coins.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        anim.SetActive(true);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 200);
        giftAnim.gameObject.GetComponent<DOTweenAnimation>().DOPause();

        //float current = PlayerPrefs.GetFloat("TotalCoins");
        //int cur = (int)current;
        //float targ = PlayerPrefs.GetFloat("TotalCoins");
        //int tar = (int)targ;
    //    notification.SetActive(false);
     //   open.SetActive(false);
     //   closeImage.GetComponent<DOTweenAnimation>().DORestart();
      //  closeImage.SetActive(false);
     //   Destroy(Instantiate(lightEffect, target.transform.position, Quaternion.identity), 0.3f);
       // get2x.SetActive(true);
       // reward.SetActive(true);
       // yield return new WaitForSecondsRealtime(0.09f);
       // openImage.SetActive(true);
     //   DOTween.To(() => cur, x => cur = x, tar, 1f).OnUpdate(() => coins.text = cur.ToString()).SetEase(Ease.Linear);
       // coins.text = PlayerPrefs.GetFloat("TotalCoins").ToString();
    }
    public void CollectReward()
    { 
        reward.SetActive(false);
    }
    public void BackFromreward()
    {
        rewardPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        //open.SetActive(true);
        //closeImage.SetActive(true);
        //openImage.SetActive(false);
        //anim.SetActive(false);
       // giftAnim.gameObject.GetComponent<DOTweenAnimation>().DORestart();
    }
    //public void Get2x()
    //{
    //    AdsManager.Instance.DailyReward2X();

    //}
    public void get2X()
    {
        float current = PlayerPrefs.GetFloat("TotalCoins");
        int cur = (int)current;
        PlayerPrefs.SetFloat("TotalCoins", PlayerPrefs.GetFloat("TotalCoins") + 600);
        float targ = PlayerPrefs.GetFloat("TotalCoins");
        int tar = (int)targ;
    //    DOTween.To(() => cur, x => cur = x, tar, 1f).OnUpdate(() => coins.text = cur.ToString()).SetEase(Ease.Linear);
        //coins.text = PlayerPrefs.GetFloat("TotalCoins").ToString();
        //getCoins.text = "+600";
        reward.SetActive(false);
    }
}
