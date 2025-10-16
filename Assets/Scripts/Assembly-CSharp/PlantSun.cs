using UnityEngine;

public class PlantSun : MonoBehaviour
{
	public GameObject sun;

	public int sunCount;

	public float produceCd;

	private float cdTime;

	private void Awake()
	{
		cdTime = produceCd / 4f;
		base.enabled = false;
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Update()
	{
		if (cdTime >= 0f)
		{
			cdTime -= Time.deltaTime;
			return;
		}
		cdTime = produceCd;
		ProduceSun();
	}

	private void ProduceSun()
	{
		for (int i = 0; i < sunCount; i++)
		{
			GameObject gameObject = Object.Instantiate(sun);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10000;
			gameObject.transform.position = base.transform.position;
			float num = 0.8f;
			Vector3 offset = new Vector3(Random.Range(0f - num, num), Random.Range(0f - num, num), 0f);
			JumpBy jumpBy = gameObject.AddComponent<JumpBy>();
			jumpBy.offset = offset;
			jumpBy.height = Random.Range(0.3f, 0.6f);
			jumpBy.time = Random.Range(0.4f, 0.6f);
			jumpBy.Begin();
		}
	}
}
