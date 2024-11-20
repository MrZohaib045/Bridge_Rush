using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.AI;
using DG.Tweening;

public class cameramovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public List<GameObject> allplayers;
    public static cameramovement Instance;
    public bool aa;
    public GameObject firstst, secondnd, third;
    public GameObject finalposofcamera;
    public bool ranks;
    public List<GameObject> rankpurpose;
    public GameObject level1, level2, level3;
    public int low, temp;
    public GameObject gamelost;
    public GameObject won;
    public int levelno;
    public Transform winningPoint; // Assign in the Inspector to your winning point (like a finish line)
    public Transform firstPosition, secondPosition, thirdPosition; // Winning positions

    public bool after_win;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        levelno = SceneManager.GetActiveScene().buildIndex - 1;
        aa = true;
        offset = transform.position - player.transform.position;
        ranks = false;
        won.SetActive(false);
        low = -1;
        PlayerPrefs.SetInt("ShowInter", 0);
        after_win = false;
    }

    void FixedUpdate()
    {
        if (player.GetComponent<PLayerController>() != null && player.GetComponent<PLayerController>().Bag != null && aa)
        {
            Vector3 targetPosition;
            int childCount = player.GetComponent<PLayerController>().Bag.transform.childCount;

            if (childCount > 70)
            {
                targetPosition = player.transform.position + offset * 1.7f;
            }
            else if (childCount > 50)
            {
                targetPosition = player.transform.position + offset * 1.45f; // Adjusted to be different for clarity
            }
            else if (childCount > 30)
            {
                targetPosition = player.transform.position + offset * 1.2f;
            }
            else
            {
                targetPosition = player.transform.position + offset;
            }

            transform.position = targetPosition;
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2.0f);
        }
        if (ranks)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalposofcamera.transform.position, 150 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(25, 0, 0);
            if (transform.position == finalposofcamera.transform.position)
            {
                ranks = false;
            }
        }
    }

    //public void Update()
    //{
    //    SortPlayersByDistanceToWinningPoint();
    //    if(after_win  == true)
    //    {
    //        CheckAndCorrectPositions();
    //    }
    //}

    void SortPlayersByDistanceToWinningPoint()
    {
        allplayers = allplayers.OrderBy(player => Vector3.Distance(player.transform.position, winningPoint.position)).ToList();
    }

    public void eliminater(List<GameObject> arethere)
    {
        // Sort players by distance to the winning point
        SortPlayersByDistanceToWinningPoint();
        after_win = true;
        if (arethere.Count == 1)
        {
            firstst = allplayers[0]; // Closest player
            secondnd = allplayers[1]; // Second closest
            third = allplayers[2]; // Third closest

            // Check if any of the top 3 players is the player controlled by the user
            if (firstst.tag == "Player" || secondnd.tag == "Player" || third.tag == "Player")
            {
                Invoke(nameof(Complete), 0.1f);
                //ShowAd();
                if (firstst.tag == "Player")
                {
                    ui.instance.LevelCompleteCoins(20);
                    //print("20 coins do");
                }
                else if (secondnd.tag == "Player")
                {
                    ui.instance.LevelCompleteCoins(15);
                    //print("15 coins doo");
                }
                else if (third.tag == "Player")
                {
                    ui.instance.LevelCompleteCoins(10);
                    //print("10 coins doo");
                }
            }
            else
            {
                Invoke(nameof(LevelFail), 0.5f);
            }

            // Move the top 3 players to their winning positions
            declarewin(firstst, secondnd, third);
        }
        else
        {
            int tempcount = allplayers.Count;
            for (int i = 0; i < tempcount; i++)
            {
                if (!arethere.Contains(allplayers[i]))
                {
                    GameObject a = allplayers[i];
                    if (a.transform.tag == "Player")
                    {
                        aa = false;
                        Invoke(nameof(LevelFail), 0.5f);
                        ui.instance.CurrentLvlNum.gameObject.SetActive(false);
                        ui.instance.PauseBtn.gameObject.SetActive(false);
                        if (PlayerPrefs.GetInt("sounds") == 0)
                        {
                            gamelost.GetComponent<AudioSource>().Play();
                        }
                    }
                    else
                    {
                        a.SetActive(false);
                    }
                }
            }
        }
    }

    public void LevelFail()
    {
        if (gamelost.activeInHierarchy)
        {
            return;
        }
        gamelost.SetActive(true);
        ui.instance.LevelFailCoin();
        AdsManager.Instance.showBanner();
    }

    public void Complete()
    {
        ui.instance.CurrentLvlNum.gameObject.SetActive(false);
        ui.instance.PauseBtn.gameObject.SetActive(false);
        aa = false;
        ranks = true;
        won.SetActive(true);
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            won.GetComponent<AudioSource>().Play();
        }
        AdsManager.Instance.showBanner();
    }

    public void declarewin(GameObject first, GameObject second, GameObject third)
    {
        StartCoroutine(MoveToPosition(first, firstPosition.position, 0.2f));
        StartCoroutine(MoveToPosition(second, secondPosition.position, 0.35f));
        StartCoroutine(MoveToPosition(third, thirdPosition.position, 0.5f));

        // Set all other players inactive
        foreach (GameObject player in allplayers)
        {
            if (player != first && player != second && player != third)
            {
                player.SetActive(false);
            }
        }
    }

    IEnumerator MoveToPosition(GameObject player, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = player.transform.position;
        float elapsed = 0;
        while (elapsed < duration)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }

            if (player.CompareTag("Player"))
            {
                PLayerController playercontoller = player.GetComponent<PLayerController>();
                if (playercontoller != null)
                {
                    playercontoller.Bag.SetActive(false);
                    playercontoller.enabled = false;
                }
            }
            if (player.CompareTag("Bot"))
            {
                AIcontroller aicontroller = player.GetComponent<AIcontroller>();
                NavMeshAgent aiagent = player.GetComponent<NavMeshAgent>();
                if (aicontroller != null && aiagent != null)
                {
                    aiagent.enabled = false;
                    aicontroller.Bag.SetActive(false);
                    aicontroller.enabled = false;
                }
            }

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            player.transform.DOMove(targetPosition, duration).SetEase(Ease.InOutQuad);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition; // Ensure the player reaches the target
        //Debug.Log($"{player.name} is moving to position {targetPosition}");
        // Rotate player to face 180 degrees on the Y-axis
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckAndCorrectPositions()
    {
        float threshold = 0.1f;
        if (Vector3.Distance(firstst.transform.position, firstPosition.position) > threshold)
        {
            StartCoroutine(MoveToPosition(firstst, firstPosition.position, 0.2f));
        }
        if (Vector3.Distance(secondnd.transform.position, secondPosition.position) > threshold)
        {
            StartCoroutine(MoveToPosition(secondnd, secondPosition.position, 0.2f));
        }
        if (Vector3.Distance(third.transform.position, thirdPosition.position) > threshold)
        {
            StartCoroutine(MoveToPosition(third, thirdPosition.position, 0.2f));
        }
    }
}
