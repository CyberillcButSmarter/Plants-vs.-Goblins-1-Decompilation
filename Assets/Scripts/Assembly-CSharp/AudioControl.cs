using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
	public Toggle musicToggle;

	public Toggle soundToggle;

	public Slider musicSlider;

	public Slider soundSlider;

	private AudioManager am;

	private void Awake()
	{
		am = AudioManager.GetInstance();
	}

	private void Start()
	{
		am.Clear();
		musicToggle.isOn = am.musicOn;
		soundToggle.isOn = am.soundOn;
		musicSlider.value = am.musicVolume;
		soundSlider.value = am.soundVolume;
	}

	public void OnMusicChanged()
	{
		am.musicOn = musicToggle.isOn;
		if (musicToggle.isOn)
		{
			am.ResumeMusic();
		}
		else
		{
			am.PauseMusic();
		}
	}

	public void OnSoundChanged()
	{
		am.soundOn = soundToggle.isOn;
		if (soundToggle.isOn)
		{
			am.ResumeAllSounds();
		}
		else
		{
			am.PauseAllSounds();
		}
	}

	public void OnMusicVolume()
	{
		am.musicVolume = musicSlider.value;
	}

	public void OnSoundVolume()
	{
		am.soundVolume = soundSlider.value;
	}
}
