using UnityEngine;
using System.Collections;

public class AttackBehaviour : StateMachineBehaviour {

	public Inimigo owner;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.owner.send_skull();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.owner.resetAttack();
		if (stateInfo.normalizedTime == 1) {
			this.owner.damage();
		}
	}
}
