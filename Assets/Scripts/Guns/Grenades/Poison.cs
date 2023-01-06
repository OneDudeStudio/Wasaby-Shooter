using System.Collections;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private int _ticks;
    [SerializeField] private int _interval;
    [SerializeField] private Vector3 _boxSize;

    private void Start()
    {
        StartCoroutine(PoisonCoroutine());
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, _boxSize*2);
    //}

    private IEnumerator PoisonCoroutine()
    {
        
        for (int i = 0; i < _ticks; i++)
        {
            yield return new WaitForSeconds(_interval);
            
            Collider[] colliders = Physics.OverlapBox(transform.position, _boxSize);
            foreach(Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IApplyableEffect effectTarget))
                    effectTarget.ApplyEffect(typeof(IApplyablePoison));
            }
        }
        Destroy(gameObject);
    }
}
