using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelloder : MonoBehaviour
{   
    void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 100);
        }
        Invoke("Load", 2f);
        AdsManager.Instance.showBanner();
    }

    public void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
