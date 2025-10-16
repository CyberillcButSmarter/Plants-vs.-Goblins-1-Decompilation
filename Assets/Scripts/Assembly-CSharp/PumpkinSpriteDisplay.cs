using UnityEngine;

public class PumpkinSpriteDisplay : MonoBehaviour, SpriteDisplay
{
	public int frontOrderOffset = 1;

	public int backOrderOffset = -1;

	private SpriteRenderer front;

	private SpriteRenderer back;

	private void Awake()
	{
		front = base.transform.Find("front").GetComponent<SpriteRenderer>();
		back = base.transform.Find("back").GetComponent<SpriteRenderer>();
	}

	public void SetAlpha(float a)
	{
		Color white = Color.white;
		white.a = a;
		front.color = white;
		back.color = white;
	}

	public void SetOrder(int order)
	{
		front.sortingOrder = order;
		back.sortingOrder = order - 1;
	}

	public void SetOrderByRow(int row)
	{
		front.sortingOrder = 1000 * (row + 1) + frontOrderOffset;
		back.sortingOrder = 1000 * (row + 1) + backOrderOffset;
	}
}
