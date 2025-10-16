using UnityEngine;

public class PotatoMine : MonoBehaviour
{
	public AudioClip explodeSound;

	public GameObject effect;

	public Vector3 effectOffset;

	public float readyTime;

	public float range;

	public float destroyTime;

	private Animator animator;

	private PlantGrow grow;

	private SearchZombie search;

	private void Awake()
	{
		animator = base.transform.Find("plant").GetComponent<Animator>();
		grow = GetComponent<PlantGrow>();
		search = GetComponent<SearchZombie>();
		base.enabled = false;
	}

	private void AfterGrow()
	{
		animator.SetBool("isReady", false);
		Invoke("GetReady", readyTime);
	}

	private void GetReady()
	{
		animator.SetBool("isReady", true);
		base.enabled = true;
	}

	private void Update()
	{
		object[] array = search.SearchZombiesInRange(grow.row, 0f - range, range);
		if (array.Length != 0)
		{
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				GameObject gameObject = (GameObject)array2[i];
				gameObject.GetComponent<ZombieHealthy>().Eat();
			}
			animator.SetTrigger("boom");
			GameObject gameObject2 = Object.Instantiate(effect);
			gameObject2.transform.position = base.transform.position + effectOffset;
			gameObject2.GetComponent<SpriteRenderer>().sortingOrder = base.transform.Find("plant").GetComponent<SpriteRenderer>().sortingOrder + 1;
			Object.Destroy(gameObject2, destroyTime);
			AudioManager.GetInstance().PlaySound(explodeSound);
			Invoke("DoDestroy", destroyTime);
			base.enabled = false;
		}
	}

	private void DoDestroy()
	{
		GetComponent<PlantHealthy>().Die();
	}
}
