using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class PlantSpike : MonoBehaviour
{
	public int atk;

	public int attackCount;

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
		if (cdTime >= 0f)
		{
			cdTime -= Time.deltaTime;
			return;
		}
		object[] array = search.SearchZombiesInRange(grow.row, 0f - range, range);
		if (array.Length != 0)
		{
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				GameObject gameObject = (GameObject)array2[i];
				gameObject.GetComponent<ZombieHealthy>().Damage(atk);
				attackCount--;
			}
			cdTime = cd;
			if (attackCount <= 0)
			{
				GameModel.GetInstance().map[grow.row, grow.col] = null;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
