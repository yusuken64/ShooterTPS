using System.Collections.Generic;
using UnityEngine;

public class InteractionSensor : MonoBehaviour
{
    public List<IInteractable> Nearby = new();

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !Nearby.Contains(interactable))
        {
            Nearby.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            Nearby.Remove(interactable);
        }
    }
}
