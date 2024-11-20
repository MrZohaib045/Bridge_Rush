using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaterials : MonoBehaviour
{
    public Vector3 FirstPosition;
    public GameObject paarent;
    void Start()
    {
        FirstPosition = transform.position;
    }
    public void BackToFirstPosition()
    {
        transform.position = FirstPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent = null;
    }    
}
