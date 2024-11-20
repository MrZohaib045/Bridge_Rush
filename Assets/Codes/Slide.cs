using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Slide : MonoBehaviour
{
    public Transform pos1;
    private Vector3 original_pos;
    public Transform pos2;

    private void Start()
    {
        original_pos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot"))
        {
            other.transform.position = transform.position;
            other.transform.SetParent(transform);

            MeshRenderer meshrenderer = transform.GetComponent<MeshRenderer>();
            meshrenderer.material.color = other.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;


            // Disable all scripts, NavMeshAgent, and Animator
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            NavMeshAgent navMeshAgent = other.gameObject.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }

            Animator Anim = other.gameObject.GetComponent<Animator>();
            if (Anim != null)
            {
                Anim.enabled = false;
            }

            Invoke("do_move", 0.1f);
            StartCoroutine(EnableScriptsAfterDelayCoroutine(other.gameObject));
        }
    }

    private IEnumerator EnableScriptsAfterDelayCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(2.5f); // Wait for 2.5 seconds before enabling scripts

        // Move the object to pos1
        obj.transform.DOMove(pos2.position, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            obj.transform.SetParent(null);
            enable_scripts(obj);
            if (cameramovement.Instance.after_win == true)
            {
                cameramovement.Instance.CheckAndCorrectPositions();
            }
        });
    }

    public void enable_scripts(GameObject obj)
    {
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }

        NavMeshAgent navMeshAgent = obj.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }

        Animator Anim = obj.GetComponent<Animator>();
        if (Anim != null)
        {
            Anim.enabled = true;
        }
        back_to_original(); // Move the Slide object back to its original position
    }

    public void do_move()
    {
        transform.DOMove(pos1.position, 2f).SetEase(Ease.Linear);
    }

    public void back_to_original()
    {
        transform.DOMove(original_pos, 2f).SetEase(Ease.Linear);
    }
}
