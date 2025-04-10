using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChangeTurnAT : ActionTask {

		public BBParameter<bool> myTurn;
		public Blackboard otherBossBlackboard;
		public Animator otherBossAnimator;

		private SpriteRenderer sprite;

		protected override string OnInit() {
			sprite = agent.GetComponentInChildren<SpriteRenderer>();
			return null;
		}

		protected override void OnExecute() {
			myTurn.value = false;
			otherBossAnimator.SetTrigger("ChangeTurn");
			otherBossBlackboard.SetVariableValue("MyTurn", true);
			otherBossBlackboard.SetVariableValue("chosenMove", Random.Range(1, 3));
			sprite.sortingOrder = 1;
			EndAction(true);
		}

	}
}