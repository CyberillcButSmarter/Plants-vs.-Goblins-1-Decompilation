using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public bool musicOn = true;

	public bool soundOn = true;

	private float _musicVolume = 1f;

	private float _soundVolume = 1f;

	private GameObject obj;

	private AudioSource mainMusic;

	private ArrayList sounds = new ArrayList();

	private static AudioManager instance;

	public float musicVolume
	{
		get
		{
			return _musicVolume;
		}
		set
		{
			if (value != _musicVolume)
			{
				_musicVolume = value;
				mainMusic.volume = value;
			}
		}
	}

	public float soundVolume
	{
		get
		{
			return _soundVolume;
		}
		set
		{
			if (value == _soundVolume)
			{
				return;
			}
			_soundVolume = value;
			foreach (AudioSource sound in sounds)
			{
				sound.volume = value;
			}
		}
	}

	public void PlayMusic(AudioClip music)
	{
		PlayMusic(music, true);
	}

	public void PlayMusic(AudioClip music, bool loop)
	{
		mainMusic.Stop();
		mainMusic.clip = music;
		mainMusic.volume = musicVolume;
		mainMusic.loop = loop;
		if (musicOn && Time.timeScale != 0f)
		{
			mainMusic.Play();
		}
	}

	public void StopMusic()
	{
		mainMusic.Stop();
	}

	public void PauseMusic()
	{
		mainMusic.Pause();
	}

	public void ResumeMusic()
	{
		if (musicOn && Time.timeScale != 0f)
		{
			mainMusic.Play();
		}
	}

	public AudioSource PlaySound(AudioClip sound)
	{
		return PlaySound(sound, false);
	}

	public AudioSource PlaySound(AudioClip sound, bool loop)
	{
		AudioSource audioSource = obj.AddComponent<AudioSource>();
		audioSource.clip = sound;
		audioSource.volume = soundVolume;
		audioSource.loop = loop;
		sounds.Add(audioSource);
		if (soundOn && Time.timeScale != 0f)
		{
			audioSource.Play();
		}
		if (!loop)
		{
			StartCoroutine(DoDestroy(audioSource, sound.length));
		}
		return audioSource;
	}

	public void StopSound(AudioSource sound)
	{
		StartCoroutine(DoDestroy(sound, 0f));
	}

	public void PauseSound(AudioSource sound)
	{
		sound.Pause();
	}

	public void ResumeSound(AudioSource sound)
	{
		if (soundOn && Time.timeScale != 0f)
		{
			sound.Play();
		}
	}

	public void PauseAllSounds()
	{
		foreach (AudioSource sound in sounds)
		{
			sound.Pause();
		}
	}

	public void ResumeAllSounds()
	{
		if (!soundOn || Time.timeScale == 0f)
		{
			return;
		}
		foreach (AudioSource sound in sounds)
		{
			sound.Play();
		}
	}

	private IEnumerator DoDestroy(AudioSource sound, float delay)
	{
		yield return new WaitForSeconds(delay);
		Object.Destroy(sound);
		sounds.Remove(sound);
	}

	public void Clear()
	{
		foreach (AudioSource sound in sounds)
		{
			Object.Destroy(sound);
		}
		sounds.Clear();
	}

	public static AudioManager GetInstance()
	{
		if (instance == null)
		{
			GameObject gameObject = new GameObject("AudioManager");
			Object.DontDestroyOnLoad(gameObject);
			instance = gameObject.AddComponent<AudioManager>();
			instance.obj = gameObject;
			gameObject.AddComponent<FollowWithCamera>();
			instance.mainMusic = gameObject.AddComponent<AudioSource>();
		}
		return instance;
	}
}
