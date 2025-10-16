using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
	public AudioClip attackSound;

	public int atk = 100;

	public float cd = 1f;

	public float range = 0.8f;

	protected Animator animator;

	protected AudioSource audioSource;

	protected GameModel model;

	protected ZombieMove move;

	protected AbnormalState state;

	protected GameObject target;

	private void Awake()
	{
		animator = base.transform.Find("zombie").GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		model = GameModel.GetInstance();
		move = GetComponent<ZombieMove>();
		state = GetComponent<AbnormalState>();
	}

	private void Update()
	{
		if (null == target)
		{
			target = SearchPlant();
		}
		if ((bool)target && move.enabled)
		{
			move.enabled = false;
			animator.SetBool("isAttacking", true);
			audioSource = AudioManager.GetInstance().PlaySound(attackSound, true);
			Invoke("DoAttack", cd);
		}
		else if (!target && !move.enabled)
		{
			move.enabled = true;
			animator.SetBool("isAttacking", false);
			AudioManager.GetInstance().StopSound(audioSource);
			CancelInvoke("DoAttack");
		}
	}

	public void DoAttack()
	{
		if ((bool)target)
		{
			target.GetComponent<PlantHealthy>().Damage(atk);
		}
		Invoke("DoAttack", cd);
	}

	public void StopAttack()
	{
		AudioManager.GetInstance().StopSound(audioSource);
		base.enabled = false;
	}

	public GameObject SearchPlant()
	{
		GameObject result = null;
		float num = 100000f;
		for (int i = 0; i < 9; i++)
		{
			GameObject gameObject = model.map[move.row, i];
			if ((bool)gameObject && (bool)gameObject.GetComponent<PlantHealthy>())
			{
				float num2 = base.transform.position.x - gameObject.transform.position.x;
				if (0f <= num2 && num2 <= range && num > num2)
				{
					num = num2;
					result = gameObject;
				}
			}
		}
		return result;
	}
}
