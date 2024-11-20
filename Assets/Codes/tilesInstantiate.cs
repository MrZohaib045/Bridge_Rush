using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesInstantiate : MonoBehaviour
{

    public GameObject tilePrefab; // Prefab jo instantiate karna hai
    public GameObject parentObject; // Parent object jahan tile instantiate hoga

    void Start()
    {
        if (tilePrefab != null && parentObject != null)
        {
            GameObject newTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);
            newTile.transform.SetParent(parentObject.transform);
        }
        else
        {
            Debug.LogError("TilePrefab or ParentObject is not assigned!");
        }
    }
}
