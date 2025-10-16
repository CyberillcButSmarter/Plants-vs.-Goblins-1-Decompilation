using UnityEngine;

public class Shovel : MonoBehaviour
{
	public Transform shovel;

	private Vector3 oriPos;

	private Quaternion oriRot;

	private void Awake()
	{
		oriPos = shovel.position;
		oriRot = shovel.rotation;
	}

	private void OnSelect()
	{
		shovel.Rotate(0f, 0f, 45f);
	}

	public void CancelSelected()
	{
		shovel.position = oriPos;
		shovel.rotation = oriRot;
	}
}
