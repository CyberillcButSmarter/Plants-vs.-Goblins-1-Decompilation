using UnityEngine;

public class PumpkinHealthy : PlantHealthy
{
	private Animator frontAnimator;

	private Animator backAnimator;

	private PumpkinGrow pumpkinGrow;

	private new void Awake()
	{
		model = GameModel.GetInstance();
		frontAnimator = base.transform.Find("front").GetComponent<Animator>();
		backAnimator = base.transform.Find("back").GetComponent<Animator>();
		pumpkinGrow = GetComponent<PumpkinGrow>();
	}

	public override void Damage(int val)
	{
		base.Damage(val);
		frontAnimator.SetInteger("hp", hp);
		backAnimator.SetInteger("hp", hp);
	}

	public override void Die()
	{
		model.map[pumpkinGrow.row, pumpkinGrow.col] = pumpkinGrow.innerPlant;
		Object.Destroy(base.gameObject);
	}
}
