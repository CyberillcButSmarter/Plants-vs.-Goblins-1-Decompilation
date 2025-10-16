using UnityEngine;

public class EatAction : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.SendMessage("DoEat");
	}
}
