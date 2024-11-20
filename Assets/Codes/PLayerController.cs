using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
public class PLayerController : MonoBehaviour
{
    public Animator animator;
    public bool alive;
    public Material MyMaterial;
    public GameObject Bag;
    public GameObject blockinbag;
    public GameObject mytiles;
    public List<GameObject> mytargetparents;
    public List<GameObject> MyTargets;
    public int stage;
    public int exits;
    Rigidbody rb;
    bool move;
    public float moveDuration = 0.3f;
    public Ease moveEase = Ease.OutQuad;

    public float magneticDuration; // Duration of the magnetic power
    private float magneticEndTime = 0f;
    [SerializeField] GameObject magnetField;
    private bool isMagnetic = false;
    public float speed;
    public GameObject bag2;

    private List<Transform> childrenToMove = new List<Transform>();
    private float lastMoveTime = 0f;
    private const float moveDelay = 0.12f;
    public Vector3 Origanl_pos;

    public AudioClip Pick_up;
    public AudioClip magnet_sfx;
    public AudioClip brick_sfx;
    public AudioClip fast_sfx;
    public AudioClip slow_sfx;
    public AudioClip death_sfx;
    public AudioClip hiteneme_sfx;
    //public AudioClip death_sfx;


    public AudioSource audio_source;

    public Transform pos_2;
    public Transform pos_3;
    private float latest_speed;

    public GameObject spawnpos1, spawnpos2, spawnpos3;
    public int pos1, pos2;

    public ParticleSystem Explosion;


    void Start()
    {
        Origanl_pos = transform.position;
        MyMaterial = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        Bag = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        alive = true;
        exits = 0;
        stage = 0;
        keepbricks();
        move = true;
        bag2 = transform.GetChild(5).gameObject;
        latest_speed = speed;
    }
    void Update()
    {
        if (move)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                animator.SetBool("Walk", true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                rb.constraints |= RigidbodyConstraints.FreezePositionZ;
                animator.SetBool("Walk", false);
            }
        }
        if (Time.time >= magneticEndTime)
        {
            isMagnetic = false;
            magnetField.SetActive(false);
        }

        //Vector3 targetPosition = new Vector3(Bag.transform.position.x, Bag.transform.position.y + ((Bag.transform.childCount - 1) * 0.4f), Bag.transform.position.z);

        // Check if we need to initiate the moving process
        if (bag2.transform.childCount > 0 && childrenToMove.Count == 0)
        {
            foreach (Transform child in bag2.transform)
            {
                childrenToMove.Add(child);
            }
        }
        // Process the children queue
        if (childrenToMove.Count > 0)
        {
            if (Time.time - lastMoveTime >= moveDelay)
            {
                Transform child = childrenToMove[0];
                childrenToMove.RemoveAt(0);
                child.SetParent(Bag.transform);
                child.localPosition = new Vector3(0, Bag.transform.childCount * 0.4f, 0);
                child.localRotation = Quaternion.identity;
                lastMoveTime = Time.time; // Update the last move time
            }
        }

        if (transform.position.z > pos1 && transform.position.z < pos2)
        {
            if (spawnpos2 != null)
                spawnpos2.SetActive(true);

            if (spawnpos1 != null)
                spawnpos1.SetActive(false);
        }
        if (transform.position.z > pos2)
        {
            if (spawnpos3 != null)
                spawnpos3.SetActive(true);

            if (spawnpos2 != null)
                spawnpos2.SetActive(false);
        }

