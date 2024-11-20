using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepmaker : MonoBehaviour
{
    public AIcontroller AIcontroller;
    public PLayerController pLayer;
    [SerializeField]
    Material player;
    public GameObject bag;
    public string name;
    //public List<Collider> c1;
    RaycastHit hit;
    public AudioClip Set_step;
    public AudioSource audio_source;
    void Start()
    {
        AIcontroller = transform.parent.GetComponent<AIcontroller>();
        player = transform.parent.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        bag = transform.parent.transform.GetChild(2).gameObject; 
    }
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 5, Color.yellow);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
        {
            if (hit.collider != null)
            { 
                if (hit.collider.tag == "EmptyStep")
                {
                    GameObject a = hit.collider.gameObject;

                    MeshRenderer meshRenderer = a.GetComponent<MeshRenderer>();
                    if (bag.transform.childCount > 0 && (!meshRenderer.enabled || meshRenderer.material.color != player.color))
                    {
                        int size = bag.transform.childCount - 1;
                        if (name == "bot")
                        {
                            a.GetComponent<EmptySteps>().counter = 0;
                            meshRenderer.enabled = true;
                            meshRenderer.material.color = player.color;
                            GameObject back = bag.transform.GetChild(size).transform.gameObject;
                            if (a.transform.GetComponentInChildren<AddMaterials>())
                            {
                                // GameObject playersTile = a.transform.GetComponentInChildren<AddMaterials>().gameObject;
                                Destroy(a.transform.GetComponentInChildren<AddMaterials>().gameObject); 
                            }
                            if (back.GetComponent<AddMaterials>().paarent == AIcontroller.mytiles)
                            {
                                AIcontroller.MyTargets.Add(back);
                            }
                            if(back.tag == "cude")
                            {
                                a.GetComponent<EmptySteps>().isTileAvailable = false;
                                bag.transform.GetChild(size).GetComponent<AddMaterials>().BackToFirstPosition();
                            }
                            else
                            {
                                Destroy(back);
                            }
                        }
                        else
                        {
                            if (Input.GetMouseButton(0))
                            {
                                meshRenderer.enabled = true;
                                //if (!transform.GetComponentInChildren<AddMaterials>() && a.GetComponents<BoxCollider>()[0].enabled && size >= 0)
                                //if (a.GetComponents<BoxCollider>()[0].enabled && size >= 0)
                                if (size >= 0)
                                {
                                    meshRenderer.material.color = pLayer.Bag.transform.GetChild(size).gameObject.GetComponent<MeshRenderer>().material.color;
                                    a.GetComponents<BoxCollider>()[0].enabled = false;
                                    GameObject tile = pLayer.Bag.transform.GetChild(size).gameObject;
                                    if( tile.tag == "cude")
                                    {
                                        if (a.GetComponent<EmptySteps>().isTileAvailable == false)
                                            a.GetComponent<EmptySteps>().SetNewTile(tile, size, pLayer);
                                    }
                                    else
                                    {
                                        Destroy(tile);
                                    }
                                    //tile.transform.localScale = Vector3.one;
                                    //tile.transform.localPosition = Vector3.zero;
                                    //tile.GetComponent<BoxCollider>().enabled = false;
                                    //pLayer.Bag.transform.GetChild(size).GetComponent<AddMaterials>().BackToFirstPosition();

                                }
                            }
                        }
                    }

                    if (name != "bot")
                    {
                        if (Input.GetMouseButton(0) && bag.transform.childCount > 0 && a.GetComponent<EmptySteps>().counter == 0)
                        {
                            audio_source.PlayOneShot(Set_step);
                            a.GetComponent<EmptySteps>().counter = 1;
                            if (meshRenderer.material.color == player.color)
                            {
                                a.GetComponents<BoxCollider>()[0].enabled = false;
                            }
                        }
                        if (bag.transform.childCount == 0 && (meshRenderer.material.color != player.color || meshRenderer.material.color == player.color))
                        {
                            //if (!c1.Contains(a.GetComponents<BoxCollider>()[0]))
                            //{
                            //    c1.Add(a.GetComponents<BoxCollider>()[0]);
                            //}
                        }
                    }
                    if (bag.transform.childCount == 0)
                    {
                        if (name == "bot")
                        {
                            AIcontroller.stepsover();
                        }
                    }
                }
            }
        }
    }


    //public void buildagain()
    //{
    //    Debug.Log("buildagain");
    //    for (int i = 0; i < c1.Count; i++)
    //    {
    //        c1[i].enabled = true;
    //    }
    //    c1.Clear();
    //}



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "EmptyStep")
    //    {
    //        //GameObject a = hit.collider.gameObject;
    //        MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
    //        print("HEllo");
    //        if (bag.transform.childCount > 0 && !meshRenderer.enabled && (meshRenderer.material != player))
    //        {
    //            meshRenderer.enabled = true;
    //            int size = bag.transform.childCount - 1;
    //            //print(transform.parent.name + " "+ size);
    //            if (name == "bot")
    //            {

    //                meshRenderer.material = player;
    //                GameObject back = bag.transform.GetChild(size).transform.gameObject;
    //                if (back.GetComponent<AddMaterials>().paarent == AIcontroller.mytiles)
    //                {
    //                    print("BOT IF");
    //                    //a.GetComponents<BoxCollider>()[0].enabled = false;
    //                    AIcontroller.MyTargets.Add(back);
    //                }
    //                bag.transform.GetChild(size).GetComponent<AddMaterials>().BackToFirstPosition();
    //            }
    //            else
    //            {
    //                if (Input.GetMouseButton(0))
    //                {
    //                    print("Else1");
    //                    //meshRenderer.material.color = pLayer.Bag.transform.GetChild(size).gameObject.GetComponent<MeshRenderer>().material.color;
    //                    meshRenderer.material = player;
    //                    //pLayer.Bag.transform.GetChild(size).GetComponent<AddMaterials>().BackToFirstPosition();
    //                }
    //            }
    //        }
    //        if (name != "bot")
    //        {
    //            if (Input.GetMouseButton(0))
    //            {
    //                if (meshRenderer.material == player)
    //                {
    //                    print("IF1");
    //                    //a.GetComponents<BoxCollider>()[0].enabled = false;
    //                }
    //            }
    //            if (bag.transform.childCount == 0)// && (meshRenderer.material != player || meshRenderer.material == player))
    //            {
    //                print("IF2");
    //                //if (!c1.Contains(a.GetComponents<BoxCollider>()[0]))
    //                //{
    //                //    c1.Add(a.GetComponents<BoxCollider>()[0]);
    //                //}
    //            }
    //        }
    //        if (bag.transform.childCount == 0)
    //        {
    //            if (name == "bot")
    //            {
    //                AIcontroller.stepsover();
    //            }
    //        }
    //    }
    //}
}

