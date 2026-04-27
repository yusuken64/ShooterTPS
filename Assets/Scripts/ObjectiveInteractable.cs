using UnityEngine;

public class ObjectiveInteractable : MonoBehaviour, IInteractable
{
	public string GetInteractionText()
	{
		return "Interact Objective";
	}

	public void Interact()
	{
	}

	public void OnFocusGained()
	{
	}

	public void OnFocusLost()
	{
	}
}
