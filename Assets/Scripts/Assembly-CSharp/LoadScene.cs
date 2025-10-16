using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	public AudioClip bgmMusic;

	public GameObject logo;

	public GameObject loadingLayer;

	public GameObject title;

	public GameObject loadBar;

	public Button button;

	public Text text;

	private LoadBar loadBarScript;

	private AsyncOperation async;

	private void Awake()
	{
		Color white = Color.white;
		white.a = 0f;
		logo.GetComponent<SpriteRenderer>().color = white;
		loadBarScript = loadBar.GetComponent<LoadBar>();
		button.enabled = false;
	}

	private void Start()
	{
		StartCoroutine(Workflow());
		AudioManager.GetInstance().PlayMusic(bgmMusic);
	}

	private IEnumerator Workflow()
	{
		FadeIn fadeIn = logo.AddComponent<FadeIn>();
		fadeIn.time = 1f;
		fadeIn.Begin();
		yield return new WaitForSeconds(2f);
		FadeOut fadeOut = logo.AddComponent<FadeOut>();
		fadeOut.time = 1f;
		fadeOut.Begin();
		yield return new WaitForSeconds(1f);
		logo.SetActive(false);
		loadingLayer.SetActive(true);
		yield return new WaitForEndOfFrame();
		MoveBy move = title.AddComponent<MoveBy>();
		move.offset = new Vector3(0f, -2f, 0f);
		move.time = 1f;
		move.Begin();
		yield return new WaitForSeconds(1f);
		async = Application.LoadLevelAsync("MainScene");
		async.allowSceneActivation = false;
		yield return StartCoroutine(Loading());
		text.text = "开始游戏";
		button.enabled = true;
	}

	private IEnumerator Loading()
	{
		float curProgress = 0f;
		while (curProgress <= 1f)
		{
			float toProgress = async.progress / 0.9f;
			while (curProgress < toProgress)
			{
				curProgress += 0.01f;
				loadBarScript.SetProgress(curProgress);
				yield return new WaitForEndOfFrame();
			}
		}
	}

	public void OnStartGame()
	{
		async.allowSceneActivation = true;
	}
}
