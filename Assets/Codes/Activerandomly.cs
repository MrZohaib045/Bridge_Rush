using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activerandomly : MonoBehaviour
{
    public float activationInterval;
    public int maxActivations;
    public float time;

    public List<GameObject> objectsToSpawn; // List to hold the GameObjects to spawn
    public List<Vector3> spawnPositions; // List to hold the possible spawn positions

    private int activationCount = 0;

    void Start()
    {
        if (objectsToSpawn.Count > 0 && spawnPositions.Count > 0)
        {
            StartCoroutine(SpawnObjectsAtRandomPositions());
        }
        else
        {
            Debug.LogWarning("Objects to spawn or spawn positions list is empty.");
        }
    }

    private IEnumerator SpawnObjectsAtRandomPositions()
    {
        while (activationCount < maxActivations && objectsToSpawn.Count > 0 && spawnPositions.Count > 0)
        {
            yield return new WaitForSeconds(activationInterval);

            int randomObjectIndex = Random.Range(0, objectsToSpawn.Count); // Select a random object
            int randomPositionIndex = Random.Range(0, spawnPositions.Count); // Select a random position

            // Instantiate the object at the random position
            GameObject spawnedObject = Instantiate(objectsToSpawn[randomObjectIndex], spawnPositions[randomPositionIndex], Quaternion.identity);

            // Remove the used object and position from the lists to prevent repetition
            objectsToSpawn.RemoveAt(randomObjectIndex);
            spawnPositions.RemoveAt(randomPositionIndex);

            // Optional: Store the spawned object reference if you want to manipulate it later
            StartCoroutine(DeactivateAfterTime(spawnedObject, time));

            activationCount++;
        }
    }

    private IEnumerator DeactivateAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
}
