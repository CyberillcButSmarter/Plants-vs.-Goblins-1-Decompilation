using UnityEngine;

public class StageMenu : MonoBehaviour
{
	public void RestartStage()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void BackToHome()
	{
		Application.LoadLevel("Map");
	}
}
