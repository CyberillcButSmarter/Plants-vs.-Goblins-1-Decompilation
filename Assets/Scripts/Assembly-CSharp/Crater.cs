using System.Collections;
using UnityEngine;

public class Crater : MonoBehaviour
{
	public float leftTime;

	private PlantGrow grow;

	private void Awake()
	{
		grow = GetComponent<PlantGrow>();
	}

	private void AfterGrow()
	{
		StartCoroutine(DoDestroy());
	}

	private IEnumerator DoDestroy()
	{
		yield return new WaitForSeconds(leftTime);
		GameModel model = GameModel.GetInstance();
		model.map[grow.row, grow.col] = null;
		Object.Destroy(base.gameObject);
	}
}
