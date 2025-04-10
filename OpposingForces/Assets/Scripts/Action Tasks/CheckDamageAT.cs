using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class CheckDamageAT : ActionTask {

		public TakeDamage damageScript;
        public BBParameter<VampireData> vampireData;
		public float damageThreshold;

        protected override void OnUpdate() {
            vampireData.value.vampireDamageTaken = vampireData.value.startingHealth - damageScript.health;
            if (vampireData.value.vampireDamageTaken > damageThreshold)
			{
				EndAction(false);
			}
			EndAction(true);
			
        }

	}
}