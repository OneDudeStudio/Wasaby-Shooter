using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectsController : MonoBehaviour
{
    private int _segments = 10;
    private List<IApplyableEffect> _victims = new List<IApplyableEffect>();


    private int counter = 0;

    private void Start()
    {
        TestBox[] targets = FindObjectsOfType<TestBox>();
        foreach(TestBox target in targets)
        {
            if(target is IApplyableEffect ef)
            {
                AddVictim(ef);
            }
        }
    }



    private void Update()
    {
        ApplyEffectsToSegment(counter);
        counter = (counter +1 ) % _segments;
    }

    private void ApplyEffectsToSegment(int segmentNum)
    {
        int segmentLength = _victims.Count / _segments;
        int segmentFirstIndex = segmentLength * segmentNum;
        int segmentLastIndex = (segmentNum == _segments - 1) ? _victims.Count : (segmentFirstIndex + segmentLength);
        float currentTime = Time.time;
        for (int i = segmentFirstIndex; i < segmentLastIndex; i++)
        {
            _victims[i].ApplyEffects(currentTime);
        }
    }

   

    public void AddVictim(IApplyableEffect victim)
    {
        if (!_victims.Contains(victim))
        {
            _victims.Add(victim);
        }
    }

    public void RemoveVictim(IApplyableEffect victim)
    {
        if (_victims.Contains(victim))
        {
            _victims.Remove(victim);
        }
    }

}
