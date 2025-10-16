using UnityEngine;

public class PoleZombieMove : ZombieMove
{
	public float walkSpeed = 0.1f;

	private Animator animator;

	private ZombieAttack attack;

	private bool hasJumped;

	private new void Awake()
	{
		base.Awake();
		animator = base.transform.Find("zombie").GetComponent<Animator>();
		attack = GetComponent<ZombieAttack>();
		attack.enabled = false;
	}

	private void Update()
	{
		base.transform.Translate((0f - speed) * Time.deltaTime * state.ratio, 0f, 0f);
		if (hasJumped)
		{
			return;
		}
		GameObject gameObject = attack.SearchPlant();
		if ((bool)gameObject)
		{
			if (gameObject.tag == "UnableJump")
			{
				animator.SetTrigger("giveUp");
				attack.enabled = true;
			}
			else
			{
				animator.SetTrigger("jump");
			}
			speed = walkSpeed;
			hasJumped = true;
		}
	}
}
