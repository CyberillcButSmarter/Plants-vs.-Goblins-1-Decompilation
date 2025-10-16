using System.Collections;
using UnityEngine;

public class DoomShroom : MonoBehaviour
{
	public AudioClip explodeSound;

	public GameObject crater;

	public GameObject effect;

	public Vector3 effectOffset;

	public float delayTime;

	private void AfterGrow()
	{
		base.transform.Find("plant").GetComponent<Animator>().Rebind();
		StartCoroutine(Explode());
	}

	private IEnumerator Explode()
	{
		yield return new WaitForSeconds(delayTime);
		GameObject newEffect = Object.Instantiate(effect);
		newEffect.transform.position = base.transform.position + effectOffset;
		newEffect.GetComponent<SpriteRenderer>().sortingOrder = base.transform.Find("plant").GetComponent<SpriteRenderer>().sortingOrder + 1;
		Object.Destroy(newEffect, 1.5f);
		GameModel model = GameModel.GetInstance();
		for (int i = 0; i < 5; i++)
		{
			object[] array = model.zombieList[i].ToArray();
			object[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				GameObject gameObject = (GameObject)array2[j];
				gameObject.GetComponent<ZombieHealthy>().BoomDie();
			}
		}
		GetComponent<PlantHealthy>().Die();
		AudioManager.GetInstance().PlaySound(explodeSound);
		GameObject newCrater = Object.Instantiate(crater);
		newCrater.transform.position = base.transform.position;
		PlantGrow grow = GetComponent<PlantGrow>();
		newCrater.GetComponent<PlantGrow>().grow(grow.row, grow.col);
	}
}
