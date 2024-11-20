using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEndcolor : MonoBehaviour
{
    public GameObject[] objects; // Array to store GameObjects
    private MeshRenderer[] meshRenderers; // Array to store MeshRenderers

    public void Start()
    {
        // Initialize the MeshRenderer array with the same size as the GameObjects array
        meshRenderers = new MeshRenderer[objects.Length];

        // Loop through each GameObject and get its MeshRenderer component
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null) // Check if the object is not null
            {
                meshRenderers[i] = objects[i].GetComponent<MeshRenderer>();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot"))
        {
            // Get the material color of the colliding object
            Material temp = other.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;

            // Loop through each MeshRenderer and change its color
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                if (meshRenderers[i] != null) // Check if the MeshRenderer is not null
                {
                    meshRenderers[i].material.color = temp.color;
                }
            }
        }
    }
}
