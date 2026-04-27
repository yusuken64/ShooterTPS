using DG.Tweening;
using System;
using UnityEngine;

public class ObjectiveInteractable : MonoBehaviour, IInteractable
{
	public event Action OnComplete;

	private bool used;

	public string GetInteractionText()
	{
		return "Interact Objective";
	}

	public void Interact()
	{
		if (used) { return; }
		used = true;
		OnComplete?.Invoke();
		transform.DOScale(0, 1.0f)
			.OnComplete(() => { this.gameObject.SetActive(false); });
	}

	public void OnFocusGained()
	{
		if (used) { return; }
		transform.DOPunchScale(Vector3.one * 1.1f, 0.1f);
	}

	public void OnFocusLost()
	{
	}
}