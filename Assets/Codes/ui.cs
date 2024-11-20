using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ui : MonoBehaviour
{
    public Text ChCoinText;
    public cameramovement CamMov;
    public int no;
    public GameObject[] chara;
    public GameObject[] select;
    public GameObject[] selected;
    public GameObject[] pricetag;
    public GameObject player;
    public GameObject[] bots;
    public Text mainmenucoins;
    public Text LvlNumCom;
    public Text LvlNumFail;
    public Text CurrentLvlNum;
    public Text watchVideoText;
    public Text watchVideoText3;
    // public Text priceText;
    bool videoText = false;
    public bool music;
    public bool sound;
    public bool vibration;
    public GameObject musicon;
    public GameObject musicoff;
    public GameObject soundon;
    public GameObject soundoff;
    public GameObject vibrationOn;
    public GameObject vibrationOff;
    public GameObject shop;
    public GameObject complete;
    public GameObject mainmenu;
    public GameObject ShopPanel;
    //public GameObject levelSelction;
    //public Text CompeleteCoins;
    public GameObject RewardMeter;
    public GameObject videoButton;
    public GameObject no_thanksBtn;
    public GameObject ContinueBtn;
    public GameObject RetryBtn;
    // public GameObject NeedleOrigin;
    public GameObject LevelCompleteShop, FailCoins, PauseBtn, PauseMenu, playBtn, CharacterPanal, homeBtn;
    public Text CompleteCoinTxt, FailCoinstxt;
    public int Rewardx;
    int levelindex, chPrice;
    public static ui instance;

    public Text Obj_text;
    public InputField Display;


    public void Awake()
    {
        instance = this;
        player = chara[PlayerPrefs.GetInt("selection")];
        chara[PlayerPrefs.GetInt("selection")].SetActive(true);
        CamMov.player = chara[PlayerPrefs.GetInt("selection")];
        CamMov.allplayers[0] = chara[PlayerPrefs.GetInt("selection")];
    }
    public void Start()
    {
        print(PlayerPrefs.GetInt("sounds"));
        print(PlayerPrefs.GetInt("vibrate"));
        print(PlayerPrefs.GetInt("music"));

        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            soundoff.SetActive(false);
            soundon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            soundoff.SetActive(true);
            soundon.SetActive(false);
        }
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicoff.SetActive(false);
            musicon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("music") == 1)
        {
            musicoff.SetActive(true);
            musicon.SetActive(false);
        }
        if (PlayerPrefs.GetInt("vibrate") == 0)
        {
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("vibrate") == 1)
        {
            vibrationOff.SetActive(true);
            vibrationOn.SetActive(false);
        }


        Time.timeScale = 1;
        PlayerPrefs.SetInt("purchase" + 0, 1);
        Obj_text.text = PlayerPrefs.GetString("UserName");
        Selection(PlayerPrefs.GetInt("selection"));
        //PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 0);
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 5000);
        }
        CurrentLvlNum.gameObject.SetActive(false);
        PauseBtn.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("FTR") == 1)
        {
            PlayerPrefs.SetInt("FTR", 0);
            mainmenu.SetActive(false);
            StartGame();
            AdsManager.Instance.hideBanner();
        }
        else
        {
            // mainmenu.SetActive(true);
            AdsManager.Instance.hideBanner();
        }
        
        levelindex = PlayerPrefs.GetInt("level");
        //LvlNumCom.text = levelindex.ToString();
        //LvlNumFail.text = levelindex.ToString();
        CurrentLvlNum.text = "LEVEL " + (levelindex).ToString();
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        shop.GetComponent<Button>().interactable = true;
        
    }
    public void create()
    {
        //Obj_text.text = Display.text;
        PlayerPrefs.SetString("UserName", Obj_text.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void activatemainMenu()
    {
        mainmenu.gameObject.SetActive(true);
    }
    #region store

    public void chnum(int num)
    {
        no = num;
    }
    public void purchase(int price)
    {
        if (PlayerPrefs.GetInt("coins") >= price)
        {
            select[no].SetActive(true);
            pricetag[no].SetActive(false);

            PlayerPrefs.SetInt("purchase" + no, 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - price);
        }
    }

    public void Lock(string price)
    {
        playBtn.SetActive(false);
        // pricetag.SetActive(true);
        //priceText.text=price;
        chPrice = int.Parse(price);
    }
    public void Selection(int sel)
    {
        //print("here it is");
        for (int i = 0; i < select.Length; i++)
        {
            selected[i].SetActive(false);
            chara[i].SetActive(false);
            if (PlayerPrefs.GetInt("purchase" + i) == 1)
            {
                select[i].SetActive(true);
                pricetag[i].SetActive(false);
            }
        }
        //  select[sel].SetActive(false);
        selected[sel].SetActive(true);
        chara[sel].SetActive(true);
        select[sel].SetActive(false);

        player = chara[sel];
        CamMov.player = chara[sel];
        CamMov.allplayers[0] = chara[sel];
        PlayerPrefs.SetInt("selection", sel);
    }
    #endregion store
    public void SelectLevel(int levelno)
    {
        PlayerPrefs.SetInt("FTR", 1);

        SceneManager.LoadSceneAsync(levelno);

        AfterSelection();

    }
    void AfterSelection()
    {
        // levelSelction.gameObject.SetActive(false);
        StartGame();
    }
    public void load_next()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        if (PlayerPrefs.GetInt("level") > 29)
        {
            int i = Random.Range(10, 29);
            SceneManager.LoadSceneAsync(i);
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void nextlevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        StartGame();
        PlayBtn();
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        if (PlayerPrefs.GetInt("level") > 17)
        {
            int i = Random.Range(10, 17);
            SceneManager.LoadSceneAsync(i);
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainmenu.SetActive(false);
        StartGame();
    }
    public void shopoff()
    {
        shop.GetComponent<Button>().interactable = false;
    }
    public void reload()
    {
        PlayerPrefs.SetInt("FTR", 1);
        restartlevel();
    }
    public void RestartGame()
    {
        mainmenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }
    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 0;
        activatemainMenu();
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(false);
        CurrentLvlNum.gameObject.SetActive(false);
        AdsManager.Instance.hideBanner();
    }
    public void PlayButtonMusic()
    {
        if (PlayerPrefs.GetInt("music") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    public void MusicOnOff()
    {
        music = !music;
        //print(music);
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicoff.SetActive(false);
            musicon.SetActive(true);
            PlayerPrefs.SetInt("music", 1);
            AudioManager.instance.Pause();
        }
        else
        {
            musicoff.SetActive(true);
            musicon.SetActive(false);
            PlayerPrefs.SetInt("music", 0);
            AudioManager.instance.Play();
        }
    }
    public void VibrateOn()
    {
        PlayerPrefs.SetInt("vibrate", 0);
        vibrationOff.SetActive(false);
        vibrationOn.SetActive(true);
    }
    public void VibrateOff()
    {
        PlayerPrefs.SetInt("vibrate", 1);
        vibrationOff.SetActive(true);
        vibrationOn.SetActive(false);
        Vibration.Vibrate(100);
    }
    public void SoundOnOff()
    {
        sound = !sound;
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            soundoff.SetActive(true);
            soundon.SetActive(false);
            //print(0);
            PlayerPrefs.SetInt("sounds", 1);
            //print(true);
        }
        else
        {
            soundoff.SetActive(false);
            soundon.SetActive(true);
            //print(1);
            PlayerPrefs.SetInt("sounds", 0);
            //print(false);
        }
    }
    public void PlayButtonSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("ads") == 0)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                AdsManager.Instance.showBanner();
            }
        }
    }
    public void HideBanner()
    {
        AdsManager.Instance.hideBanner();
    }
    public void ShowCoinReward()
    {
        AdsManager.Instance.CoinReward();
    }
    public void CoinRewarded()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 20);
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void StartGame()
    {
        CurrentLvlNum.gameObject.SetActive(true);
        PauseBtn.gameObject.SetActive(true);
        mainmenu.gameObject.SetActive(false);
        player.GetComponent<PLayerController>().enabled = true;
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].GetComponent<AIcontroller>().enabled = true;
        }
        //AdsManager.Instance.hideBanner();
    }
    public void PlayBtn()
    {
        mainmenu.SetActive(false);
        StartGame();
        int sceneNumber = PlayerPrefs.GetInt("level");
        print(sceneNumber);
        if (sceneNumber > 29)
            sceneNumber = Random.Range(10, 29);
        PlayerPrefs.SetInt("FTR", 1);
        SceneManager.LoadScene(sceneNumber);
    }
    public void PauseGame()
    {
        PauseBtn.gameObject.SetActive(false);
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        //AdsManager.Instance.showBanner();
    }
    public void Resume()
    {
        PlayButtonSound();
        PauseBtn.gameObject.SetActive(true);
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        //AdsManager.Instance.hideBanner();
    }
    public void XReward()
    {
        AdsManager.Instance.CoinRewardX();
    }
    public void Xrewarded()
    {
        Rewardx = 100;
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + Rewardx);
        //print("2x");
        mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        //print(PlayerPrefs.GetInt("coins"));
    }
    public void LevelCompleteCoins(int coins)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coins);
        LevelCompleteShop.gameObject.SetActive(true);
        CompleteCoinTxt.text = coins.ToString();

        //CompeleteCoins.text = PlayerPrefs.GetInt("coins").ToString();
        //print(PlayerPrefs.GetInt("coins"));
        //mainmenucoins.gameObject.SetActive(false);
    }
    public void LevelFailCoin()
    {
        FailCoins.gameObject.SetActive(true);
        FailCoinstxt.text = "0".ToString();
    }
    //public void Removeads()
    //{
    //    AdsManager.Instance.inAppDeals(0);
    //}
    //public void Deal1()
    //{
    //    AdsManager.Instance.inAppDeals(1);
    //}
    //public void Deal2()
    //{
    //    AdsManager.Instance.inAppDeals(2);
    //}
    //public void Deal3()
    //{
    //    AdsManager.Instance.inAppDeals(3);
    //}
    //public void Deal4()
    //{
    //    AdsManager.Instance.inAppDeals(4);
    //}
    public void unlockall()
    {
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("purchase" + i, 1);
        }
    }
    public void fCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 500);
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void thousandcoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1000);
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void Fifteen()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1500);
        // mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void SelectionBack()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    for (int i = 0; i < bots.Length; i++)
        //    {
        //        bots[i].GetComponent<AIcontroller>().enabled = true;
        //    }
        //}       
    }
    public void Shop()
    {
        ShopPanel.SetActive(true);
        mainmenu.SetActive(false);
    }
    public void BackFromShop()
    {
        ShopPanel.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void WatchVideo2()
    {
        PlayerPrefs.SetInt("VideoText2", PlayerPrefs.GetInt("VideoText2") + 1);
        UpdateText2();
    }
    void UpdateText2()
    {
        int videoText2 = PlayerPrefs.GetInt("VideoText2");
        int divider = 2;
        if (videoText2 >= divider)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 50);
            PlayerPrefs.SetInt("VideoText2", 0);
        }
        watchVideoText.text = videoText2 + "/" + divider;
    }
    public void WatchVideo3()
    {
        PlayerPrefs.SetInt("VideoText3", PlayerPrefs.GetInt("VideoText3") + 1);
        UpdateText3();
    }
    void UpdateText3()
    {
        int videoText3 = PlayerPrefs.GetInt("VideoText3");
        int divider = 3;
        if (videoText3 >= divider)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 100);
            PlayerPrefs.SetInt("VideoText3", 0);
        }
        watchVideoText3.text = videoText3 + "/" + divider;
    }
    public void Afterwatch()
    {
        videoButton.gameObject.SetActive(false);
        no_thanksBtn.gameObject.SetActive(false);
        ContinueBtn.gameObject.SetActive(true);
        RetryBtn.gameObject.SetActive(true);
        homeBtn.SetActive(true);
    }
    public void Rate_Us()
    {
        Application.OpenURL("amzn://apps/android?p=" + Application.identifier);
    }
    public void OnShare()
    {
        SocialNetworks.ShareURL("Stack blast & Ball Helix Tiles Jump Platform ", "amzn://apps/android?p=" + Application.identifier);
    }
}