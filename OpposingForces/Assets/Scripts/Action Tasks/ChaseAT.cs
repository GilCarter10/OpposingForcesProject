using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChaseAT : ActionTask {
		public BBParameter<Vector3> characterAcceleration;
		public BBParameter<Vector3> player;
		public float steeringAcceleration;

		public Animator animator;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//trigger animation
			animator.SetBool("vampire", true);

			//movement
			Vector3 direction = player.value - agent.transform.position;
			direction = new Vector3(direction.x, direction.y, 0);
			characterAcceleration.value += direction.normalized * steeringAcceleration * Time.deltaTime;
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}