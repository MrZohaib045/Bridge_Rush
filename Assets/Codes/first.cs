using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first : MonoBehaviour
{
    public Animator anim;
    public GameObject explosion_particle;
    public GameObject hide;
    public static first instance;

    public int counter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot"))
        {
            counter = 1;
            anim = other.GetComponent<Animator>();
            anim.SetTrigger("Dance1");
            hide.SetActive(false);
            explosion_particle.SetActive(true);
        }
    }

}
