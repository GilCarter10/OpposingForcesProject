using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class NavigateAT : ActionTask {

        //public BBParameter<Vector3> velocity;
        //public BBParameter<Vector3> acceleration;
        //public BBParameter<float> maxSpeed;
        public BBParameter<VampireData> vampireData;

        protected override void OnUpdate() {
            vampireData.value.velocity += vampireData.value.acceleration;
			float speed = Mathf.Sqrt(vampireData.value.velocity.x * vampireData.value.velocity.x + vampireData.value.velocity.y * vampireData.value.velocity.y);
			if (vampireData.value.maxSpeed < speed)
			{
				float maxSpeedX = vampireData.value.velocity.x / speed * vampireData.value.maxSpeed;
				float maxSpeedY = vampireData.value.velocity.y / speed * vampireData.value.maxSpeed;
                vampireData.value.velocity = new Vector3(maxSpeedX, maxSpeedY, vampireData.value.velocity.z);
			}
			agent.transform.position += vampireData.value.velocity * Time.deltaTime;

            vampireData.value.acceleration = Vector3.zero;
		}

	}
}