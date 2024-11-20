using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sconed : MonoBehaviour
{
    public Animator anim;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot"))
        {
            anim = other.GetComponent<Animator>();
            anim.SetTrigger("Dance2");

            other.GetComponent<Names>().set_pos_two();
        }
    }
}
