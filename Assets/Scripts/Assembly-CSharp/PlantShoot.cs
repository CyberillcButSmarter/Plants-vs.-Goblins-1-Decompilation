using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class PlantShoot : MonoBehaviour
{
	public GameObject[] bullets;

	public Vector3 bulletOffset;

	public float interval;

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
		else if (search.IsZombieInRange(grow.row, 0f, range))
		{
			StartCoroutine(Shoot());
			cdTime = cd;
		}
	}

	private IEnumerator Shoot()
	{
		Vector3 pos = base.transform.position + bulletOffset;
		GameObject[] array = bullets;
		foreach (GameObject bullet in array)
		{
			GameObject newBullet = Object.Instantiate(bullet);
			newBullet.transform.position = pos;
			newBullet.GetComponent<Bullet>().row = grow.row;
			newBullet.GetComponent<SpriteRenderer>().sortingOrder = 1000 * (grow.row + 1) + 1;
			yield return new WaitForSeconds(interval);
		}
	}
}
