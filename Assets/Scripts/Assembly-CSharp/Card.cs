using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
	public enum State
	{
		NORMAL = 0,
		SELECTED = 1,
		NO_SUN = 2,
		CD = 4
	}

	public Sprite enableSprite;

	public Sprite disableSprite;

	public int price;

	public float cd;

	public GameObject plant;

	[HideInInspector]
	public State state;

	private GameModel model;

	private HandlerForPlant plantHandler;

	private SpriteRenderer renderer;

	private float cdTime;

	public float CdTime
	{
		get
		{
			return cdTime;
		}
	}

	private void Awake()
	{
		model = GameModel.GetInstance();
		plantHandler = GameObject.Find("GameController").GetComponent<HandlerForPlant>();
		renderer = GetComponent<SpriteRenderer>();
		state = State.NORMAL;
		renderer.sprite = enableSprite;
		plant.GetComponent<PlantGrow>().price = price;
	}

	private void Update()
	{
		UpdateUI();
		if ((state & State.CD) != State.NORMAL)
		{
			cdTime -= Time.deltaTime;
			if (cdTime <= 0f)
			{
				state &= (State)(-5);
			}
		}
	}

	public void OnSelect()
	{
		if (state == State.NORMAL)
		{
			plantHandler.SetSelectedCard(this);
		}
	}

	private void UpdateUI()
	{
		CheckSun();
		if (state == State.NORMAL)
		{
			renderer.sprite = enableSprite;
		}
		else
		{
			renderer.sprite = disableSprite;
		}
	}

	private void CheckSun()
	{
		if (model.sun < price)
		{
			state |= State.NO_SUN;
		}
		else
		{
			state &= (State)(-3);
		}
	}

	public void Select()
	{
		state |= State.CD;
		state &= (State)(-2);
		cdTime = cd;
		model.sun -= price;
		UpdateUI();
	}

	public void SetSprite(bool enable)
	{
		if (enable)
		{
			renderer.sprite = enableSprite;
		}
		else
		{
			renderer.sprite = disableSprite;
		}
	}
}
