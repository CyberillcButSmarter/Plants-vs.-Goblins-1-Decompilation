using UnityEngine;

public class GarlicHealthy : PlantHealthy
{
	private SearchZombie search;

	private new void Awake()
	{
		base.Awake();
		search = GetComponent<SearchZombie>();
	}

	public override void Damage(int val)
	{
		base.Damage(val);
		GameObject gameObject = search.SearchClosetZombie(grow.row, 0f, 0.8f);
		if ((bool)gameObject)
		{
			bool upward;
			switch (grow.row)
			{
			case 0:
				upward = false;
				break;
			case 4:
				upward = true;
				break;
			default:
				upward = (double)Random.Range(0f, 1f) < 0.5;
				break;
			}
			gameObject.GetComponent<ZombieMove>().ChangeRow(upward);
		}
	}

	private void AfterGrow()
	{
	}
}
