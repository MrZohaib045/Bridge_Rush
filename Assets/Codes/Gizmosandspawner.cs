using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmosandspawner : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public Vector3 areaPosition = Vector3.zero;
    public Vector3 areaSize = new Vector3(5, 5, 5);
    public GameObject[] prefabs;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position + areaPosition, areaSize);
    }

    private void Start()
    {
        SpawnObjects();
        //SelectPrefabs();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            Vector3 randomPosition = GetRandomPositionInArea();
            Instantiate(prefabs[i], randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 halfSize = areaSize / 2;
        float randomX = Random.Range(-halfSize.x, halfSize.x);
        float randomY = Random.Range(-halfSize.y, halfSize.y);
        float randomZ = Random.Range(-halfSize.z, halfSize.z);
        return transform.position + areaPosition + new Vector3(randomX, randomY, randomZ);
    }
}