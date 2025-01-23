using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum Dir
{
    None = 0,
    Left = -1,
    Right = 1,
}

public class CarController : MonoBehaviour
{
    public Lane Road;
    public UIManager UIController;
    private int mCurrentLaneNumber = 1;
    public float RemainGas { get; private set; }
    private float mConsumeGasPerSec = 10f;
    private float mMaxGas = 100f;
    
    private void Start()
    {
        moveCar(Dir.None);
        RemainGas = 100f;
    }

    void Update()
    {
        processClickMove();
        ConsumeGas();
    }

    public void AddGas(float gas)
    {
        float nextGasRemain = RemainGas + gas;
        nextGasRemain = mMaxGas < nextGasRemain ? mMaxGas : nextGasRemain;
        setGas(nextGasRemain);
    }

    private void setGas(float gas)
    {
        RemainGas = gas;
        UIController.SetRemainGas(gas);
    }

    private void processClickMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition.x < Screen.width / 2)
            {
                moveCar(Dir.Left);
            }
            else
            {
                moveCar(Dir.Right);
            }
        }
    }

    private void ConsumeGas()
    {
        float nextGasRemain = RemainGas - Time.deltaTime * mConsumeGasPerSec;
        if (nextGasRemain <= 0)
        {
            setGas(0);
            gameOver();
        }
        else
        {
            setGas(nextGasRemain);
        }
    }

    private void gameOver()
    {
        UIController.GameOver();
        Destroy(gameObject);
    }

    private bool moveCar(Dir dir)
    {
        int nextLaneNumber = mCurrentLaneNumber + (int)dir;
        if (!Road.IsInLaneRange(nextLaneNumber))
        {
            return false;
        }
        
        mCurrentLaneNumber = nextLaneNumber;
        transform.position = Road.GetPlayerLanePos(mCurrentLaneNumber);
        return true;
    }
}
