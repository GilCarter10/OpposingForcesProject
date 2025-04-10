using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class BeamAttackAT : ActionTask {

        public GameObject beam;

		protected override void OnExecute() {
            beam.SetActive(true);
            EndAction(true);
        }

	}
}