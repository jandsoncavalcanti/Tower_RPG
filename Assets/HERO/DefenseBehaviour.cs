using UnityEngine;
using System.Collections;

public class DefenseBehaviour : StateMachineBehaviour {

	public Hero heroi;

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.heroi.resetAllButNoDamage();
		this.heroi.resetDefense();
	}
}
