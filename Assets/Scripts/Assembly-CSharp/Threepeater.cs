using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class Threepeater : MonoBehaviour
{
	public GameObject bullet;

	public Vector3 bulletOffset;

	public float cd;

	public float range;

	private PlantGrow grow;

	private SearchZombie search;

	private float cdTime;

	private void Awake()
	{
		grow = GetComponent<PlantGrow>();
		search = GetComponent<SearchZombie>();
		base.enabled = false;
	}

	private void Update()
	{
		if (cdTime > 0f)
		{
			cdTime -= Time.deltaTime;
			return;
		}
		bool flag = search.IsZombieInRange(grow.row, 0f, range);
		if (grow.row - 1 >= 0)
		{
			flag = flag || search.IsZombieInRange(grow.row - 1, 0f, range);
		}
		if (grow.row + 1 < 5)
		{
			flag = flag || search.IsZombieInRange(grow.row + 1, 0f, range);
		}
		if (flag)
		{
			Shoot();
			cdTime = cd;
		}
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Shoot()
	{
		Vector3 position = base.transform.position + bulletOffset;
		GameObject[] array = new GameObject[3];
		for (int i = 0; i < 3; i++)
		{
			array[i] = Object.Instantiate(bullet);
			array[i].transform.position = position;
			array[i].GetComponent<Bullet>().row = grow.row - 1 + i;
			array[i].GetComponent<SpriteRenderer>().sortingOrder = 1000 * (grow.row + i) + 1;
		}
		MoveBy moveBy = array[0].AddComponent<MoveBy>();
		moveBy.offset.Set(0f, 1f, 0f);
		moveBy.time = 0.2f;
		moveBy.Begin();
		MoveBy moveBy2 = array[2].AddComponent<MoveBy>();
		moveBy2.offset.Set(0f, -1f, 0f);
		moveBy2.time = 0.2f;
		moveBy2.Begin();
	}
}
