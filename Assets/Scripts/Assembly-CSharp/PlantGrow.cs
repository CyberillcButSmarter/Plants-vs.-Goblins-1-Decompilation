using UnityEngine;

public class PlantGrow : MonoBehaviour
{
	public GameObject soil;

	[HideInInspector]
	public int row;

	[HideInInspector]
	public int col;

	[HideInInspector]
	public int price;

	protected GameModel model;

	protected Transform shadow;

	protected PlantSpriteDisplay display;

	protected void Awake()
	{
		model = GameModel.GetInstance();
		shadow = base.transform.Find("shadow");
		display = GetComponent<PlantSpriteDisplay>();
	}

	public virtual bool canGrowInMap(int row, int col)
	{
		GameObject gameObject = model.map[row, col];
		if (!gameObject || (bool)gameObject.GetComponent<PumpkinGrow>())
		{
			return true;
		}
		return false;
	}

	public virtual void grow(int _row, int _col)
	{
		row = _row;
		col = _col;
		GameObject gameObject = model.map[row, col];
		if ((bool)gameObject && (bool)gameObject.GetComponent<PumpkinGrow>())
		{
			gameObject.GetComponent<PumpkinGrow>().innerPlant = base.gameObject;
		}
		else
		{
			model.map[row, col] = base.gameObject;
		}
		display.SetOrderByRow(row);
		if ((bool)shadow)
		{
			shadow.gameObject.SetActive(true);
		}
		if ((bool)soil)
		{
			GameObject gameObject2 = Object.Instantiate(soil);
			gameObject2.transform.position = base.transform.position;
			Object.Destroy(gameObject2, 0.2f);
		}
		base.gameObject.SendMessage("AfterGrow");
	}

	public void Sell()
	{
		model.sun += (int)((double)price * 0.6);
		GetComponent<PlantHealthy>().Die();
	}
}
