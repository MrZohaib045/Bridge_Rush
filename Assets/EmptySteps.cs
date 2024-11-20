using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySteps : MonoBehaviour
{
    public bool isTileAvailable = false;
    public int counter = 0;
    //PLayerController player;

    public void Start()
    {
        counter = 0;
    }

    public void SetNewTile(GameObject newTile , int number , PLayerController player)
    {
        if (isTileAvailable)
            return;
        isTileAvailable = true;
        GameObject newTileGo = Instantiate(newTile, transform);
        newTileGo.transform.localScale = Vector3.one;
        newTileGo.transform.localPosition = Vector3.zero;
        newTileGo.GetComponent<BoxCollider>().enabled = false;
        player.Bag.transform.GetChild(number).GetComponent<AddMaterials>().BackToFirstPosition();
    }
}
