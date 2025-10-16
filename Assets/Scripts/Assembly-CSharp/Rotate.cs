using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float speed;

	private void Update()
	{
		base.transform.Rotate(0f, 0f, base.transform.rotation.z + speed * Time.deltaTime);
	}
}
