using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
	public AudioClip pauseSound;

	public Text text;

	private AudioManager am;

	private void Awake()
	{
		am = AudioManager.GetInstance();
	}

	public void OnClick()
	{
		if ("PAUSE" == text.text)
		{
			pauseGame();
		}
		else
		{
			resumeGame();
		}
	}

	public void pauseGame()
	{
		text.text = "PAUSE";
		am.PauseAllSounds();
		am.PlaySound(pauseSound);
		Time.timeScale = 0f;
		am.PauseMusic();
	}

	public void resumeGame()
	{
		text.text = "GAME";
		Time.timeScale = 1f;
		am.ResumeAllSounds();
		am.PlaySound(pauseSound);
		am.ResumeMusic();
	}
}
