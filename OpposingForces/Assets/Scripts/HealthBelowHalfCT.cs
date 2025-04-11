using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class HealthBelowHalfCT : ConditionTask {

		public BBParameter<TakeDamage> damageScript;
		private float maxHealth;

		protected override string OnInit(){
			maxHealth = damageScript.value.health;
			return null;
		}

		protected override bool OnCheck() {
			return damageScript.value.health <= (maxHealth/2);
		}
	}
}