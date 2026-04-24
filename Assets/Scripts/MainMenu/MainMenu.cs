using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public AudioClip EagleScreech;

	public void StartGame_Clicked()
	{
		Common.Instance.AudioManager.PlaySound(EagleScreech);
		SceneManager.LoadScene("Combat");
	}
}
