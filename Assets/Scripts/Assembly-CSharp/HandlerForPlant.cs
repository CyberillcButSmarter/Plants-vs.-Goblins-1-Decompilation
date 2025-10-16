using UnityEngine;

public class HandlerForPlant : MonoBehaviour
{
	public AudioClip seedLift;

	public AudioClip seedCancel;

	public AudioClip plantGrow;

	private GameObject tempPlant;

	private GameObject selectedPlant;

	private Card selectedCard;

	private int row = -1;

	private int col = -1;

	private void Update()
	{
		HandleMouseMoveForPlant();
		HandleMouseDownForPlant();
	}

	private void HandleMouseDownForPlant()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Collider2D collider2D = Physics2D.OverlapPoint(Utility.GetMouseWorldPos());
			if (collider2D != null)
			{
				CancelSelectdCard();
				if (collider2D.gameObject.tag == "Card")
				{
					collider2D.gameObject.SendMessage("OnSelect");
					AudioManager.GetInstance().PlaySound(seedLift);
				}
			}
			else if ((bool)selectedPlant)
			{
				if (row != -1)
				{
					selectedPlant.transform.position = StageMap.GetPlantPos(row, col);
					selectedPlant.GetComponent<PlantGrow>().grow(row, col);
					AudioManager.GetInstance().PlaySound(plantGrow);
					selectedPlant = null;
					Object.Destroy(tempPlant);
					tempPlant = null;
					selectedCard.Select();
					selectedCard = null;
				}
				else
				{
					CancelSelectdCard();
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			CancelSelectdCard();
		}
	}

	private void HandleMouseMoveForPlant()
	{
		if (!selectedPlant)
		{
			return;
		}
		Vector3 mouseWorldPos = Utility.GetMouseWorldPos();
		Vector3 position = mouseWorldPos;
		position.y -= 0.3f;
		selectedPlant.transform.position = position;
		if (StageMap.IsPointInMap(mouseWorldPos))
		{
			StageMap.GetRowAndCol(mouseWorldPos, out row, out col);
			if (tempPlant.GetComponent<PlantGrow>().canGrowInMap(row, col))
			{
				tempPlant.transform.position = StageMap.GetPlantPos(row, col);
				tempPlant.GetComponent<SpriteDisplay>().SetOrderByRow(row);
			}
			else
			{
				col = (row = -1);
				tempPlant.transform.position = new Vector3(1000f, 1000f, 0f);
			}
		}
		else
		{
			col = (row = -1);
			tempPlant.transform.position = new Vector3(1000f, 1000f, 0f);
		}
	}

	private void CancelSelectdCard()
	{
		if ((bool)selectedCard)
		{
			Object.Destroy(tempPlant);
			Object.Destroy(selectedPlant);
			selectedPlant = (tempPlant = null);
			selectedCard.state &= (Card.State)(-2);
			selectedCard = null;
			AudioManager.GetInstance().PlaySound(seedCancel);
		}
	}

	public void SetSelectedCard(Card card)
	{
		card.state = Card.State.SELECTED;
		selectedCard = card;
		tempPlant = Object.Instantiate(card.plant);
		tempPlant.GetComponent<SpriteDisplay>().SetAlpha(0.6f);
		tempPlant.transform.position = new Vector3(1000f, 1000f, 0f);
		selectedPlant = Object.Instantiate(card.plant);
		selectedPlant.GetComponent<SpriteDisplay>().SetOrder(15000);
		selectedPlant.transform.position = new Vector3(1000f, 1000f, 0f);
	}
}
