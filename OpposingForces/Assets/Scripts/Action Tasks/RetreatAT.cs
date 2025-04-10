using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class RetreatAT : ActionTask {

		public BBParameter<VampireData> vampireData;
		public float steeringAcceleration;

		public Animator animator;

		protected override void OnExecute() {
			//trigger animation
			animator.SetBool("hurt", true);

			//retreat movement
			Vector3 direction = vampireData.value.defaultPos.position - agent.transform.position;
            vampireData.value.acceleration += direction.normalized * steeringAcceleration * Time.deltaTime;
			if (direction.magnitude < 0.9)
			{
				//arrived
				animator.SetBool("vampire", false);
                animator.SetBool("hurt", false);
                EndAction(false);
			}
			EndAction(true);

		}

	}
}