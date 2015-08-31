using UnityEngine;
using System.Collections;

public class HitBehaviour : StateMachineBehaviour {

	public Inimigo owner;
	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.owner.resetHit();
	}
}
