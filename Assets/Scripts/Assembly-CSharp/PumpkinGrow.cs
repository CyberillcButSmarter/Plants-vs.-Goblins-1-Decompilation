using UnityEngine;

public class PumpkinGrow : PlantGrow
{
	[HideInInspector]
	public GameObject innerPlant;

	private PumpkinSpriteDisplay pumpDisplay;

	private new void Awake()
	{
		model = GameModel.GetInstance();
		pumpDisplay = GetComponent<PumpkinSpriteDisplay>();
	}

	public override bool canGrowInMap(int row, int col)
	{
		GameObject gameObject = model.map[row, col];
		if ((bool)gameObject && (bool)gameObject.GetComponent<PumpkinGrow>())
		{
			return false;
		}
		return true;
	}

	public override void grow(int _row, int _col)
	{
		row = _row;
		col = _col;
		innerPlant = model.map[row, col];
		model.map[row, col] = base.gameObject;
		pumpDisplay.SetOrderByRow(row);
	}
}
