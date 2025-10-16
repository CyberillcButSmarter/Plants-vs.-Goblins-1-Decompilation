using UnityEngine;

public class LoadBar : MonoBehaviour
{
	public GameObject rollCap;

	public GameObject grass;

	private const float endRotationZ = -360f;

	private const float endScale = 0.5f;

	private const float leftX = -1.5f;

	private const float rightX = 1.4f;

	private const float leftY = 0.5f;

	private const float rightY = 0.3f;

	private Material grassMaterial;

	private void Awake()
	{
		grassMaterial = grass.GetComponent<SpriteRenderer>().material;
		rollCap.transform.localPosition = new Vector3(-1.5f, 0.5f, 0f);
	}

	public void SetProgress(float ratio)
	{
		ratio = Mathf.Clamp(ratio, 0f, 1f);
		grassMaterial.SetFloat("_Progress", ratio);
		float x = Mathf.Lerp(-1.5f, 1.4f, ratio);
		float y = Mathf.Lerp(0.5f, 0.3f, ratio);
		rollCap.transform.localPosition = new Vector3(x, y, 0f);
		float z = Mathf.Lerp(0f, -360f, ratio);
		rollCap.transform.localEulerAngles = new Vector3(0f, 0f, z);
		float num = Mathf.Lerp(1f, 0.5f, ratio);
		rollCap.transform.localScale = new Vector3(num, num, 1f);
	}
}
