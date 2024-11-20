using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tileschange : MonoBehaviour
{
    public GameObject[] Tiles1;
    public GameObject[] Tiles2;
    public GameObject[] Tiles3;
    public GameObject[] Tiles4;
    public GameObject[] Tiles5;
    public GameObject[] Tiles6;
    public GameObject[] Tiles7;
    public GameObject[] Tiles8;
    public GameObject[] Tiles9;
    public GameObject[] Tiles10;
    public GameObject[] Tiles11;
    public GameObject[] Tiles12;

    public int SelectedModel = 0;
    public void Start()
    {
        SelectedModel = PlayerPrefs.GetInt("SelectModel", 1);
        DeactivateAllModels();
        ActivateModel(SelectedModel);
    }
    private void DeactivateAllModels()
    {
        DeactivateGameObjects(Tiles1);
        DeactivateGameObjects(Tiles2);
        DeactivateGameObjects(Tiles3);
        DeactivateGameObjects(Tiles4);
        DeactivateGameObjects(Tiles5);
        DeactivateGameObjects(Tiles6);
        DeactivateGameObjects(Tiles7);
        DeactivateGameObjects(Tiles8);
        DeactivateGameObjects(Tiles9);
        DeactivateGameObjects(Tiles10);
        DeactivateGameObjects(Tiles11);
        DeactivateGameObjects(Tiles12);
    }
    private void DeactivateGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
    private void ActivateGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(true);
        }
    }
    private void ActivateModel(int modelIndex)
    {
        switch (modelIndex)
        {
            case 1: ActivateGameObjects(Tiles1); break;
            case 2: ActivateGameObjects(Tiles2); break;
            case 3: ActivateGameObjects(Tiles3); break;
            case 4: ActivateGameObjects(Tiles4); break;
            case 5: ActivateGameObjects(Tiles5); break;
            case 6: ActivateGameObjects(Tiles6); break;
            case 7: ActivateGameObjects(Tiles7); break;
            case 8: ActivateGameObjects(Tiles8); break;
            case 9: ActivateGameObjects(Tiles9); break;
            case 10: ActivateGameObjects(Tiles10); break;
            case 11: ActivateGameObjects(Tiles11); break;
            case 12: ActivateGameObjects(Tiles12); break;
            default: break;
        }
    }
    public void Active_Tiles1()
    {
        SelectedModel = 1;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();
        DeactivateAllModels();
        ActivateGameObjects(Tiles1);

        print("Tile1");
    }
    public void Active_Tiles2()
    {
        SelectedModel = 2;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles2);
        print("Tile2");

    }
    public void Active_Tiles3()
    {
        SelectedModel = 3;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles3);
        print("Tile3");
    }
    public void Active_Tiles4()
    {
        SelectedModel = 4;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles4);
        print("Tile4");
    }
    public void Active_Tiles5()
    {
        SelectedModel = 5;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles5);
        print("Tile5");
    }
    public void Active_Tiles6()
    {
        SelectedModel = 6;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles6);
        print("Tile6");
    }
    public void Active_Tiles7()
    {
        SelectedModel = 7;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles7);
        print("Tile7");
    }
    public void Active_Tiles8()
    {
        SelectedModel = 8;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles8);
        print("Tile8");
    }
    public void Active_Tiles9()
    {
        SelectedModel = 9;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles9);
        print("Tile9");
    }
    public void Active_Tiles10()
    {
        SelectedModel = 10;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles10);
        print("Tile10");
    }
    public void Active_Tiles11()
    {
        SelectedModel = 11;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles11);
        print("Tile11");
    }
    public void Active_Tiles12()
    {
        SelectedModel = 12;
        PlayerPrefs.SetInt("SelectModel", SelectedModel);
        PlayerPrefs.Save();

        DeactivateAllModels();
        ActivateGameObjects(Tiles12);
        print("Tile12");
    }
}
