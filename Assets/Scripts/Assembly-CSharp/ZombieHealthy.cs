using UnityEngine;

public class ZombieHealthy : MonoBehaviour
{
	public AudioClip damageSound;

	public int hp = 100;

	protected Animator animator;

	protected Blink blink;

	protected void Awake()
	{
		animator = base.transform.Find("zombie").GetComponent<Animator>();
		blink = base.transform.Find("zombie").GetComponent<Blink>();
	}

	public virtual void Damage(int val)
	{
		if (hp > 0)
		{
			AudioManager.GetInstance().PlaySound(damageSound);
			hp -= val;
			animator.SetInteger("hp", hp);
			blink.Begin(0.15f);
			if (hp <= 0)
			{
				Die();
			}
		}
	}

	protected void Die()
	{
		ZombieMove componentInChildren = GetComponentInChildren<ZombieMove>();
		GameModel.GetInstance().zombieList[componentInChildren.row].Remove(base.gameObject);
		componentInChildren.enabled = false;
		GetComponentInChildren<ZombieAttack>().StopAttack();
		Object.Destroy(base.gameObject, 2f);
	}

	public void BoomDie()
	{
		if (hp > 0)
		{
			animator.SetTrigger("boomDie");
			Die();
		}
	}

	public void Eat()
	{
		ZombieMove componentInChildren = GetComponentInChildren<ZombieMove>();
		GameModel.GetInstance().zombieList[componentInChildren.row].Remove(base.gameObject);
		componentInChildren.enabled = false;
		Object.Destroy(base.gameObject);
	}
}
