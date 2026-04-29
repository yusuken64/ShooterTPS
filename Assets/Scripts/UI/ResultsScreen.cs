using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
	public GameObject MissionCompleteObject;
	public GameObject GameOverObject;

	private bool resultsShown;

	public void ShowGameOver()
	{
		if (resultsShown) { return; }
		resultsShown = true;

		MissionCompleteObject.gameObject.SetActive(false);
		GameOverObject.gameObject.SetActive(true);
		this.gameObject.SetActive(true);

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void GameOverOK_Clicked()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ShowMissionComplete()
	{
		if (resultsShown) { return; }
		resultsShown = true;

		MissionCompleteObject.gameObject.SetActive(true);
		GameOverObject.gameObject.SetActive(false);
		this.gameObject.SetActive(true);

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void MissionCompleteOK_Clicked()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
