using UnityEngine;

public class LostPaperAction : StateMachineBehaviour
{
	public AudioClip angrySound;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<PaperZombieMove>().speed = 0f;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		PaperZombieMove component = animator.transform.parent.GetComponent<PaperZombieMove>();
		component.speed = component.angrySpeed;
		PaperZombieAttack component2 = animator.transform.parent.GetComponent<PaperZombieAttack>();
		component2.cd = component2.angryCd;
		AudioManager.GetInstance().PlaySound(angrySound);
	}
}
