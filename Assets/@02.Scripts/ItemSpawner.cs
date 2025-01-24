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

    private List<GameObject> objectsPool = new List<GameObject>();

    private void Update()
    {
        spawnPositiveObject();
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectsPool.Add(obj);
    }

    public void ReturnObjects(List<GameObject> objs)
    {
        foreach (var obj in objs)
        {
            ReturnObject(obj);
        }
    }

    private void spawnPositiveObject()
    {
        PositiveSpawnTimer += Time.deltaTime;
        if (PositiveSpawnTimer > PositiveSpawnInterval)
        {
            spawnGas();
            PositiveSpawnTimer -= PositiveSpawnInterval;
        }
    }

    private void spawnGas()
    {
        int randomLaneNumber = Random.Range(1, Road.LineCount + 1);
        Vector3 spawnPos = Road.GetFarLanePos(randomLaneNumber) 
                           + Vector3.forward * GasPrefab.GetComponent<Collider>().bounds.size.z / 2f;;

        if (objectsPool.Count == 0)
        {
            GameObject newObject = Instantiate(GasPrefab, spawnPos, Quaternion.identity);
            newObject.GetComponent<Gas>().Road = Road;
            Road.AddMovingObjectToRemove(newObject);
        }
        else
        {
            GameObject newObject = objectsPool[0];
            objectsPool.RemoveAt(0);
            
            newObject.transform.position = spawnPos;
            newObject.SetActive(true);
            newObject.GetComponent<Gas>().Road = Road;
            Road.AddMovingObjectToRemove(newObject);
            
        }
    }
    
    
}
