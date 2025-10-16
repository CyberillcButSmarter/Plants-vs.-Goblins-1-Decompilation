using UnityEngine;

public class Starfruit : MonoBehaviour
{
	public GameObject bullet;

	public Vector3 bulletOffset;

	public Vector3[] direction = new Vector3[5];

	public float cd;

	public float range;

	private SearchZombie search;

	private float cdTime;

	private void Awake()
	{
		search = GetComponent<SearchZombie>();
		base.enabled = false;
		for (int i = 0; i < 5; i++)
		{
			direction[i].Normalize();
		}
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Update()
	{
		if (cdTime > 0f)
		{
			cdTime -= Time.deltaTime;
		}
		else if (search.IsZombieInRange(range))
		{
			Shoot();
			cdTime = cd;
		}
	}

	private void Shoot()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = Object.Instantiate(bullet);
			gameObject.transform.position = base.transform.position + bulletOffset;
			gameObject.GetComponent<StartBullet>().direction = direction[i];
		}
	}
}
