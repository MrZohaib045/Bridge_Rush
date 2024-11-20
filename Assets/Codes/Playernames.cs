using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playernames : MonoBehaviour
{
    public List<GameObject> players;
    public List<string> namesToAssign;
    void Start()
    {
        AssignNamesToPlayers();
    }

    void AssignNamesToPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i < namesToAssign.Count)
            {
                Names player_name = players[i].GetComponent<Names>();
                if (player_name != null)
                {
                    player_name.SetName(namesToAssign[i]);
                }
                else
                {
                    Debug.LogError("Player component not found on " + players[i].name);
                }
            }
            else
            {
                Debug.LogError("Not enough names to assign to all players");
            }
        }
    }
}
