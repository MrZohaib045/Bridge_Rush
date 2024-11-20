using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent Enemy;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    public Animator anim;
    public float walkInterval = 3f;
    private float timer;
    public int level_no;
    void Start()
    {
        timer = walkInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SetRandomDestination();
            timer = walkInterval;
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDestination = new Vector3(Random.Range(minBounds.x, maxBounds.x),
                                                transform.position.y,
                                                Random.Range(minBounds.z, maxBounds.z));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDestination, out hit, 1.0f, NavMesh.AllAreas))
        {
            Enemy.SetDestination(hit.position);
            anim.SetBool("Walk",true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((minBounds + maxBounds) / 2, maxBounds - minBounds);
    }
}
