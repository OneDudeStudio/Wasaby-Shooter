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

		private const string EnemyWalking = "EnemyWalking";
		private const string Idle = "Idle";

		private float radius;
		
		protected override void OnExecute()
		{
			float targetRadius = Target.value.gameObject.GetComponent<CapsuleCollider>().radius;
			float originRadius = Agent.value.gameObject.GetComponent<CapsuleCollider>().radius;
			radius = originRadius + targetRadius;
			Agent.value.updateRotation = false;
		}

		protected override void OnUpdate()
		{
			RotateToTarget();
			float distance = Vector3.Distance(agent.transform.position, Target.value.position);
			 
			if (distance > (MinimumDistance.value + radius) && Agent.value)
			{
				 Agent.value.SetDestination(Target.value.transform.position);
				 if(!Animator.isNull)
					 Animator.value.SetTrigger(EnemyWalking);
			}
			else
			{
				 Agent.value.SetDestination(Agent.value.transform.position);
				 if (!Animator.isNull)
				 {
					 Animator.value.ResetTrigger(EnemyWalking);
					 Animator.value.Play(Idle);
					 
				 }
			}
		}

		protected override void OnStop()
		{
			if(Agent.value && Agent.value.enabled)
				Agent.value.SetDestination(Agent.value.transform.position);
			
			if (!Animator.isNull)
			{
				Animator.value.ResetTrigger(EnemyWalking);	
				Animator.value.Play(Idle);
			}
		}

		private void RotateToTarget()
		{
			const float rotationSpeed = 5f; 
			Vector3 direction = (Target.value.position - Agent.value.transform.position).normalized;
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation(direction);
			
			Agent.value.transform.rotation =
			Quaternion.Lerp(Agent.value.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
}