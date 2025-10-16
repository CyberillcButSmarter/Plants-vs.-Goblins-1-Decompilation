using UnityEngine;

public class OpenScreen : MonoBehaviour
{
	public string NextScreen;

	public void GoToNextScreen()
	{
		Application.LoadLevel(NextScreen);
	}
}
