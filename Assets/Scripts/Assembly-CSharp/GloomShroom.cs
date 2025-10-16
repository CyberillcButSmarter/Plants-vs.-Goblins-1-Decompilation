using System.Collections;
using UnityEngine;

public class GloomShroom : MonoBehaviour
{
	public GameObject bullet;

	public Vector3 bulletOffset;

	public float cd;

	public float range;

	public float attackTime;

	private Animator animator;

	private PlantGrow grow;

	private SearchZombie search;

	private float cdTime;

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
		if (cdTime > 0f)
		{
			cdTime -= Time.deltaTime;
		}
		else if (search.IsZombieInRange(range))
		{
			StartCoroutine(Shoot());
			cdTime = cd;
		}
	}

	private IEnumerator Shoot()
	{
		Vector3 pos = base.transform.position + bulletOffset;
		GameObject newBullet = Object.Instantiate(bullet);
		newBullet.transform.position = pos;
		newBullet.transform.Find("bullet").GetComponent<SpriteRenderer>().sortingOrder = 1000 * (grow.row + 1) + 1;
		animator.SetBool("isAttacking", true);
		yield return new WaitForSeconds(attackTime);
		animator.SetBool("isAttacking", false);
	}
}
