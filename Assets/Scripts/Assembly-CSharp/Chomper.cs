using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class Chomper : MonoBehaviour
{
	public AudioClip chompSound;

	public float eatRange;

	public float cd;

	private Animator animator;

	private PlantGrow grow;

	private GameModel model;

	private SearchZombie search;

	private float cdTime;

	private bool isReady = true;

	private void Awake()
	{
		animator = base.transform.Find("plant").GetComponent<Animator>();
		grow = GetComponent<PlantGrow>();
		model = GameModel.GetInstance();
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
		if (!isReady)
		{
			isReady = true;
			animator.SetTrigger("ready");
		}
		if (search.IsZombieInRange(grow.row, 0f, eatRange))
		{
			animator.SetTrigger("eat");
			cdTime = cd;
			Invoke("ChompSound", 0.6f);
		}
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void ChompSound()
	{
		AudioManager.GetInstance().PlaySound(chompSound);
	}

	private void DoEat()
	{
		bool flag = false;
		object[] array = model.zombieList[grow.row].ToArray();
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject gameObject = (GameObject)array2[i];
			float num = gameObject.transform.position.x - base.transform.position.x;
			if (0f <= num && num <= eatRange)
			{
				gameObject.GetComponent<ZombieHealthy>().Eat();
				flag = true;
			}
		}
		if (flag)
		{
			cdTime = cd;
			isReady = false;
		}
		else
		{
			animator.SetTrigger("cancelEat");
			cdTime = 0f;
		}
	}
}
