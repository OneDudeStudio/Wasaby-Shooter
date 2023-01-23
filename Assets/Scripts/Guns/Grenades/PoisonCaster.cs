using System.Collections.Generic;
using UnityEngine;

public class PoisonCaster : MonoBehaviour
{
    [SerializeField] private Vector3 _boxSize;

    public List<IApplyablePoison> Cast()
    {
        List<IApplyablePoison> victims = new List<IApplyablePoison>();
        Collider[] colliders = Physics.OverlapBox(transform.position, _boxSize);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IApplyablePoison effectTarget))
            {
                victims.Add(effectTarget);
            }
        }
        return victims;
    }
}