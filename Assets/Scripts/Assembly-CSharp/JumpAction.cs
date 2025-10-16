using UnityEngine;

public class JumpAction : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.Translate(-1f, 0f, 0f);
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<ZombieAttack>().enabled = true;
	}
}
