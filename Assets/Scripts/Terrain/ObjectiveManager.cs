using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public TerrainGenerator TerrainGenerator;
    public ObjectiveInteractable ObjectivePrefab;
    public GameObject HelicopterPrefab;

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

    internal void SpawnObjective()
    {
        List<ObjectiveInteractable> objectives = new();
        for (int i = 0; i < 3; i++)
        {
            var newItem = TerrainGenerator.SpawnObjective(ObjectivePrefab.gameObject);
            objectives.Add(newItem.GetComponent<ObjectiveInteractable>());
        }

        SetObjectives(objectives);
    }

	private string ObjectivesAsText()
    {
        string text = "";
        text +=  $"Progress: { completed.Count}/{ requiredInteractables.Count}";

        if (completed.Count == requiredInteractables.Count)
        {
            text += Environment.NewLine;
            text += "Objective Complete! Get to the CHOPPA!";
        }

        return text;
    }

	internal void SetObjectives(List<ObjectiveInteractable> objectives)
	{
        requiredInteractables = objectives;
        foreach(var interactable in requiredInteractables)
		{
			interactable.OnComplete += Interactable_OnComplete;
		}
        UpdateUI();
    }

	private void Interactable_OnComplete()
    {
        if (completed.Count == requiredInteractables.Count)
        {
            var newItem = TerrainGenerator.SpawnObjective(HelicopterPrefab.gameObject);
            var interactable = newItem.GetComponentInChildren<ObjectiveInteractable>();
			interactable.OnComplete += HeliInteractable_OnComplete;
        }
    }

	private void HeliInteractable_OnComplete()
    {
        FindFirstObjectByType<ResultsScreen>(FindObjectsInactive.Include).ShowMissionComplete();
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
