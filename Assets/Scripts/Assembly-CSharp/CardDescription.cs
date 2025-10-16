using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardDescription : MonoBehaviour
{
	[Multiline]
	public string description;

	private Card card;

	private void Awake()
	{
		card = GetComponent<Card>();
		base.enabled = false;
	}

	private void OnGUI()
	{
		GUI.skin.box.fontSize = 18;
		GUI.skin.box.fontStyle = FontStyle.Bold;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		GUI.skin.box.richText = true;
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
		string text = "<color=white>" + description + "</color>";
		float height = 90f;
		Rect position = new Rect(vector.x + 50f, (float)Camera.main.pixelHeight - vector.y - 20f, 200f, height);
		GUI.Box(position, text);
	}

	public void OnMouseEnter()
	{
		base.enabled = true;
	}

	public void OnMouseExit()
	{
		base.enabled = false;
	}
}
