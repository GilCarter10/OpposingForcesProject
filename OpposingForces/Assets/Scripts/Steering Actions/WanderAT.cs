using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class WanderAT : ActionTask {
		public float wanderSampleFrequency;
		public float wanderDirectionChangeFrequency;
		public BBParameter<Vector3> acceleration;
		public float accelerationStrength;

		private Vector3 randomPoint = Vector3.zero;
		private Vector3 currentAccelerationDirection = Vector3.zero;

		private float timeSinceLastDirectionChange = 0f;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			timeSinceLastDirectionChange += Time.deltaTime;
			if (timeSinceLastDirectionChange > wanderDirectionChangeFrequency)
			{
				randomPoint = Random.insideUnitCircle.normalized;
				timeSinceLastDirectionChange = 0f;
				currentAccelerationDirection = new Vector3(randomPoint.x, agent.transform.position.y, randomPoint.y);
			}
			Debug.DrawLine(agent.transform.position, currentAccelerationDirection + agent.transform.position);
			acceleration.value += currentAccelerationDirection.normalized * Time.deltaTime * accelerationStrength;
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