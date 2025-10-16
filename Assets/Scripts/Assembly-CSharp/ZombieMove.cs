using UnityEngine;

public class ZombieMove : MonoBehaviour
{
	public AudioClip groanSound;

	public float speed = 0.1f;

	[HideInInspector]
	public int row;

	protected GameModel model;

	protected ZombieSpriteDisplay display;

	protected AbnormalState state;

	protected void Awake()
	{
		model = GameModel.GetInstance();
		display = GetComponent<ZombieSpriteDisplay>();
		state = GetComponent<AbnormalState>();
		Invoke("Groan", Random.Range(5f, 10f));
	}

	private void Update()
	{
		base.transform.Translate((0f - speed) * Time.deltaTime * state.ratio, 0f, 0f);
	}

	protected void Groan()
	{
		AudioManager.GetInstance().PlaySound(groanSound);
		Invoke("Groan", Random.Range(5f, 10f));
	}

	public void ChangeRow(bool upward)
	{
		MoveBy moveBy = base.gameObject.AddComponent<MoveBy>();
		Vector3 offset = new Vector3(0f, 1f, 0f);
		if (!upward)
		{
			offset.y = 0f - offset.y;
		}
		moveBy.offset = offset;
		moveBy.time = 0.5f;
		moveBy.Begin();
		model.zombieList[row].Remove(base.gameObject);
		if (upward)
		{
			row++;
		}
		else
		{
			row--;
		}
		model.zombieList[row].Add(base.gameObject);
		display.SetOrderByRow(row);
	}
}
