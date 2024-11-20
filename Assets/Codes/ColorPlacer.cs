using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ColorPlacer : MonoBehaviour
{
    public int color_number;
    public int Player_Tiles;
    public void assigncolor(GameObject player, string name)
    {
        Material temp = player.transform.GetChild(0).transform.GetComponent<SkinnedMeshRenderer>().material;
        List<Transform> activechildrens = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                activechildrens.Add(child);
            }
        }

        // Determine the number of tiles based on the player's tag
        int numberOfTiles = player.CompareTag("Player") ? Player_Tiles : color_number;

        for (int j = 0; j < numberOfTiles; j++)
        {
            if (activechildrens.Count == 0) break; // Exit if there are no more children to process

            int k = Random.Range(0, activechildrens.Count);
            Transform selectedChild = activechildrens[k];
            MeshRenderer m1 = selectedChild.GetComponent<MeshRenderer>();

            if (m1 != null)
            {
                m1.material = temp;
                m1.enabled = true;
            }

            selectedChild.gameObject.name = name; // Set the name directly

            if (name == "bot")
            {
                player.GetComponent<AIcontroller>().MyTargets.Add(selectedChild.gameObject);
            }
            else if (name == "player")
            {
                player.GetComponent<PLayerController>().MyTargets.Add(selectedChild.gameObject);
            }

            selectedChild.parent = null;

            activechildrens.RemoveAt(k);
        }
    }
}
