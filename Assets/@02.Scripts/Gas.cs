using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Gas : MonoBehaviour, IInteractable
{
    private float mGasAmount = 30f;
    public Lane Road;
    
    public void OnInteract(GameObject car)
    {
        car.GetComponent<CarController>().AddGas(mGasAmount);
        Road.MovingObjectsToRemove.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider car)
    {
        OnInteract(car.gameObject);
    }
}
