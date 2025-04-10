using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class SetStartingHealthAT : ActionTask {

        public BBParameter<VampireData> vampireData;
        public TakeDamage damageScript;

		protected override void OnExecute() {
            vampireData.value.startingHealth = damageScript.health;
			EndAction(false);
		}

	}
}