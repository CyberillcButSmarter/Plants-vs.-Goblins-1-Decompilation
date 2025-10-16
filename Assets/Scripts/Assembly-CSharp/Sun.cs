using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sun : MonoBehaviour
{
	public AudioClip sound;

	public Vector3 disappearPos;

	public int value = 25;

	public float actionTime = 0.5f;

	public float disappearTime;

	private GameModel model;

	private Vector3 speed;

	private Vector3 scaleSpeed;

	private void Awake()
	{
		model = GameModel.GetInstance();
		scaleSpeed = new Vector3(0.9f, 0.9f, 0.9f) / actionTime;
		GetComponent<FadeOut>().Begin();
		Object.Destroy(base.gameObject, disappearTime);
		base.enabled = false;
	}

	private void Update()
	{
		base.transform.Translate(speed * Time.deltaTime);
		base.transform.localScale = base.transform.localScale - scaleSpeed * Time.deltaTime;
	}

	private void OnMouseDown()
	{
		model.sun += value;
		MoveBy component = GetComponent<MoveBy>();
		if ((bool)component)
		{
			component.enabled = false;
		}
		base.enabled = true;
		speed = (disappearPos - base.transform.position) / actionTime;
		AudioManager.GetInstance().PlaySound(sound);
		Object.Destroy(base.gameObject, actionTime);
	}
}
