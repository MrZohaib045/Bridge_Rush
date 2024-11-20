using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameManager : MonoBehaviour
{

    public void Load()
    {
        Invoke("Load_next_scene", 2f);
    }
    public void Load_next_scene()
    {
        SceneManager.LoadScene(1);

    }
}
