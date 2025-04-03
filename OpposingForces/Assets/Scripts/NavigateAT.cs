using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class NavigateAT : ActionTask {

		public BBParameter<Vector3> velocity;
		public BBParameter<Vector3> acceleration;
		public BBParameter<float> maxSpeed;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			velocity.value += acceleration.value;
			float groundSpeed = Mathf.Sqrt(velocity.value.x * velocity.value.x + velocity.value.y * velocity.value.y);
			if (maxSpeed.value < groundSpeed)
			{
				float cappedX = velocity.value.x / groundSpeed * maxSpeed.value;
				float cappedY = velocity.value.z / groundSpeed * maxSpeed.value;
				velocity = new Vector3(cappedX, cappedY, velocity.value.z);
			}
			agent.transform.position += velocity.value * Time.deltaTime;

			acceleration.value = Vector3.zero;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}