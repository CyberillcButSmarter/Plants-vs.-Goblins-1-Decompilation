using UnityEngine;

public class AbnormalState : MonoBehaviour
{
	public GameObject icetrap;

	[HideInInspector]
	public float ratio = 1f;

	private int SPEED_DOWN = 1;

	private int FREEZE_UP = 4;

	private ZombieSpriteDisplay display;

	private Animator animator;

	private int state;

	private float speedDownRatio;

	private void Awake()
	{
		display = GetComponent<ZombieSpriteDisplay>();
		animator = base.transform.Find("zombie").GetComponent<Animator>();
		state = 0;
	}

	public void SpeedDown(float time, float val)
	{
		speedDownRatio = val;
		state |= SPEED_DOWN;
		UpdateAction();
		if (IsInvoking("RemoveSpeedDown"))
		{
			CancelInvoke("RemoveSpeedDown");
		}
		Invoke("RemoveSpeedDown", time);
	}

	private void RemoveSpeedDown()
	{
		state &= ~SPEED_DOWN;
		UpdateAction();
	}

	public void FreezeUp(float time)
	{
		state |= FREEZE_UP;
		UpdateAction();
		GameObject gameObject = Object.Instantiate(icetrap);
		gameObject.transform.position = base.transform.position;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = base.transform.Find("zombie").GetComponent<SpriteRenderer>().sortingOrder + 1;
		Object.Destroy(gameObject, time);
		if (IsInvoking("RemoveFreezeUp"))
		{
			CancelInvoke("RemoveFreezeUp");
		}
		Invoke("RemoveFreezeUp", time);
	}

	private void RemoveFreezeUp()
	{
		state &= ~FREEZE_UP;
		UpdateAction();
	}

	private void UpdateAction()
	{
		float speed;
		if ((state & FREEZE_UP) != 0)
		{
			speed = 0f;
			display.SetColor(0.5f, 0.5f, 1f);
		}
		else if ((state & SPEED_DOWN) != 0)
		{
			speed = speedDownRatio;
			display.SetColor(0.5f, 0.5f, 1f);
		}
		else
		{
			speed = 1f;
			display.SetColor(1f, 1f, 1f);
		}
		ratio = speed;
		animator.speed = speed;
	}
}
