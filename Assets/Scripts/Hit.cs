using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject Enemy;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PLayerController player = collision.gameObject.GetComponent<PLayerController>();
            EnemyController Agent_no = Enemy.gameObject.GetComponent<EnemyController>();
            AgentAi Agent_hit = Enemy.gameObject.GetComponent<AgentAi>();
            if (player != null && Agent_no != null && Agent_hit != null)
            {
                if(Agent_no.level_no == 0)
                {
                    if (Agent_hit.hit == 1)
                    {
                        player.sendallbrikesback();
                        player.Explosion.transform.position = player.transform.position;
                        player.Explosion.Play();
                        player.audio_source.PlayOneShot(player.death_sfx);
                        player.Sendbacktoorignalpos();
                    }
                }
                if (Agent_no.level_no == 1)
                {
                    if (Agent_hit.hit == 1)
                    {
                        player.sendallbrikesback();
                        player.Explosion.transform.position = player.transform.position;
                        player.Explosion.Play();
                        player.audio_source.PlayOneShot(player.death_sfx);
                        player.SendbacktoSeconedpos();
                    }
                }
            }
        }
    }
}
