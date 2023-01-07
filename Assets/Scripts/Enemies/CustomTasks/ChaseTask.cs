using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.CustomTasks
{
	[Category("CustomTask")]
	public class ChaseTask : ActionTask
	{
		public BBParameter<Transform> Target;
		public BBParameter<NavMeshAgent> Agent;
		public BBParameter<float> MinimumDistance;
		
		protected override void OnUpdate()
		{
			float distance = Vector3.Distance(agent.transform.position, Target.value.position);

			if (distance > MinimumDistance.value)
			{
				Agent.value.isStopped = false;
				Agent.value.SetDestination(Target.value.transform.position);
			}
			else
			{
				Agent.value.isStopped = true;
				EndAction(true);
			}
		}
	}
}