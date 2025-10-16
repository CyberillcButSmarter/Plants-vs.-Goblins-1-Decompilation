using UnityEngine;

public class GloomShroomBullet : MonoBehaviour
{
	public int atk;

	public float attackRadius;

	public float destroyTime;

	private SearchZombie search;

	private void Awake()
	{
		search = GetComponent<SearchZombie>();
	}

	private void Start()
	{
		object[] array = search.SearchZombiesInRange(attackRadius);
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject gameObject = (GameObject)array2[i];
			gameObject.GetComponent<ZombieHealthy>().Damage(atk);
		}
		Object.Destroy(base.gameObject, destroyTime);
	}
}
