using UnityEngine;
using System.Collections;

public class HurtBehaviour : StateMachineBehaviour {

	public Hero heroi;

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.heroi.resetAllButNoDamage();
		this.heroi.resetHurt();
	}
}