        //if (bag2.transform.childCount > 0)
        //{
        //    StartCoroutine(MoveChildrenFromBag2ToBagCoroutine());
        //}

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && move)
        {
            if (alive)
            {
                Vector3 direction = Vector3.forward * Input.GetAxis("Mouse Y") + Vector3.right * Input.GetAxis("Mouse X");
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 8.5f * Time.deltaTime);
                }
            }
        }
    }

    void ActivateMagneticPower()
    {
        isMagnetic = true;
        magneticEndTime = Time.time + magneticDuration;
        magnetField.SetActive(true);
    }

    //private IEnumerator MoveChildrenFromBag2ToBagCoroutine()
    //{
    //    List<Transform> children = new List<Transform>();

    //    foreach (Transform child in bag2.transform)
    //    {
    //        children.Add(child);
    //    }

    //    foreach (Transform child in children)
    //    {
    //        child.SetParent(null);

    //        // Add a slight delay to ensure sequential processing
    //        yield return new WaitForSeconds(0.2f);

    //        child.SetParent(Bag.transform);
    //        child.localPosition = new Vector3(0, Bag.transform.childCount * 0.4f, 0); // Adjusting the position
    //        child.localRotation = Quaternion.identity;

    //        yield return new WaitForSeconds(0.2f); // Small delay between setting each child
    //    }
    //}

    public void MoveTileToBag(GameObject cube)
    {
        if (cube != null && cube.GetComponent<MeshRenderer>().material.color == MyMaterial.color && cube.transform.parent != Bag)
        {
            cube.transform.DOMove(bag2.transform.position, moveDuration).SetEase(moveEase).OnComplete(() =>
            {
                cube.transform.localPosition = new Vector3(bag2.transform.localPosition.x, bag2.transform.localPosition.y, Bag.transform.localPosition.z);
                cube.transform.SetParent(bag2.transform);
                cube.transform.localRotation = Quaternion.identity;
            });
        }
    }

    public void sendallbrikesback()
    {
        int tempSize = Bag.transform.childCount;
        // Iterate backward to avoid index issues when destroying children
        for (int i = tempSize - 1; i >= 0; i--)
        {
            GameObject child = Bag.transform.GetChild(i).gameObject;

            if (child.tag == "cude")
            {
                child.GetComponent<AddMaterials>().BackToFirstPosition();
            }
            else if (child.tag == "Clones")
            {
                Destroy(child);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            other.gameObject.tag = "Untagged";
            other.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
            stage++;
            if (stage <= 2)
            {
                keepbricks();
            }
        }
        if (other.gameObject.tag == "0th")
        {
            //gameObject.transform.GetChild(4).GetComponent<BoxCollider>().enabled = false;
            exits++;
            if (exits == 2)
            {
                exits = 0;
                //transform.GetChild(3).GetComponent<stepmaker>().buildagain();
            }
        }
    }

    public void Bricksback(int number)
    {
        int tempsize = number;
        for (int i = 0; i < tempsize; i++)
        {
            int currentChildCount = Bag.transform.childCount;
            if (currentChildCount == 0)
            {
                break; // No more children to process
            }

            GameObject child = Bag.transform.GetChild(currentChildCount - 1).gameObject;
            if (child.tag == "cude")
            {
                child.GetComponent<AddMaterials>().BackToFirstPosition();
            }
            else if (child.tag == "Clones")
            {
                Destroy(child);
            }
        }
    }

    public void Multiply_Tiles()
    {
        int tempsize = Bag.transform.childCount;
        GameObject temp = Bag.transform.GetChild(0).gameObject;
        for (int i = 0; i < tempsize; i++)
        {
            GameObject clone = Instantiate(temp);
            clone.transform.SetParent(bag2.transform);
            clone.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z);
            //clone.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.4f), Bag.transform.localPosition.z);
            //clone.transform.localRotation = Quaternion.identity;
            clone.tag = "Clones";
        }
    }

    public void addbricks(int number)
    {
        GameObject temp = Bag.transform.GetChild(0).gameObject;
        int tempsize = number;
        for (int i = 0; i < tempsize; i++)
        {
            GameObject clone = Instantiate(temp);
            clone.transform.SetParent(bag2.transform);
            clone.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z);
            //clone.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.4f), Bag.transform.localPosition.z);
            //clone.transform.localRotation = Quaternion.identity;
            clone.tag = "Clones";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cude"))
        {
            if (move)
            {
                //if (collision.gameObject.GetComponent<MeshRenderer>().material == MyMaterial)
                if (other.gameObject.GetComponent<MeshRenderer>().material.color == MyMaterial.color && other.gameObject.name == "player")
                {
                    GameObject a = other.gameObject;
                    if (PlayerPrefs.GetInt("sounds") == 0)
                    {
                        audio_source.PlayOneShot(Pick_up);
                    }
                    a.transform.DOMove(bag2.transform.position, moveDuration).SetEase(moveEase).OnComplete(() =>
                    {
                        a.transform.SetParent(bag2.transform);
                        //a.transform.localPosition = new Vector3(0, Bag.transform.childCount * 0.4f, 0); // Adjusting the position
                        //a.transform.localRotation = Quaternion.identity;
                    });
                }
            }
        }

        if (other.CompareTag("Magnet"))
        {
            if (PlayerPrefs.GetInt("sounds") == 0)
            {
                audio_source.PlayOneShot(magnet_sfx);
            }
            ActivateMagneticPower();
            Destroy(other.gameObject);
        }
        //if (other.gameObject.CompareTag("Stairs"))
        //{
        //    EC.enabled = true;
        //}

        if (other.gameObject.CompareTag("5Brick"))
        {
            Counter Brick_no = other.gameObject.GetComponent<Counter>();
            if (Brick_no != null)
            {
                Bricksback(Brick_no.Brick_number);
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "nth")
        {
            other.transform.root.GetComponent<Eliminater>().thischar(gameObject);
            //gameObject.transform.GetChild(4).GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Sendbacktoorignalpos()
    {
        transform.position = Origanl_pos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SendbacktoSeconedpos()
    {
        transform.position = pos_2.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SendbacktoThirdpos()
    {
        transform.position = pos_3.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            Invoke("Enable_script", 1.3f);
            if (Bag.transform.childCount != 0 && exits == 0)
            {
                if (collision.gameObject.transform.GetComponent<AIcontroller>().Bag.transform.childCount > Bag.transform.childCount)
                {
                    //rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
                    if (PlayerPrefs.GetInt("sounds") == 0) 
                    {
                        audio_source.PlayOneShot(hiteneme_sfx);
                    }
                    Instantiate_tile();
                    StartCoroutine(stand());
                    Vibration.Vibrate(100);
                }
            }
        }

        if (collision.gameObject.CompareTag("AddBrick"))
        {
            Counter Brick_no = collision.gameObject.GetComponent<Counter>();
            if (Brick_no != null && Bag.transform.childCount > 0)
            {
                if (PlayerPrefs.GetInt("sounds") == 0)
                {
                    audio_source.PlayOneShot(brick_sfx);
                }
                addbricks(Brick_no.Brick_number);
                Destroy(collision.gameObject);
            }
        }

        //if (collision.gameObject.CompareTag("Death"))
        //{
        //    Explosion.transform.position = collision.transform.position;
        //    Explosion.Play();
        //    sendallbrikesback();
        //    if (collision.gameObject.GetComponent<Deathpos>().death_val == 0)
        //    {
        //        Invoke("Sendbacktoorignalpos", 0.2f);
        //    }
        //    if (collision.gameObject.GetComponent<Deathpos>().death_val == 1)
        //    {
        //        Invoke("SendbacktoSeconedpos", 0.2f);
        //    }
        //    if (collision.gameObject.GetComponent<Deathpos>().death_val == 2)
        //    {
        //        Invoke("SendbacktoThirdpos", 0.2f);
        //    }
        //    print("You are Dead");

        //}

        if (collision.gameObject.CompareTag("Death"))
        {
            Explosion.transform.position = collision.transform.position;
            Explosion.Play();
            sendallbrikesback();
            if (PlayerPrefs.GetInt("sounds") == 0)
            {
                audio_source.PlayOneShot(death_sfx);
            }
            int deathVal = collision.gameObject.GetComponent<Deathpos>().death_val;

            switch (deathVal)
            {
                case 0:
                    Invoke("Sendbacktoorignalpos", 0.2f);
                    break;
                case 1:
                    Invoke("SendbacktoSeconedpos", 0.2f);
                    break;
                case 2:
                    Invoke("SendbacktoThirdpos", 0.2f);
                    break;
            }

            print("You are Dead");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Clone_Tile"))
        {
            if (Bag.transform.childCount > 0)
            {
                GameObject temp = Bag.transform.GetChild(0).gameObject;
                GameObject clone = Instantiate(temp);
                clone.transform.SetParent(bag2.transform);
                clone.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z);
                clone.tag = "Clones";
                if (PlayerPrefs.GetInt("sounds") == 0)
                {
                    audio_source.PlayOneShot(Pick_up);
                }
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Slow"))
        {
            if (PlayerPrefs.GetInt("sounds") == 0)
            {
                audio_source.PlayOneShot(slow_sfx);
            }
            speed = 3;
            Invoke("reset_speed", 5f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Fast"))
        {
            if (PlayerPrefs.GetInt("sounds") == 0)
            {
                audio_source.PlayOneShot(fast_sfx);
            }
            speed = 8;
            Invoke("reset_speed", 3);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("AddReward"))
        {
            if (Bag.transform.childCount > 0)
            {
                Multiply_Tiles();
                if (PlayerPrefs.GetInt("sounds") == 0)
                {
                    audio_source.PlayOneShot(brick_sfx);
                }
                Destroy(collision.gameObject);
            }
        }
    }
    void reset_speed()
    {
        speed = latest_speed;
    }
    public void Instantiate_tile()
    {
        GameObject Clone_Obj = Bag.transform.GetChild(0).gameObject;
        if (Clone_Obj != null)
        {
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
    }
    public void keepbricks()
    {
        mytiles = mytargetparents[stage].gameObject;
        mytargetparents[stage].GetComponent<ColorPlacer>().assigncolor(gameObject, "player");
    }
    IEnumerator stand()
    {
        animator.SetTrigger("Death1");
        GetComponent<PLayerController>().enabled = false;
        move = false;
        GetComponent<Collider>().isTrigger = true;
        sendallbrikesback();
        yield return new WaitForSeconds(0.6f);
        //GetComponent<PLayerController>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        move = true;
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public void Enable_script()
    {
        GetComponent<PLayerController>().enabled = true;
    }


}