using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinReward : MonoBehaviour
{
    public Animator anim;
    public Text coinsText;
    public GameObject rewards, needle,noThanks,home;
    bool stop = false;
    bool coincollected = false;
    void Start()
    {
        anim = anim.GetComponent<Animator>();
        anim.SetBool("Stop", true);        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
       int otherObject = int.Parse(other.gameObject.name);
       other.transform.name = otherObject.ToString(); 
        if(otherObject == 2)
        {
            int completeCoinValue = int.Parse(ui.instance.CompleteCoinTxt.text);
            // Multiply the value by 2
            int multipliedCoins = completeCoinValue * 2;
            //print(multipliedCoins);
            // Set the multiplied value to the `coinsText`
            coinsText.text = multipliedCoins.ToString();
        }
        if (otherObject == 3)
        {
            int completeCoinValue = int.Parse(ui.instance.CompleteCoinTxt.text);
            // Multiply the value by 2
            int multipliedCoins = completeCoinValue * 3;
            //print(multipliedCoins);
            // Set the multiplied value to the `coinsText`
            coinsText.text = multipliedCoins.ToString();
        }
        if (otherObject == 5)
        {
            int completeCoinValue = int.Parse(ui.instance.CompleteCoinTxt.text);
            // Multiply the value by 2
            int multipliedCoins = completeCoinValue * 5;
            //print(multipliedCoins);
            // Set the multiplied value to the `coinsText`
            coinsText.text = multipliedCoins.ToString();
        }

        if (otherObject == 2 && stop == true)
       {
            if (coincollected == false)
            {
              //  ui.instance.LevelCompleteCoins()
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 40);
                coincollected = true;
            }
       }
      else if (otherObject == 3 && stop == true)
      {
            if (coincollected == false)
            {
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 60);
                coincollected = true;
            }       
      }
       else if (otherObject == 5 && stop == true)
       {
            if(coincollected == false)
            {
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 100);
                coincollected = true;
            }        
       }
    }
    public void Stop()
    {      
        stop = true;
        anim.enabled = false;
       // noThanks.SetActive(false);
        home.SetActive(true);
    }
    public void NoThanks()
    {
        if(stop == false)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 20);
        }       
        rewards.SetActive(false);
        needle.SetActive(false);
    }
}
