using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Blink : MonoBehaviour
{
	private SpriteRenderer renderer;

	private float time;

	private float curTime;

	private void Awake()
	{
		base.enabled = false;
		renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		curTime += Time.deltaTime;
		Color color = renderer.color;
		if (curTime < time)
		{
			color.a = 1f - curTime / time;
		}
		else
		{
			color.a = curTime / time - 1f;
			if (curTime > time * 2f)
			{
				base.enabled = false;
			}
		}
		renderer.color = color;
	}

	public void Begin(float t)
	{
		base.enabled = true;
		time = t / 2f;
		curTime = 0f;
	}
}
