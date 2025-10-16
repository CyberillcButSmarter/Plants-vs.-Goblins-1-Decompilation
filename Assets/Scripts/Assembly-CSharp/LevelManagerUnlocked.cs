using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerUnlocked : MonoBehaviour
{
	public int Level;

	public Image Image;

	private string LevelString;

	private void Start()
	{
		if (ButtonSettings.releasedLevelStatic >= Level)
		{
			Levelunlocked();
		}
		else
		{
			Levellocked();
		}
	}

	public void LevelSelect(string _level)
	{
		LevelString = _level;
		SceneManager.LoadScene(LevelString);
	}

	private void Levellocked()
	{
		GetComponent<Button>().interactable = false;
		Image.enabled = true;
	}

	private void Levelunlocked()
	{
		GetComponent<Button>().interactable = true;
		Image.enabled = false;
	}
}
