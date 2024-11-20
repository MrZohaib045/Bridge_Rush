using UnityEngine;
using System.Collections;

public class RandomInstantiate : MonoBehaviour
{
    // Array to hold the prefabs to instantiate
    public GameObject[] Harm_objs;
    public GameObject[] Benefits_objs;

    // Range for random positions
    public Vector3 minPosition;
    public Vector3 maxPosition;

    // Time after which objects should be destroyed
    public float destroyDelay = 5f;

    void Start()
    {
        StartCoroutine(InstantiateObjectsInPairs());
    }

    IEnumerator InstantiateObjectsInPairs()
    {
        while (Harm_objs.Length > 0 && Benefits_objs.Length > 0)
        {
            // Generate random indices to select objects randomly
            int randomIndex1 = Random.Range(0, Harm_objs.Length);
            int randomIndex2 = Random.Range(0, Benefits_objs.Length);

            // Generate random positions for the pair
            Vector3 randomPosition1 = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            Vector3 randomPosition2 = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            // Instantiate the pair of objects at the random positions
            GameObject activeObject1 = Instantiate(Harm_objs[randomIndex1], randomPosition1, Quaternion.identity);
            GameObject activeObject2 = Instantiate(Benefits_objs[randomIndex2], randomPosition2, Quaternion.identity);

            // Wait for the specified time before destroying the objects
            yield return new WaitForSeconds(destroyDelay);

            // Destroy the instantiated objects
            if (activeObject1 != null) Destroy(activeObject1);
            if (activeObject2 != null) Destroy(activeObject2);

            // Optional: Wait until both objects are destroyed (to ensure scene cleanup)
            yield return new WaitUntil(() =>
                (activeObject1 == null || !activeObject1.activeInHierarchy) &&
                (activeObject2 == null || !activeObject2.activeInHierarchy));
        }
    }
}
