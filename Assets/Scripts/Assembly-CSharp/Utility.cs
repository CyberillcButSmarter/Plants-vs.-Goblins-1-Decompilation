using UnityEngine;

public class Utility
{
	public static Vector3 GetMouseWorldPos()
	{
		Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		result.z = 0f;
		return result;
	}
}
