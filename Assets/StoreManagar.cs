using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManagar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Lock;
    public GameObject[] Levels;

    public void OnEnable()
    {
        for (int j = 0; j < Levels.Length; j++)
        {
            if (PlayerPrefs.GetInt("stars" + j) == 3)
            {
                Levels[j].transform.GetChild(5).gameObject.SetActive(true);
                Levels[j].transform.GetChild(6).gameObject.SetActive(true);
                Levels[j].transform.GetChild(7).gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("stars" + j) == 2)
            {
                Levels[j].transform.GetChild(5).gameObject.SetActive(true);
                Levels[j].transform.GetChild(6).gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("stars" + j) == 1)
            {
                Levels[j].transform.GetChild(5).gameObject.SetActive(true);
            }
        }
    }
}
