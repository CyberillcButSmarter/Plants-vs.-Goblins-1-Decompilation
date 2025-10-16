using UnityEngine;

public class SunLabel : MonoBehaviour
{
	private GameObject text;

	private GameModel model;

	private void Awake()
	{
		text = base.transform.Find("Text").gameObject;
		text.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
		model = GameModel.GetInstance();
	}

	private void Update()
	{
		text.GetComponent<TextMesh>().text = model.sun.ToString();
	}
}
