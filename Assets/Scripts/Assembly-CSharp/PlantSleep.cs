using UnityEngine;

public class PlantSleep : MonoBehaviour
{
	public MonoBehaviour[] stopBehaviour;

	private GameModel model;

	private Animator animator;

	private void Awake()
	{
		model = GameModel.GetInstance();
		animator = base.transform.Find("plant").GetComponent<Animator>();
	}

	public void WakeUp(float time)
	{
		animator.SetBool("isSleeping", false);
		EnableBehaviour(true);
		Invoke("Sleep", time);
	}

	private void AfterGrow()
	{
		if (model.inDay)
		{
			Invoke("Sleep", 0f);
		}
	}

	private void Sleep()
	{
		animator.SetBool("isSleeping", true);
		EnableBehaviour(false);
	}

	private void EnableBehaviour(bool enabled)
	{
		MonoBehaviour[] array = stopBehaviour;
		foreach (MonoBehaviour monoBehaviour in array)
		{
			monoBehaviour.enabled = enabled;
		}
	}
}
