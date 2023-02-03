using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class CheckRangeTask : ConditionTask{
		public BBParameter<float> Range;
		public BBParameter<Transform> Origin;
		public BBParameter<Transform> Target;

		private float radius;

		protected override bool OnCheck()
		{
			float targetRadius = Target.value.gameObject.GetComponent<CapsuleCollider>().radius;
			float originRadius = Origin.value.gameObject.GetComponent<CapsuleCollider>().radius;
			radius = originRadius + targetRadius;
			
			if (Target.value == null)
				return false;
			
			float distance = Vector3.Distance(Origin.value.position, Target.value.position) - radius;
			
			if(distance <= Range.value)
				RotateToTarget();
			
			return distance <= Range.value;
		}
		
		private void RotateToTarget()
		{
			const float rotationSpeed = 5f; 
			Vector3 direction = (Target.value.position - Origin.value.transform.position).normalized;
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation(direction);
			
			Origin.value.transform.rotation =
			Quaternion.Lerp(Origin.value.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
}