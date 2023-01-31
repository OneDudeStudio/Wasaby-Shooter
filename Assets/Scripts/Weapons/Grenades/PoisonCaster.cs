using System.Collections.Generic;
using UnityEngine;

public class PoisonCaster : MonoBehaviour
{
    [SerializeField] private Vector3 _boxSize;

    public List<IApplyableEffect> Cast()
    {
        List<IApplyableEffect> victims = new List<IApplyableEffect>();
        Collider[] colliders = Physics.OverlapBox(transform.position, _boxSize);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IApplyableEffect effectTarget))
            {
                victims.Add(effectTarget);
            }
        }
        return victims;
    }
}