using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class PatrolTask : ActionTask{
		public BBParameter<NavMeshAgent> Agent;
		public BBParameter<float> Range;
		public BBParameter<Animator> Animator;

		private Vector3 _centrePoint;
		private readonly int WalkingAnimaitonId = UnityEngine.Animator.StringToHash("Walking");

		protected override void OnExecute()
		{
			_centrePoint = Agent.value.transform.position;
		}

		protected override void OnUpdate(){
			if (!Animator.isNull && Animator.value.GetCurrentAnimatorStateInfo(0).IsName("MeleePunch"))
				return;

			if (!Agent.isNull && Agent.value.remainingDistance <= Agent.value.stoppingDistance)
			{
				TrySetNewWayPoint();
			}
		}

		private void TrySetNewWayPoint()
		{
			Vector3 point = _centrePoint + Random.insideUnitSphere * Range.value;

			if (NavMesh.SamplePosition(point, out NavMeshHit hit, Range.value, NavMesh.AllAreas))
			{
				Agent.value.SetDestination(hit.position);
				
				if(!Animator.isNull)
					Animator.value.SetBool(WalkingAnimaitonId, true);
			}
		}
	}
}