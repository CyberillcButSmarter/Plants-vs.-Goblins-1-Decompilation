using UnityEngine;

public class GatlingPeaGrow : PlantGrow
{
	public override bool canGrowInMap(int row, int col)
	{
		GameObject gameObject = model.map[row, col];
		if ((bool)gameObject)
		{
			if (gameObject.tag == "Repeater")
			{
				return true;
			}
			if ((bool)gameObject.GetComponent<PumpkinGrow>())
			{
				PumpkinGrow component = gameObject.GetComponent<PumpkinGrow>();
				if ((bool)component.innerPlant && component.innerPlant.tag == "Repeater")
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
		GameObject gameObject = model.map[row, col];
		if ((bool)gameObject && (bool)gameObject.GetComponent<PumpkinGrow>())
		{
			Object.Destroy(gameObject.GetComponent<PumpkinGrow>().innerPlant);
			gameObject.GetComponent<PumpkinGrow>().innerPlant = base.gameObject;
		}
		else
		{
			Object.Destroy(model.map[row, col]);
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
}
