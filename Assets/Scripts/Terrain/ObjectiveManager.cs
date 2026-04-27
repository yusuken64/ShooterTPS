using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<ObjectiveInteractable> requiredInteractables;

    private HashSet<IInteractable> completed = new();
    private UI ui;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        ui.ObjectiveText.text = ObjectivesAsText();
    }

    private string ObjectivesAsText()
    {
        string text = "";
        text +=  $"Progress: { completed.Count}/{ requiredInteractables.Count}";

        if (completed.Count == requiredInteractables.Count)
        {
            text += Environment.NewLine;
            text += "Objective Complete!";
        }

        return text;
    }

	internal void SetObjectives(List<ObjectiveInteractable> objectives)
	{
        requiredInteractables = objectives;
        UpdateUI();
    }

	public void RegisterInteraction(IInteractable interactable)
    {
        if (requiredInteractables.Contains(interactable))
        {
            completed.Add(interactable);

            Debug.Log($"Progress: {completed.Count}/{requiredInteractables.Count}");

        }
        UpdateUI();
    }
}
