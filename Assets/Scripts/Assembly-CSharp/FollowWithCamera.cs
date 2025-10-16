using UnityEngine;

public class FollowWithCamera : MonoBehaviour
{
	private void Update()
	{
		Vector3 position = Camera.main.transform.position;
		position.z = 0f;
		base.transform.position = position;
	}
}
