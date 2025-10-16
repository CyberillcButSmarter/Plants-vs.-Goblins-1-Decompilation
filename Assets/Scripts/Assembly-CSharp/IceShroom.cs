using System.Collections;
using UnityEngine;

public class IceShroom : MonoBehaviour
{
	public AudioClip explodeSound;

	public GameObject effect;

	public Vector3 effectOffset;

	public int atk;

	public float frozenTime;

	public float speedDownTime;

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
		Object.Destroy(newEffect, 1.5f);
		GameModel model = GameModel.GetInstance();
		for (int i = 0; i < 5; i++)
		{
			object[] array = model.zombieList[i].ToArray();
			object[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				GameObject gameObject = (GameObject)array2[j];
				gameObject.GetComponent<ZombieHealthy>().Damage(atk);
				gameObject.GetComponent<AbnormalState>().FreezeUp(frozenTime);
				gameObject.GetComponent<AbnormalState>().SpeedDown(frozenTime + speedDownTime, 0.5f);
			}
		}
		AudioManager.GetInstance().PlaySound(explodeSound);
		GetComponent<PlantHealthy>().Die();
	}
}
