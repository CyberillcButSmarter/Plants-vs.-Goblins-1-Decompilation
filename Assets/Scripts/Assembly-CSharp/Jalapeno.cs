using System.Collections;
using UnityEngine;

public class Jalapeno : MonoBehaviour
{
	public AudioClip explodeSound;

	public GameObject effect;

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
		newEffect.transform.position = new Vector3(1.8f, base.transform.position.y + 0.5f, 0f);
		newEffect.GetComponent<SpriteRenderer>().sortingOrder = base.transform.Find("plant").GetComponent<SpriteRenderer>().sortingOrder + 1;
		Object.Destroy(newEffect, 1.2f);
		GameModel model = GameModel.GetInstance();
		int row = GetComponent<PlantGrow>().row;
		object[] zombies = model.zombieList[row].ToArray();
		object[] array = zombies;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = (GameObject)array[i];
			gameObject.GetComponent<ZombieHealthy>().BoomDie();
		}
		AudioManager.GetInstance().PlaySound(explodeSound);
		GetComponent<PlantHealthy>().Die();
	}
}
