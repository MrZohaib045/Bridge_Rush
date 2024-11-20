using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Names : MonoBehaviour
{
    private Vector3 textpos;
    public TextMeshPro textm;

    private void Start()
    {
        textpos = transform.position + new Vector3(0, 3f, -1.3f);
    }
    public void Update()
    {
        textpos = transform.position + new Vector3(0, 3f, -1.3f);
        textm.transform.position = textpos;
    }
    public void SetName(string name)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (textm)
            {
                string userName = PlayerPrefs.GetString("UserName", null);
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "You";
                }
                textm.text = userName;
            }
        }
        else
        {
            if (textm)
            {
                textm.text = name;
            }
        }
    }

    public void set_pos_two()
    {
        textpos = transform.position + new Vector3(0, 2f, -1.3f);

    }
}
