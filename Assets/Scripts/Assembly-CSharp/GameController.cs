using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[Space(10f)]
	public AudioClip bgmMusic;

	[Space(10f)]
	public AudioClip loseMusic;

	public AudioClip winMusic;

	public AudioClip readySound;

	public AudioClip zombieComing;

	public AudioClip hugeWaveSound;

	public AudioClip finalWaveSound;

	[Space(10f)]
	public GameObject Zombie1;

	public GameObject Zombie2;

	public GameObject Zombie3;

	public GameObject Zombie4;

	public GameObject Zombie5;

	[Space(10f)]
	public GameObject gameLabel;

	public GameObject progressBar;

	public GameObject cardDialog;

	public GameObject sunLabel;

	public GameObject shovelBg;

	[Space(10f)]
	public GameObject sunPrefab;

	public float sunInterval;

	public int WayFrom;

	public int WayTo;

	[Space(10f)]
	public int initSun = 150;

	public bool inDay = true;

	public string nextStage;

	[Space(10f)]
	public Wave[] waves;

	[Space(10f)]
	public float readyTime;

	public float playTime;

	private GameModel model;

	private float elapsedTime;

	private bool hasLostGame;

	public static int releasedLevelStatic = 1;

	public int releasedLevel;

	private void Awake()
	{
		model = GameModel.GetInstance();
		if (PlayerPrefs.HasKey("Level"))
		{
			releasedLevelStatic = PlayerPrefs.GetInt("Level", releasedLevelStatic);
		}
	}

	private void Start()
	{
		model.Clear();
		model.sun = initSun;
		model.inDay = inDay;
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < waves.Length; i++)
		{
			if (waves[i].isLargeWave)
			{
				arrayList.Add(waves[i].percentage);
			}
		}
		progressBar.GetComponent<ProgressBar>().InitWithFlag((float[])arrayList.ToArray(typeof(float)));
		progressBar.SetActive(false);
		cardDialog.SetActive(false);
		sunLabel.SetActive(false);
		shovelBg.SetActive(false);
		GetComponent<HandlerForPlant>().enabled = false;
		GetComponent<HandlerForShovel>().enabled = false;
		StartCoroutine(GameReady());
		AudioManager.GetInstance().PlayMusic(bgmMusic);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			model.sun += 50;
		}
		if (!hasLostGame)
		{
			for (int i = 0; i < model.zombieList.Length; i++)
			{
				foreach (GameObject item in model.zombieList[i])
				{
					if (item.transform.position.x < -2.4f)
					{
						LoseGame();
						hasLostGame = true;
						return;
					}
				}
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			GameObject.Find("btn_menu").GetComponent<Button>().onClick.Invoke();
		}
	}

	private IEnumerator GameReady()
	{
		yield return new WaitForSeconds(0f);
		yield return new WaitForSeconds(0f);
		sunLabel.SetActive(true);
		cardDialog.SetActive(true);
	}

	public void AfterSelectCard()
	{
		UnityEngine.Object.Destroy(cardDialog);
		shovelBg.SetActive(true);
		GetComponent<HandlerForPlant>().enabled = true;
		GetComponent<HandlerForShovel>().enabled = true;
		Camera.main.transform.position = new Vector3(1.1f, 0f, -1f);
		StartCoroutine(Workflow());
		if (inDay)
		{
			InvokeRepeating("ProduceSun", sunInterval, sunInterval);
		}
	}

	private IEnumerator Workflow()
	{
		gameLabel.GetComponent<GameTips>().ShowStartTip();
		AudioManager.GetInstance().PlaySound(readySound);
		yield return new WaitForSeconds(readyTime);
		ShowProgressBar();
		AudioManager.GetInstance().PlaySound(zombieComing);
		for (int i = 0; i < waves.Length; i++)
		{
			yield return StartCoroutine(WaitForWavePercentage(waves[i].percentage));
			if (waves[i].isLargeWave)
			{
				StopCoroutine("UpdateProgress");
				yield return StartCoroutine(WaitForZombieClear());
				yield return new WaitForSeconds(3f);
				gameLabel.GetComponent<GameTips>().ShowApproachingTip();
				AudioManager.GetInstance().PlaySound(hugeWaveSound);
				yield return new WaitForSeconds(3f);
				StartCoroutine("UpdateProgress");
			}
			if (i + 1 == waves.Length)
			{
				gameLabel.GetComponent<GameTips>().ShowFinalTip();
				AudioManager.GetInstance().PlaySound(finalWaveSound);
			}
			CreateZombies(ref waves[i]);
		}
		yield return StartCoroutine(WaitForZombieClear());
		yield return new WaitForSeconds(2f);
		WinGame();
	}

	private void ShowProgressBar()
	{
		progressBar.SetActive(true);
		StartCoroutine("UpdateProgress");
	}

	private IEnumerator UpdateProgress()
	{
		while (true)
		{
			elapsedTime += Time.deltaTime;
			progressBar.GetComponent<ProgressBar>().SetProgress(elapsedTime / playTime);
			yield return 0;
		}
	}

	private void CreateZombies(ref Wave wave)
	{
		Wave.Data[] zombieData = wave.zombieData;
		for (int i = 0; i < zombieData.Length; i++)
		{
			Wave.Data data = zombieData[i];
			for (int j = 0; j < data.count; j++)
			{
				CreateOneZombie(data.zombieType);
			}
		}
	}

	private void CreateOneZombie(ZombieType type)
	{
		GameObject gameObject;
		switch (type)
		{
		case ZombieType.Zombie1:
			gameObject = UnityEngine.Object.Instantiate(Zombie1);
			break;
		case ZombieType.Zombie2:
			gameObject = UnityEngine.Object.Instantiate(Zombie2);
			break;
		case ZombieType.Zombie3:
			gameObject = UnityEngine.Object.Instantiate(Zombie3);
			break;
		case ZombieType.Zombie4:
			gameObject = UnityEngine.Object.Instantiate(Zombie4);
			break;
		case ZombieType.Zombie5:
			gameObject = UnityEngine.Object.Instantiate(Zombie5);
			break;
		default:
			throw new Exception("Wrong zombie type");
		}
		int num = UnityEngine.Random.Range(WayFrom, WayTo);
		gameObject.transform.position = StageMap.GetZombiePos(num);
		gameObject.GetComponent<ZombieMove>().row = num;
		gameObject.GetComponent<SpriteDisplay>().SetOrderByRow(num);
		model.zombieList[num].Add(gameObject);
	}

	private IEnumerator WaitForZombieClear()
	{
		while (true)
		{
			bool hasZombie = false;
			for (int i = 0; i < 5; i++)
			{
				if (model.zombieList[i].Count != 0)
				{
					hasZombie = true;
					break;
				}
			}
			if (hasZombie)
			{
				yield return new WaitForSeconds(2.1f);
				continue;
			}
			break;
		}
	}

	private IEnumerator WaitForWavePercentage(float percentage)
	{
		while (!(elapsedTime / playTime >= percentage))
		{
			yield return 0;
		}
	}

	private void ProduceSun()
	{
		float x = UnityEngine.Random.Range(-2f, 5.2f);
		float num = UnityEngine.Random.Range(-2.6f, 2.3f);
		float num2 = 3.8f;
		GameObject gameObject = UnityEngine.Object.Instantiate(sunPrefab);
		gameObject.transform.position = new Vector3(x, num2, 0f);
		MoveBy moveBy = gameObject.AddComponent<MoveBy>();
		moveBy.offset = new Vector3(0f, num - num2, 0f);
		moveBy.time = (num2 - num) / 1f;
		moveBy.Begin();
	}

	private void LoseGame()
	{
		gameLabel.GetComponent<GameTips>().ShowLostTip();
		GetComponent<HandlerForPlant>().enabled = false;
		GetComponent<HandlerForShovel>().enabled = false;
		CancelInvoke("ProduceSun");
		AudioManager.GetInstance().PlayMusic(loseMusic, false);
	}

	private void WinGame()
	{
		CancelInvoke("ProduceSun");
		AudioManager.GetInstance().PlayMusic(winMusic, false);
		if (releasedLevelStatic <= releasedLevel)
		{
			releasedLevelStatic = releasedLevel;
			PlayerPrefs.SetInt("Level", releasedLevelStatic);
		}
		Invoke("GoToNextStage", 2f);
	}

	private void GoToNextStage()
	{
		Application.LoadLevel(nextStage);
	}
}
