using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentAi : MonoBehaviour
{
    private GameObject player;
    public NavMeshAgent enemyAgent;
    public float attackRange = 5f;
    public float attackCooldown = 1.5f;
    //public GameObject hitEffect;
    public Animator anim;
    private bool canAttack = true;
    private bool isInRange = false;
    public int hit;

    void Start()
    {
        hit = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in players)
        {
            if (obj.activeSelf)
            {
                player = obj;
                break;
            }
        }
    }


    void Update()
    {
        if (player == null || enemyAgent == null)
            return;

        float distance = Vector3.Distance(player.transform.position, enemyAgent.transform.position);
        if (distance <= attackRange)
        {
            isInRange = true;
            enemyAgent.SetDestination(player.transform.position);
        }
        else
        {
            isInRange = false;
        }
        // print(distance);
        if (isInRange && canAttack && distance <= 2)
        {
            PlayHitAnimation();
            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }
    }

    void PlayHitAnimation()
    {
        hit = 1;
        anim.SetTrigger("Hit");
    }

    void ResetAttackCooldown()
    {
        hit = 0;
        canAttack = true;
    }


}
