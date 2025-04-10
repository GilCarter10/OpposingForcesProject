using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class AvoidAT : ActionTask {
		public BBParameter<Vector3> acceleration;
		public BBParameter<Transform> target;
		public float avoidRange;
		public float steeringAcceleration;

		protected override void OnExecute() {
			float distanceToTarget = Vector3.Distance(agent.transform.position, target.value.position);
			if (distanceToTarget > avoidRange)
			{
				EndAction(true);
				return;
			}
			Vector3 direction = target.value.position - agent.transform.position;
			direction = new Vector3(direction.x, 0f, direction.z);
			acceleration.value -= direction.normalized * steeringAcceleration * Time.deltaTime;
			EndAction(true);
		}

	}
}