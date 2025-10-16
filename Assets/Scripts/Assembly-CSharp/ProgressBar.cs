using UnityEngine;

public class ProgressBar : MonoBehaviour
{
	public GameObject flagPrefab;

	private const float leftX = -0.69f;

	private const float rightX = 0.69f;

	private Material fullMaterial;

	private GameObject head;

	private void Awake()
	{
		fullMaterial = base.transform.Find("full").GetComponent<SpriteRenderer>().material;
		head = base.transform.Find("head").gameObject;
	}

	public void InitWithFlag(float[] percentage)
	{
		for (int i = 0; i < percentage.Length; i++)
		{
			GameObject gameObject = Object.Instantiate(flagPrefab);
			gameObject.transform.parent = base.transform;
			float t = Mathf.Clamp(percentage[i], 0f, 1f);
			float x = Mathf.Lerp(0.69f, -0.69f, t);
			gameObject.transform.localPosition = new Vector3(x, 0.06f, 0f);
		}
	}

	public void SetProgress(float ratio)
	{
		ratio = Mathf.Clamp(ratio, 0f, 1f);
		fullMaterial.SetFloat("_Progress", ratio);
		float x = Mathf.Lerp(0.69f, -0.69f, ratio);
		head.transform.localPosition = new Vector3(x, 0.03f, 0f);
	}
}
