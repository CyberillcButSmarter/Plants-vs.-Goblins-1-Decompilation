using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardShow : MonoBehaviour
{
	private Card card;

	private TextMesh cdText;

	private void Awake()
	{
		card = GetComponent<Card>();
	}

	private void Start()
	{
		int sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
		Transform transform = base.transform.Find("Price");
		transform.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		transform.GetComponent<TextMesh>().text = card.price.ToString();
		Transform transform2 = base.transform.Find("CD");
		transform2.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		cdText = transform2.GetComponent<TextMesh>();
	}

	private void Update()
	{
		if ((card.state & Card.State.CD) != Card.State.NORMAL)
		{
			cdText.text = card.CdTime.ToString("F1") + "s";
		}
		else
		{
			cdText.text = string.Empty;
		}
	}
}
