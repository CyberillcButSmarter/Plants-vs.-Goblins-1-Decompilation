using System.Collections;
using UnityEngine;

public class Squash : MonoBehaviour
{
	public AudioClip findZombie;

	public float range;

	public float actionTime;

	public float destroyTime;

	private Animator animator;

	private PlantGrow grow;

	private SearchZombie search;

	private GameObject target;

	private void Awake()
	{
		animator = base.transform.Find("plant").GetComponent<Animator>();
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
		target = search.SearchClosetZombie(grow.row, 0f - range, range);
		if ((bool)target)
		{
			Vector3 position = base.transform.position;
			position.x = target.transform.position.x;
			base.transform.position = position;
			animator.SetTrigger("attack");
			AudioManager.GetInstance().PlaySound(findZombie);
			StartCoroutine(DoEat());
			base.enabled = false;
		}
	}

	private IEnumerator DoEat()
	{
		yield return new WaitForSeconds(actionTime);
		if ((bool)target)
		{
			target.GetComponent<ZombieHealthy>().Eat();
		}
		yield return new WaitForSeconds(destroyTime);
		GetComponent<PlantHealthy>().Die();
	}
}
