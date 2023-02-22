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
		public BBParameter<Animator> Animator;

		private float radius;
		private readonly int WalkingAnimaitonId = UnityEngine.Animator.StringToHash("Walking");

		protected override void OnExecute()
		{
			float targetRadius = Target.value.gameObject.GetComponent<CapsuleCollider>().radius;
			float originRadius = Agent.value.gameObject.GetComponent<CapsuleCollider>().radius;
			
			radius = originRadius + targetRadius;
		}

		protected override void OnUpdate()
		{
			if (!Animator.isNull && Animator.value.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
				return;
			
			if (Agent.isNull || !Agent.value.isOnNavMesh)
				return;
			
			float distance = Vector3.Distance(agent.transform.position, Target.value.position);

			if (distance > (MinimumDistance.value + radius))
			{
				 Agent.value.SetDestination(Target.value.transform.position);
				 if(!Animator.isNull)
					 Animator.value.SetBool(WalkingAnimaitonId, true);
			}
			else
			{
				 Agent.value.SetDestination(Agent.value.transform.position);
				 if (!Animator.isNull)
				 {
					 Animator.value.SetBool(WalkingAnimaitonId, false);
				 }
			}
		}

		protected override void OnStop()
		{
			if (!Agent.value.gameObject)
				return;
			
			if(!Agent.isNull && Agent.value.enabled)
				Agent.value.SetDestination(Agent.value.transform.position);
			
			if (!Animator.isNull)
				Animator.value.SetBool(WalkingAnimaitonId, false);
		}
	}
}