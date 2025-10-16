using UnityEngine;

public class PlantSpriteDisplay : MonoBehaviour, SpriteDisplay
{
	public int orderOffset;

	private SpriteRenderer shadow;

	private SpriteRenderer plant;

	private void Awake()
	{
		if ((bool)base.transform.Find("shadow"))
		{
			shadow = base.transform.Find("shadow").GetComponent<SpriteRenderer>();
		}
		plant = base.transform.Find("plant").GetComponent<SpriteRenderer>();
	}

	public void SetAlpha(float a)
	{
		Color white = Color.white;
		white.a = a;
		if ((bool)shadow)
		{
			shadow.color = white;
		}
		plant.color = white;
	}

	public void SetOrder(int order)
	{
		plant.sortingOrder = order;
	}

	public void SetOrderByRow(int row)
	{
		plant.sortingOrder = 1000 * (row + 1) + orderOffset;
	}
}
