using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class CheckDamageAT : ActionTask {

		public TakeDamage damageScript;
		public BBParameter<float> startingHealth;
		public BBParameter<float> damageTaken;

		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//startingHealth = damageScript.health;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			damageTaken.value = startingHealth.value - damageScript.health;
            if (damageTaken.value > 30)
			{
				EndAction(false);
			}
			EndAction(true);
			
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}