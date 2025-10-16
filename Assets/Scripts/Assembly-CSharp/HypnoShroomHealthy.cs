using UnityEngine;

public class HypnoShroomHealthy : PlantHealthy
{
	private SearchZombie search;

	private new void Awake()
	{
		base.Awake();
		search = GetComponent<SearchZombie>();
	}

	public override void Die()
	{
		base.Die();
		GameObject gameObject = search.SearchClosetZombie(grow.row, 0f, 0.8f);
		if ((bool)gameObject)
		{
			gameObject.GetComponent<ZombieHealthy>().BoomDie();
		}
	}

	private void AfterGrow()
	{
	}
}
