using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class jump : MonoBehaviour
{
    public Transform pos1;
    public AudioSource audio_source;
    public AudioClip Jump_sfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot"))
        {
            //MeshRenderer meshrenderer = transform.GetComponent<MeshRenderer>();
            //meshrenderer.material.color = other.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;
            //Rigidbody rb = other.GetComponent<Rigidbody>();
            NavMeshAgent navMeshAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
                //rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
            }
            Animator Anim = other.gameObject.GetComponent<Animator>();
            if (Anim != null)
            {
                Anim.SetTrigger("Jump");
            }
            // Play jump sound effect only if the collider has the "Player" tag
            if (other.gameObject.CompareTag("Player"))
            {
                audio_source.PlayOneShot(Jump_sfx);
            }
            other.transform.DOJump(pos1.position, 10f, 1, 3f, false).SetEase(Ease.Linear).SetId("JumpTag").OnComplete(() =>
            {
                // Enable NavMeshAgent again after jump is complete
                if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = true;
                }
                if(cameramovement.Instance.after_win == true)
                {
                    cameramovement.Instance.CheckAndCorrectPositions();
                }
            });
        }
    }
}

