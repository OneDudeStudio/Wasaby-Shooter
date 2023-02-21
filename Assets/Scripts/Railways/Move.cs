using UnityEngine;
using UnityEngine.Serialization;

namespace Railways
{
    public class Move : MonoBehaviour
    {
        public float Speed;
        public GameObject DirectionPoint;

        private void Update()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, DirectionPoint.transform.position, Time.deltaTime * Speed);
        }
    }
}
