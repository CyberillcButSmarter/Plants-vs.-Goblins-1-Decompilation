using System.Collections;
using UnityEngine;

public class CherryBomb : MonoBehaviour
{
	public AudioClip explodeSound;

	public GameObject effect;

	public Vector3 effectOffset;

	public float explodeRange;

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
		SearchZombie search = GetComponent<SearchZombie>();
		object[] array = search.SearchZombiesInRange(explodeRange);
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = (GameObject)array[i];
			gameObject.GetComponent<ZombieHealthy>().BoomDie();
		}
		AudioManager.GetInstance().PlaySound(explodeSound);
		GetComponent<PlantHealthy>().Die();
	}
}
