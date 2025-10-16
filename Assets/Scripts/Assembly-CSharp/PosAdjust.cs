using UnityEngine;

public class PosAdjust : StateMachineBehaviour
{
	public Vector3 offset;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.localPosition = offset;
	}
}
