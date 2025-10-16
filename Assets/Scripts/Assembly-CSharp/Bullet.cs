using UnityEngine;

[RequireComponent(typeof(SearchZombie))]
public class Bullet : MonoBehaviour
{
	public GameObject effect;

	public int atk;

	public float speed;

	public float range;

	public bool rightward = true;

	protected GameModel model;

	protected SearchZombie search;

	protected GameObject target;

	protected int _row;

	public int row
	{
		get
		{
			return _row;
		}
		set
		{
			_row = value;
			if (0 <= _row && _row < 5)
			{
				model.bulletList[_row].Add(base.gameObject);
			}
		}
	}

	private void Awake()
	{
		model = GameModel.GetInstance();
		search = GetComponent<SearchZombie>();
		_row = -1;
	}

	private void Update()
	{
		base.transform.Translate(speed * Time.deltaTime, 0f, 0f);
		if (_row < 0 || 5 <= _row)
		{
			target = null;
		}
		else if (rightward)
		{
			target = search.SearchClosetZombie(_row, 0f, range);
		}
		else
		{
			target = search.SearchClosetZombie(_row, 0f - range, 0f);
		}
		if ((bool)target)
		{
			target.GetComponent<ZombieHealthy>().Damage(atk);
			HitEffect();
		}
		if (!Camera.main.pixelRect.Contains(Camera.main.WorldToScreenPoint(base.transform.position)))
		{
			HitEffect();
		}
	}

	protected virtual void HitEffect()
	{
		if ((bool)effect)
		{
			GameObject gameObject = Object.Instantiate(effect);
			gameObject.transform.position = base.transform.position;
			Object.Destroy(gameObject, 0.2f);
		}
		DoDestroy();
	}

	public void DoDestroy()
	{
		if (0 <= _row && _row < 5)
		{
			model.bulletList[row].Remove(base.gameObject);
		}
		Object.Destroy(base.gameObject);
	}

	public void Reverse()
	{
		rightward = !rightward;
		base.transform.Rotate(0f, 180f, 0f);
	}
}
