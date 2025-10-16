using UnityEngine;

public class Torchwood : MonoBehaviour
{
	public GameObject peaBullet;

	public GameObject fireBullet;

	public float range;

	private GameModel model;

	private PlantGrow grow;

	private void Awake()
	{
		model = GameModel.GetInstance();
		grow = GetComponent<PlantGrow>();
		base.enabled = false;
	}

	private void AfterGrow()
	{
		base.enabled = true;
	}

	private void Update()
	{
		object[] array = model.bulletList[grow.row].ToArray();
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject gameObject = (GameObject)array2[i];
			float num = base.transform.position.x - gameObject.transform.position.x;
			if (0f < num && num <= range && (gameObject.CompareTag("PeaBullet") || gameObject.CompareTag("SnowBullet")))
			{
				GameObject gameObject2 = ((!gameObject.CompareTag("PeaBullet")) ? Object.Instantiate(peaBullet) : Object.Instantiate(fireBullet));
				Vector3 position = gameObject.transform.position;
				position.x += num;
				gameObject2.transform.position = position;
				gameObject2.GetComponent<Bullet>().row = gameObject.GetComponent<Bullet>().row;
				gameObject2.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
				if (!gameObject.GetComponent<Bullet>().rightward)
				{
					gameObject2.GetComponent<Bullet>().Reverse();
				}
				gameObject.GetComponent<Bullet>().DoDestroy();
			}
		}
	}
}
