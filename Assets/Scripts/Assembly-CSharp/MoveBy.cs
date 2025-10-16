using UnityEngine;

public class MoveBy : MonoBehaviour
{
	public Vector3 offset;

	public float time;

	private Vector3 speed;

	private float curTime;

	private void Awake()
	{
		base.enabled = false;
	}

	private void Update()
	{
		if (curTime < time)
		{
			curTime += Time.deltaTime;
			base.transform.Translate(speed * Time.deltaTime);
		}
		else
		{
			Object.Destroy(this);
		}
	}

	public void Begin()
	{
		base.enabled = true;
		speed = offset / time;
	}
}
