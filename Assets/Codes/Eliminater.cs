using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminater : MonoBehaviour
{
    public int totalicangive;
    public int count;
    public List<GameObject> colorgivento;
    public void thischar(GameObject a)
    {
        count++;
        colorgivento.Add(a);
        if (totalicangive == count)
        {
            cameramovement.Instance.eliminater(colorgivento);
        }
    }
}