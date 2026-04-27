using StarterAssets;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public InteractionSensor sensor;
    public float forwardThreshold = 0.7f;

    private IInteractable currentInteractable;
	private ObjectiveManager objectiveManager;
	private StarterAssetsInputs _input;
	private UI ui;

	private void Start()
	{
        objectiveManager = FindFirstObjectByType<ObjectiveManager>();
        _input = GetComponent<StarterAssetsInputs>();
        ui = FindFirstObjectByType<UI>();
    }

    void Update()
    {
        DetectInteractable();

        if (_input.interact &&
            currentInteractable != null)
        {
            currentInteractable.Interact();
            objectiveManager.RegisterInteraction(currentInteractable);
        }

        if (currentInteractable != null)
		{
            ui.InteractableText.text = currentInteractable.GetInteractionText();
		}
		else
		{
            ui.InteractableText.text = "";
        }
    }

    void DetectInteractable()
    {
        IInteractable best = null;
        float bestScore = forwardThreshold;

        foreach (var interactable in sensor.Nearby)
        {
            if (interactable == null) continue;

            Transform t = ((MonoBehaviour)interactable).transform;

            Vector3 dir = (t.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dir);

            if (dot > bestScore)
            {
                bestScore = dot;
                best = interactable;
            }
        }

        currentInteractable = best;
    }
}

public interface IInteractable
{
	void Interact();
    void OnFocusGained();
    void OnFocusLost();
	string GetInteractionText();
}
