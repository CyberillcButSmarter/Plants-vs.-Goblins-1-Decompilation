using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class FumeShroomBullet : MonoBehaviour
{
	public int atk;

	public float attackLength;

	public float destroyTime;

	[HideInInspector]
	public int row;

	private SearchZombie search;

	private void Awake()
	{
		search = GetComponent<SearchZombie>();
	}

	private void Start()
	{
		object[] array = search.SearchZombiesInRange(row, (0f - attackLength) / 2f, attackLength / 2f);
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject gameObject = (GameObject)array2[i];
			gameObject.GetComponent<ZombieHealthy>().Damage(atk);
		}
		Object.Destroy(base.gameObject, destroyTime);
	}
}
