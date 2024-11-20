using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcontroller : MonoBehaviour
{
    bool collection, placeing, movetoplace, backdown;
    private int carethismany, havethismany;
    public GameObject othstep, nthstep;
    public GameObject mytiles;
    public List<GameObject> mytargetparents;
    public List<GameObject> MyTargets;
    public List<GameObject> mystairs;
    public List<GameObject> staircase;
    public GameObject Thisstaircase;
    public GameObject TargetTile;
    public Material MyMaterial;
    public Animator animator;
    public GameObject Bag;
    private int total, count;
    public int stage;
    public float speed;
    public int exits;
    public bool move;
    public float moveDuration = 0.3f;
    public Ease moveEase = Ease.OutQuad;

    public Vector3 last_pos;
    private bool isCheckingPosition = false;

    public AudioClip hiteneme_sfx;
    public AudioSource audio_source;

    private void Awake()
    {
        MyMaterial = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
    }
    void Start()
    {
        last_pos = transform.position;
        havethismany = total = count = stage = 0;
        move = true;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        Bag = transform.GetChild(2).gameObject;
        Takethismany();
        addstairscases();
        collection = true;
        GoToTile();
        exits = 0;
        SetStairCaseList();
    }
    void SetStairCaseList()
    {
        for (int i = 0; i < mystairs[0].transform.childCount; i++)
        {
            staircase.Add(mystairs[0].transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if (move)
        {
            if (collection)
            {
                if (TargetTile != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetTile.transform.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, TargetTile.transform.position) < 0.2f)
                    {
                        GetComponent<Collider>().isTrigger = false;
                    }
                }
                else
                {
                    // Handle the case where TargetTile is null
                    GoToTile();
                }
            }
            if (placeing)
            {
                if (othstep != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, othstep.transform.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, othstep.transform.position) < 0.15f)
                    {
                        placeing = false;
                        movetoplace = true;
                        if (nthstep != null)
                        {
                            transform.LookAt(nthstep.transform.position);
                        }
                    }
                }
            }
            if (movetoplace)
            {
                if (nthstep != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nthstep.transform.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, nthstep.transform.position) < 0.25f)
                    {
                        nthstep.transform.root.GetComponent<Eliminater>().thischar(gameObject);
                        movetoplace = false;
                        stage++;
                        Takethismany();
                        addstairscases();
                        GoToTile();
                        GetComponent<Collider>().isTrigger = false;
                        exits = 0;
                    }
                }
            }
            if (backdown)
            {
                if (othstep != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, othstep.transform.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, othstep.transform.position) < 0.25f)
                    {
                        backdown = false;
                        Takethismany();
                        GoToTile();
                        collection = true;
                    }
                }
            }
        }

        last_pos = transform.position;

        Vector3 current_pos = transform.position;

        if (current_pos == last_pos && !isCheckingPosition)
        {
            StartCoroutine(CheckIfStuck());
        }
        // Update the last position for the next frame
        last_pos = current_pos;
    }

    IEnumerator CheckIfStuck()
    {
        isCheckingPosition = true;
        Vector3 startPosition = transform.position;

        // Wait for 1 second
        yield return new WaitForSeconds(0.2f);

        if (transform.position == startPosition)
        {
            StartCoroutine(reset_collider());
        }

        isCheckingPosition = false;
    }
    IEnumerator reset_collider()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Player"))
        {
            if (!movetoplace)
            {
                if (Bag.transform.childCount > 0)
                {
                    if (collision.gameObject.CompareTag("Bot"))
                    {
                        if (collision.gameObject.transform.GetComponent<AIcontroller>().Bag.transform.childCount > Bag.transform.childCount)
                        {
                            Instantiate_tile();
                            StartCoroutine(stand());
                        }
                    }
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        if (collision.gameObject.transform.GetComponent<PLayerController>().Bag.transform.childCount > Bag.transform.childCount)
                        {
                            if (PlayerPrefs.GetInt("sounds") == 0)
                            {
                                audio_source.PlayOneShot(hiteneme_sfx);
                            }
                            Instantiate_tile();
                            StartCoroutine(stand());
                        }
                    }
                }
            }
        }

        if (collision.gameObject.CompareTag("Clone_Tile"))
        {
            if (Bag.transform.childCount > 0)
            {
                Destroy(collision.gameObject);
                GameObject Clone_Obj = Bag.transform.GetChild(0).gameObject;
                Instantiate(Clone_Obj);
                Clone_Obj.transform.SetParent(Bag.transform);
                Clone_Obj.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.25f), Bag.transform.localPosition.z);
                Clone_Obj.transform.localRotation = Quaternion.identity;
                Clone_Obj.name = "Theak";
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cude"))
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material.color == MyMaterial.color)
            {
                GameObject a = other.gameObject;
                //a.transform.parent = Bag.transform;
                //a.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.25f), Bag.transform.localPosition.z);
                //a.transform.localRotation = Quaternion.Euler(0, 0, 0);

                a.transform.DOMove(Bag.transform.position, moveDuration).SetEase(moveEase).OnComplete(() =>
                {
                    // Once the item has reached the bag, make it a child of the bag
                    a.transform.SetParent(Bag.transform);
                    a.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.4f), Bag.transform.localPosition.z);
                    a.transform.localRotation = Quaternion.identity;
                });
                MyTargets.Remove(other.gameObject);
                if (!placeing)
                {
                    havethismany++;
                    if (carethismany == havethismany)
                    {
                        ChooseACase();
                    }
                    else
                    {
                        GoToTile();
                    }
                }
            }
        }
    }
    public void Instantiate_tile()
    {
        GameObject Clone_Obj = Bag.transform.GetChild(0).gameObject;
        for (int i = 0; i < Bag.transform.childCount; i++)
        {
            GameObject clone = Instantiate(Clone_Obj.gameObject);
            clone.transform.localScale = new Vector3(Clone_Obj.transform.localScale.x / 1.7f, Clone_Obj.transform.localScale.y / 1.7f, Clone_Obj.transform.localScale.x / 1.7f);
            clone.transform.position = Bag.transform.position + Vector3.up * 2f;

            Rigidbody cloneRb = clone.AddComponent<Rigidbody>();
            cloneRb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            cloneRb.mass = 0.5f;
            cloneRb.drag = 1f;

            clone.GetComponent<BoxCollider>().isTrigger = false;

            MeshRenderer meshRenderer = clone.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material.color = Color.grey;
            }
            clone.tag = "Clone_Tile";
        }
    }

    public void Takethismany()
    {
        havethismany = 0;
        carethismany = Random.Range(8, 20);
    }
    IEnumerator stand()
    {
        GetComponent<Collider>().isTrigger = true;
        animator.SetTrigger("Death2");
        move = false;
        sendallbrikesback();
        yield return new WaitForSeconds(2f);
        move = true;
        animator.SetBool("Walk", true);
        GetComponent<Collider>().isTrigger = false;
    }
    public void sendallbrikesback()
    {
        int tempsize = Bag.transform.childCount;
        GameObject go = Bag.transform.GetChild(0).gameObject;
        for (int i = 0; i < tempsize; i++)
        {
            MyTargets.Add(Bag.transform.GetChild(0).gameObject);
            Bag.transform.GetChild(0).GetComponent<AddMaterials>().BackToFirstPosition();
        }
    }
    public void ChooseACase()
    {
        total = 0;
        count = 0;
        for (int i = 0; i < staircase.Count; i++)
        {
            //print("staircase[i] : " + i);
            count = 0;
            GameObject k = staircase[i].transform.GetChild(1).gameObject;
            //print(k.name);
            for (int j = 0; j < k.transform.childCount; j++)
            {
                MeshRenderer thism = k.transform.GetChild(j).GetComponent<MeshRenderer>();
                if (thism.enabled)
                {
                    if (thism.material.color == MyMaterial.color)
                    {
                        count++;
                        if (count > total)
                        {
                            total = count;
                            Thisstaircase = staircase[i];
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        if (total == 0)
        {
            Thisstaircase = staircase[Random.Range(0, staircase.Count)];
            oandnth();
        }
        else
        {
            oandnth();
        }
        transform.LookAt(othstep.transform.position);
    }
    public void stepsover()
    {
        movetoplace = collection = placeing = false;
        backdown = true;
        transform.LookAt(othstep.transform.position);
    }
    public void GoToTile()
    {
        float low = 100f;
        GetComponent<Collider>().isTrigger = false;
        for (int i = 0; i < MyTargets.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, MyTargets[i].transform.position);
            if (low > distance)
            {
                low = distance;
                TargetTile = MyTargets[i];
            }
            //print("MyTargets :: " + i);
        }
        transform.LookAt(TargetTile.transform);
    }
    public void oandnth()
    {
        transform.LookAt(Thisstaircase.transform.position);
        othstep = Thisstaircase.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        nthstep = Thisstaircase.transform.GetChild(1).GetChild((Thisstaircase.transform.GetChild(1).childCount) - 1).GetChild(0).gameObject;
        collection = false;
        placeing = true;
    }
    public void addstairscases()
    {
        if (stage < mystairs.Count)
        {
            staircase.Clear();
            MyTargets.Clear();
            //print(stage);
            mytiles = mytargetparents[stage].gameObject;
            mytargetparents[stage].GetComponent<ColorPlacer>().assigncolor(gameObject, "bot");
            collection = true;
            //print("StairsCleared");
            UpdateStairsCase();
        }
    }

    void UpdateStairsCase()
    {
        //print("UpdateStairsCase");
        if (stage == 1)
        {
            //staircase.Add(mystairs[1].transform.GetChild(0).gameObject);
            //staircase.Add(mystairs[1].transform.GetChild(1).gameObject);
            //staircase.Add(mystairs[1].transform.GetChild(2).gameObject);
            for (int i = 0; i < mystairs[1].transform.childCount; i++)
            {
                staircase.Add(mystairs[1].transform.GetChild(i).gameObject);
            }

        }
        else if (stage == 2)
        {
            staircase.Add(mystairs[2].transform.GetChild(0).gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "0th")
        {
            exits++;
            if (exits == 1)
            {
                GetComponent<Collider>().isTrigger = true;
            }
            if (exits == 2)
            {
                GetComponent<Collider>().isTrigger = false;
                exits = 0;
            }
        }

    }
}
