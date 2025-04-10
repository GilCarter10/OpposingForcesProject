using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class SetAnimatorTriggerAT : ActionTask {

		public Animator animator;
		public string triggerName;

		protected override void OnExecute() {
			animator.SetTrigger(triggerName);
			EndAction(true);
		}

	}
}