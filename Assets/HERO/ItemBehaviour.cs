using UnityEngine;
using System.Collections;

public class ItemBehaviour : StateMachineBehaviour {

	public Hero heroi;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.heroi.setSpriteBarras();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.heroi.resetAllButNoDamage ();
		if (stateInfo.normalizedTime == 1) {
			this.heroi.recuperaHp(2);
		}
	}
}
