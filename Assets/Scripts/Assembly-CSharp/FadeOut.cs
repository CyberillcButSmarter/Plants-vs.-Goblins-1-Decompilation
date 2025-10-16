using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public float time;

	private SpriteRenderer renderer;

	private float curTime;

	private void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
		base.enabled = false;
	}

	public void Begin()
	{
		base.enabled = true;
	}

	private void Update()
	{
		curTime += Time.deltaTime;
		if (curTime <= time)
		{
			renderer.color = new Color(1f, 1f, 1f, 1f - curTime / time);
		}
		else
		{
			Object.Destroy(this);
		}
	}
}
