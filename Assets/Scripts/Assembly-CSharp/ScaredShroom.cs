using UnityEngine;

public class ScaredShroom : MonoBehaviour
{
	public float scaredRange;

	public float recoverTime;

	private Animator animator;

	private SearchZombie search;

	private PlantShoot shoot;

	private bool isCrying;

	private void Awake()
	{
		animator = base.transform.Find("plant").GetComponent<Animator>();
		search = GetComponent<SearchZombie>();
		shoot = GetComponent<PlantShoot>();
		base.enabled = false;
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Update()
	{
		bool flag = search.IsZombieInRange(scaredRange);
		if (flag && !isCrying)
		{
			if (IsInvoking("Recover"))
			{
				CancelInvoke("Recover");
				return;
			}
			isCrying = true;
			shoot.enabled = false;
			animator.SetBool("isCrying", true);
		}
		else if (!flag && isCrying && !IsInvoking("Recover"))
		{
			Invoke("Recover", recoverTime);
		}
	}

	private void Recover()
	{
		isCrying = false;
		shoot.enabled = true;
		animator.SetBool("isCrying", false);
	}
}
