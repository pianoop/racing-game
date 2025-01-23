using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lane : MonoBehaviour
{
    public int LineCount;
    public float StartLineOffsetZ = 2f;
    
    private float mFirstLinePosX;
    private float mLaneInterval;
    private float mRoadPlayerLinePosZ;
    private float mRoadStartLinePosZ;
    private float mRoadEndLinePosZ;

    private float mRoadSpeed = 10f;
    
    public List<GameObject> MovingObjectsToRemove = new List<GameObject>();
    public List<GameObject> MovingObjectsToReset = new List<GameObject>();

    void Start()
    {
        calcLaneValue();
    }

    void Update()
    {
        moveOnRoadObjects();
    }

    

    public Vector3 GetFarLanePos(int line)
    {
        return new Vector3(GetLanePosX(line), 0, mRoadEndLinePosZ);
    }

    public Vector3 GetPlayerLanePos(int line)
    {
        return new Vector3(GetLanePosX(line), 0, mRoadPlayerLinePosZ);
    }
    
    public float GetLanePosX(int line)
    {
        if (line <= 0 || line > LineCount)
        {
            Debug.LogError("Lane line is out of range.");
        }

        return mFirstLinePosX + (line - 1) * mLaneInterval;
    }

    public bool IsInLaneRange(int line)
    {
        return line > 0 && line <= LineCount;
    }

    public void AddMovingObjectToRemove(GameObject movingObject)
    {
        MovingObjectsToRemove.Add(movingObject);
    }

    public void AddMovingObjectToReset(GameObject movingObject)
    {
        MovingObjectsToReset.Add(movingObject);
    }
    private void calcLaneValue()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float xMin = bounds.min.x;
        float xMax = bounds.max.x;
        mRoadEndLinePosZ = bounds.max.z;
        mRoadPlayerLinePosZ = bounds.min.z + StartLineOffsetZ;
        mRoadStartLinePosZ = bounds.min.z;

        mLaneInterval = (xMax - xMin) / LineCount;
        mFirstLinePosX = xMin + mLaneInterval / 2f;
    }

    private void moveOnRoadObjects()
    {
        foreach (GameObject movingObject in MovingObjectsToReset)
        {
            movingObject.transform.Translate(Vector3.forward * -mRoadSpeed * Time.deltaTime);
            if (IsOutOfStartLine(movingObject))
            {
                float newPosZ = mRoadEndLinePosZ + movingObject.GetComponent<Collider>().bounds.size.z / 2f;
                movingObject.transform.position = new Vector3(movingObject.transform.position.x, movingObject.transform.position.y, newPosZ);
                
            }
        }
        
        List<GameObject> removeList = new List<GameObject>();
        foreach (GameObject movingObject in MovingObjectsToRemove)
        {
            movingObject.transform.Translate(Vector3.forward * -mRoadSpeed * Time.deltaTime);
            if (IsOutOfStartLine(movingObject))
            {
                removeList.Add(movingObject);
            }
        }

        foreach (GameObject movingObject in removeList)
        {
            MovingObjectsToReset.Remove(movingObject);
            Destroy(movingObject);
        }
    }
    
    private bool IsOutOfStartLine(GameObject obj)
    {
        return obj.GetComponent<Collider>().bounds.max.z < mRoadStartLinePosZ;
    }
}