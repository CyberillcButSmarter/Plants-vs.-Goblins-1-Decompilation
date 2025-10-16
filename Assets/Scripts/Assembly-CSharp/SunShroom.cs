using UnityEngine;

public class SunShroom : MonoBehaviour
{
	public GameObject littleSun;

	public GameObject normalSun;

	public float produceCd;

	public float growTime;

	private Animator animator;

	private GameObject sun;

	private float cdTime;

	private float curGrowTime;

	private bool hasGrown;

	private void Awake()
	{
		animator = base.transform.Find("plant").GetComponent<Animator>();
		cdTime = produceCd / 4f;
		sun = littleSun;
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
		}
		else
		{
			cdTime = produceCd;
			ProduceSun();
		}
		if (curGrowTime < growTime)
		{
			curGrowTime += Time.deltaTime;
		}
		else if (!hasGrown)
		{
			hasGrown = true;
			sun = normalSun;
			animator.SetTrigger("growUp");
			Object.Destroy(GetComponent<PlantSleep>());
		}
	}

	private void ProduceSun()
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
