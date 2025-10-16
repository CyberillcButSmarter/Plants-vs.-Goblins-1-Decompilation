using UnityEngine;

public class HandlerForShovel : MonoBehaviour
{
	public AudioClip shovelLift;

	public AudioClip shovelCancel;

	public GameObject shovelBg;

	private GameObject shovel;

	private GameObject selectedPlant;

	private void Update()
	{
		HandleMouseMoveForShovel();
		HandleMouseDownForShovel();
	}

	private void HandleMouseDownForShovel()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (shovelBg.GetComponent<Collider2D>().OverlapPoint(Utility.GetMouseWorldPos()))
			{
				CancelSelectedShovel();
				shovel = shovelBg.GetComponent<Shovel>().shovel.gameObject;
				shovelBg.SendMessage("OnSelect");
				AudioManager.GetInstance().PlaySound(shovelLift);
			}
			else if ((bool)shovel)
			{
				if ((bool)selectedPlant)
				{
					selectedPlant.GetComponent<PlantGrow>().Sell();
					selectedPlant = null;
				}
				else
				{
					CancelSelectedShovel();
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			CancelSelectedShovel();
		}
	}

	private void HandleMouseMoveForShovel()
	{
		if (!shovel)
		{
			return;
		}
		Vector3 mouseWorldPos = Utility.GetMouseWorldPos();
		Vector3 position = mouseWorldPos;
		position.x += 0.1f;
		position.y += 0.1f;
		shovel.transform.position = position;
		if (!StageMap.IsPointInMap(mouseWorldPos))
		{
			return;
		}
		int row;
		int col;
		StageMap.GetRowAndCol(mouseWorldPos, out row, out col);
		GameObject gameObject = GameModel.GetInstance().map[row, col];
		if (selectedPlant != gameObject)
		{
			if ((bool)selectedPlant)
			{
				selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(1f);
			}
			if ((bool)gameObject && gameObject.tag != "UnableSell")
			{
				selectedPlant = gameObject;
				selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(0.6f);
			}
			else
			{
				selectedPlant = null;
			}
		}
	}

	private void CancelSelectedShovel()
	{
		if ((bool)shovel)
		{
			shovelBg.GetComponent<Shovel>().CancelSelected();
			shovel = null;
			AudioManager.GetInstance().PlaySound(shovelCancel);
			if ((bool)selectedPlant)
			{
				selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(1f);
				selectedPlant = null;
			}
		}
	}
}
