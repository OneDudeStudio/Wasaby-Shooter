using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class CheckRangeTask : ConditionTask{
		public BBParameter<float> Range;
		public BBParameter<Transform> Origin;
		public BBParameter<Transform> Target;

		public float AdditionalDistance { get; set; }

		protected override bool OnCheck()
		{
			float targetRadius = Target.value.gameObject.GetComponent<CapsuleCollider>().radius;
			float originRadius = Origin.value.gameObject.GetComponent<CapsuleCollider>().radius;
			AdditionalDistance = originRadius + targetRadius;
			
			if (Target.isNull)
				return false;
			
			float distance = Vector3.Distance(Origin.value.position, Target.value.position) - AdditionalDistance;
			
			return distance <= Range.value;
		}
	}
}