using UnityEngine;

public class LawnMower : MonoBehaviour
{
	public AudioClip sound;

	public int row;

	public float speed;

	private SearchZombie search;

	private bool startUp;

	private float range = 0.2f;

	private void Awake()
	{
		search = GetComponent<SearchZombie>();
	}

	private void Update()
	{
		if (startUp)
		{
			base.transform.Translate(Time.deltaTime * speed, 0f, 0f);
			object[] array = search.SearchZombiesInRange(row, 0f, range);
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				GameObject gameObject = (GameObject)array2[i];
				gameObject.GetComponent<ZombieHealthy>().Damage(10000);
			}
			if (base.transform.position.x > 5.6f)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else if (search.IsZombieInRange(row, 0f, range))
		{
			startUp = true;
			AudioManager.GetInstance().PlaySound(sound);
		}
	}
}
