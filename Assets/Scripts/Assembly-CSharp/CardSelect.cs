using System.Collections;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
	public GameObject[] cards;

	public int maxCardNumber;

	public GameObject StartButton;

	private float xOffset = 1.1f;

	private float yOffset = 0.6f;

	private ArrayList selectedCards = new ArrayList();

	private ArrayList barCardList = new ArrayList();

	private GameObject gameController;

	private GameObject cardBar;

	private void Awake()
	{
		gameController = GameObject.Find("GameController");
		cardBar = GameObject.Find("Cards");
		Transform transform = base.transform.Find("Text");
		transform.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
		TextMesh component = transform.GetComponent<TextMesh>();
		string text = component.text;
		component.text = text + "<color=yellow>" + maxCardNumber + "</color>";
	}

	private void Start()
	{
		Transform parent = base.transform.Find("CardContainer");
		for (int i = 0; i < cards.Length; i++)
		{
			float x = (float)(i % 4) * xOffset;
			float y = (float)(-(i / 4)) * yOffset;
			GameObject gameObject = Object.Instantiate(cards[i]);
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = new Vector3(x, y, 0f);
			gameObject.GetComponent<Card>().enabled = false;
			gameObject.tag = "SelectingCard";
		}
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Collider2D collider2D = Physics2D.OverlapPoint(Utility.GetMouseWorldPos());
		if (collider2D != null && collider2D.gameObject.tag == "SelectingCard")
		{
			GameObject gameObject = collider2D.gameObject;
			if (selectedCards.Contains(gameObject))
			{
				selectedCards.Remove(gameObject);
				gameObject.GetComponent<Card>().SetSprite(true);
				UpdateCardBar();
			}
			else if (selectedCards.Count < maxCardNumber)
			{
				selectedCards.Add(gameObject);
				gameObject.GetComponent<Card>().SetSprite(false);
				UpdateCardBar();
			}
		}
	}

	private void UpdateCardBar()
	{
		RemoveAllBarCards();
		float num = -0.6f;
		for (int i = 0; i < selectedCards.Count; i++)
		{
			GameObject original = selectedCards[i] as GameObject;
			GameObject gameObject = Object.Instantiate(original);
			gameObject.tag = "Card";
			gameObject.transform.parent = cardBar.transform;
			gameObject.transform.localPosition = new Vector3(0f, (float)i * num, 0f);
			barCardList.Add(gameObject);
		}
	}

	private void RemoveAllBarCards()
	{
		object[] array = barCardList.ToArray();
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject obj = (GameObject)array2[i];
			Object.Destroy(obj);
		}
		barCardList.Clear();
	}

	private void RemoveStartButton()
	{
		Object.Destroy(StartButton);
	}

	private void Submit()
	{
		foreach (GameObject barCard in barCardList)
		{
			barCard.GetComponent<Card>().enabled = true;
		}
		gameController.GetComponent<GameController>().AfterSelectCard();
	}

	public void SubmitButton()
	{
		foreach (GameObject barCard in barCardList)
		{
			barCard.GetComponent<Card>().enabled = true;
		}
		gameController.GetComponent<GameController>().AfterSelectCard();
		RemoveStartButton();
	}

	private void Reset()
	{
		foreach (GameObject selectedCard in selectedCards)
		{
			selectedCard.GetComponent<Card>().SetSprite(true);
		}
		selectedCards.Clear();
		RemoveAllBarCards();
	}
}
