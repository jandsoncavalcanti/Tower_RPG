using UnityEngine;
using System.Collections;

public class StoppedSkullBehaviour : StateMachineBehaviour {

	public Caveira skull;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.skull.enterIdle();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.skull.exitIdle();
	}
}
