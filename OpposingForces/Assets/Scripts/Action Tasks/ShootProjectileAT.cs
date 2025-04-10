using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ShootProjectileAT : ActionTask {

		public GameObject projectilePrefab;

		protected override void OnExecute() {

            MonoBehaviour.Instantiate(projectilePrefab, agent.transform.position, agent.transform.rotation);
            EndAction(true);
		}

	}
}