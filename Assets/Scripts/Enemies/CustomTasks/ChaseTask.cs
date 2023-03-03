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

		private Vector3 _targetOnCirclePosition;
		private float _radius;
		private readonly int WalkingAnimaitonId = UnityEngine.Animator.StringToHash("Walking");

		protected override void OnExecute()
		{
			float targetRadius = Target.value.gameObject.GetComponent<CapsuleCollider>().radius;
			float originRadius = Agent.value.gameObject.GetComponent<CapsuleCollider>().radius;
			
			_radius = originRadius + targetRadius;

			float randomPosition = Mathf.PI * Random.Range(0.01f, 0.99f);
			_targetOnCirclePosition = new Vector3(
				MinimumDistance.value * 3f * Mathf.Cos(randomPosition),
				0,
				MinimumDistance.value * 3f * Mathf.Sin(randomPosition));
		}

		protected override void OnUpdate()
		{
			if (!Animator.isNull && Animator.value.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
				return;
			
			if (Agent.isNull || !Agent.value.isOnNavMesh)
				return;

			//Vector3 targetPosition = Target.value.position + _targetOnCirclePosition;
			Vector3 targetPosition = Target.value.position;

			float distance = Vector3.Distance(agent.transform.position, targetPosition);

			if (distance > MinimumDistance.value + _radius)
			{
				 Agent.value.SetDestination(targetPosition);
				 if(!Animator.isNull)
					 Animator.value.SetBool(WalkingAnimaitonId, true);
			}
			// else if (Vector3.Distance(agent.transform.position, Target.value.position)>MinimumDistance.value + _radius)
			// {
			// 	Agent.value.SetDestination(Target.value.position);
			// }
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