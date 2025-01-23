using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public Lane Road;
    public GameObject GasPrefab;
    // public List<GameObject> PositiveItemPrefabs;
    public float PositiveSpawnInterval = 2f;
    public float PositiveSpawnTimer = 0f;
    // public List<GameObject> NegativeItemPrefabs;
    // public float NegativeSpawnInterval;
    // public float NegativeSpawnTimer;

    private void Update()
    {
        spawnPositiveObject();
    }

    private void spawnPositiveObject()
    {
        PositiveSpawnTimer += Time.deltaTime;
        
        if (PositiveSpawnTimer > PositiveSpawnInterval)
        {
            int randomLaneNumber = Random.Range(1, Road.LineCount + 1);
            Vector3 spawnPos = Road.GetFarLanePos(randomLaneNumber) 
                               + Vector3.forward * GasPrefab.GetComponent<Collider>().bounds.size.z / 2f;;
            GameObject newObject = Instantiate(GasPrefab, spawnPos, Quaternion.identity);
            newObject.GetComponent<Gas>().Road = Road;
            Road.AddMovingObjectToRemove(newObject);
            
            PositiveSpawnTimer -= PositiveSpawnInterval;
        }
    }
}
