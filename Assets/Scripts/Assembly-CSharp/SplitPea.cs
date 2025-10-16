using UnityEngine;

public class SplitPea : MonoBehaviour
{
	public GameObject bullet;

	public Vector3 rightOffset;

	public Vector3 leftOffset;

	public float cd;

	public float range;

	private PlantGrow grow;

	private SearchZombie search;

	private float rightCdTime;

	private float leftCdTime;

	private void Awake()
	{
		grow = GetComponent<PlantGrow>();
		search = GetComponent<SearchZombie>();
		base.enabled = false;
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Update()
	{
		if (rightCdTime > 0f)
		{
			rightCdTime -= Time.deltaTime;
		}
		else if (search.IsZombieInRange(grow.row, 0f, range))
		{
			RightShoot();
			rightCdTime = cd;
		}
		if (leftCdTime > 0f)
		{
			leftCdTime -= Time.deltaTime;
		}
		else if (search.IsZombieInRange(grow.row, 0f - range, 0f))
		{
			LeftShoot();
			leftCdTime = cd;
		}
	}

	private void RightShoot()
	{
		GameObject gameObject = Object.Instantiate(bullet);
		gameObject.transform.position = base.transform.position + rightOffset;
		gameObject.GetComponent<Bullet>().row = grow.row;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1000 * (grow.row + 1) + 1;
	}

	private void LeftShoot()
	{
		GameObject gameObject = Object.Instantiate(bullet);
		gameObject.transform.position = base.transform.position + leftOffset;
		gameObject.GetComponent<Bullet>().row = grow.row;
		gameObject.GetComponent<Bullet>().Reverse();
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1000 * (grow.row + 1) + 1;
	}
}
