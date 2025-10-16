using System.Collections;
using UnityEngine;

public class CoffeeBeanGrow : PlantGrow
{
	public AudioClip destroySound;

	public float effectiveTime;

	public float duration;

	public float animationTime;

	[HideInInspector]
	public GameObject sleepPlant;

	private Animator animator;

	private new void Awake()
	{
		base.Awake();
		animator = base.transform.Find("plant").GetComponent<Animator>();
		display = GetComponent<PlantSpriteDisplay>();
		base.enabled = false;
	}

	private void Update()
	{
		GameObject gameObject = model.map[row, col];
		if (!gameObject)
		{
			StopAllCoroutines();
			DoDestroy();
		}
	}

	public override bool canGrowInMap(int row, int col)
	{
		GameObject gameObject = model.map[row, col];
		if ((bool)gameObject)
		{
			if ((bool)gameObject.GetComponent<PlantSleep>())
			{
				return true;
			}
			if ((bool)gameObject.GetComponent<PumpkinGrow>())
			{
				PumpkinGrow component = gameObject.GetComponent<PumpkinGrow>();
				if ((bool)component.innerPlant && (bool)component.innerPlant.GetComponent<PlantSleep>())
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	public override void grow(int _row, int _col)
	{
		row = _row;
		col = _col;
		if ((bool)model.map[row, col].GetComponent<PlantSleep>())
		{
			sleepPlant = model.map[row, col];
		}
		else
		{
			sleepPlant = model.map[row, col].GetComponent<PumpkinGrow>().innerPlant;
		}
		display.SetOrderByRow(row);
		StartCoroutine(TakeEffect());
		base.enabled = true;
	}

	private IEnumerator TakeEffect()
	{
		yield return new WaitForSeconds(effectiveTime);
		sleepPlant.GetComponent<PlantSleep>().WakeUp(duration);
		yield return new WaitForSeconds(duration);
		DoDestroy();
	}

	private void DoDestroy()
	{
		animator.SetTrigger("destroy");
		Object.Destroy(base.gameObject, animationTime);
		AudioManager.GetInstance().PlaySound(destroySound);
	}
}
