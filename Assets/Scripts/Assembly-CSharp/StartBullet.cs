using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class StartBullet : MonoBehaviour
{
	public int atk;

	public float speed;

	public float range;

	[HideInInspector]
	public Vector3 direction;

	private SearchZombie search;

	private void Awake()
	{
		search = GetComponent<SearchZombie>();
	}

	private void Update()
	{
		base.transform.position = base.transform.position + speed * Time.deltaTime * direction;
		GameObject gameObject = search.SearchClosetZombie(range);
		if ((bool)gameObject)
		{
			gameObject.GetComponent<ZombieHealthy>().Damage(atk);
			Object.Destroy(base.gameObject);
		}
		if (!Camera.main.pixelRect.Contains(Camera.main.WorldToScreenPoint(base.transform.position)))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
