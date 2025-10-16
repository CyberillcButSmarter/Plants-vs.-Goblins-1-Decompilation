using System;
using UnityEngine;

public class JumpBy : MonoBehaviour
{
	public Vector3 offset;

	public float height;

	public float time;

	private Vector3 oriPos;

	private Vector3 offsetSpeed;

	private float curTime;

	private void Awake()
	{
		base.enabled = false;
	}

	public void Begin()
	{
		offsetSpeed = offset / time;
		oriPos = base.transform.position;
		base.enabled = true;
	}

	private void Update()
	{
		if (curTime < time)
		{
			curTime += Time.deltaTime;
			Vector3 position = oriPos + offsetSpeed * curTime;
			position.y += Mathf.Cos(curTime / time * (float)Math.PI - (float)Math.PI / 2f) * height;
			base.transform.position = position;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}
}
