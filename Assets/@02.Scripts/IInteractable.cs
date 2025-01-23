using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteract(GameObject car);
    void OnTriggerEnter(Collider car);
}
