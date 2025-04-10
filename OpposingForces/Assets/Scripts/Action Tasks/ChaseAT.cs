using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChaseAT : ActionTask {
        
		public BBParameter<VampireData> vampireData;
        public float steeringAcceleration;
		public Animator animator;

		protected override void OnExecute() {
			//trigger animation
			animator.SetBool("vampire", true);

			//movement
			Vector3 direction = vampireData.value.player.position - agent.transform.position;
			direction = new Vector3(direction.x, direction.y, 0);
            vampireData.value.acceleration += direction.normalized * steeringAcceleration * Time.deltaTime;
			EndAction(true);
		}

	}
}