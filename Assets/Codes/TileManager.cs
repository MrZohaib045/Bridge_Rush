using UnityEngine;
public class TileManager : MonoBehaviour
{
    public GameObject[] Select, Current, Price_tag;
    private int Selected_tile;
    int tile_price;

    private void Start()
    {
        Selected_tile =  PlayerPrefs.GetInt("SelectedTile");
        Selection(Selected_tile);
        print(Selected_tile);
    }

    public void Selection(int num)
    {
        for (int i = 0; i < Select.Length; i++)
        {
            Current[i].SetActive(false);
            Select[i].SetActive(true);
            if (PlayerPrefs.GetInt("PurchaseTile" + i) == 1)
            {
                Price_tag[i].SetActive(false);
            }
        }    
        Select[num].SetActive(false);
        Current[num].SetActive(true);

        Selected_tile = num;
        PlayerPrefs.SetInt("SelectedTile", Selected_tile);
    }

    public void PurchaseTiles(int price)
    {
        tile_price = price;
    }

    public void UnlockBtn(int num)
    {
        if (PlayerPrefs.GetInt("coins") >= tile_price)
        {
            Price_tag[num].SetActive(false);
            Select[num].SetActive(true);
            PlayerPrefs.SetInt("PurchaseTile" + num, 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - tile_price);
        }
    }
}

