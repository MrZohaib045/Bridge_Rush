using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ameramovement.Instance.ShowAd();
        //print("Inters");
        AdsManager.Instance.ShowPriorityInterstitial();

    }
}
