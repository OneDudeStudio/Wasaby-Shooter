using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Transform> _victims = new List<Transform>();

    public void AddVictim(Transform victim)
    {
        if (!_victims.Contains(victim))
        {
            _victims.Add(victim);
        }
    }

    public void DestroyVictim(Transform victim)
    {
        _victims.Remove(victim);
        Destroy(victim.gameObject);
    }

    public List<Transform> AffectAreaVictims(Vector3 center, float radius)
    {
        List<Transform> inAreaVictims = new List<Transform>();

        foreach(var victim in _victims)
        {
            if((victim.position+Vector3.up*2 - center).sqrMagnitude < radius * radius)
            {
                inAreaVictims.Add(victim);
            }
        }
        return inAreaVictims;
    } 
}
