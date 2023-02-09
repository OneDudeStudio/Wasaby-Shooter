using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    private float _electricInterval = .2f;
    private float _maxRadius = 10f;
    private int _maxTargets = 15;
    private int _currentConductors;
    private int _maxConductors = 5;

    private bool _isCanStartLightningChain = true;
    private IApplyableEffect[] _targets;

    private AudioSource _source;
    [SerializeField] private AudioClip _zapSound;

    private void Start()
    {
        _targets = new IApplyableEffect[_maxTargets];
        _source = GetComponent<AudioSource>();
    }

    public void TryStartLightningChain(IApplyableDamage victim)
    {
        if (!_isCanStartLightningChain)
            return;

        Transform targetTransform = ((Enemy)victim).transform;
        _isCanStartLightningChain = false;

        Collider[] colliders = new Collider[_maxTargets];
        int collidersNumber = Physics.OverlapSphereNonAlloc(targetTransform.position, _maxRadius, colliders);
        int counter = 0;
        for (int i = 0; i < collidersNumber; i++)
        {
            if (colliders[i].TryGetComponent(out IApplyableEffect effected) && targetTransform != colliders[i].transform)
            {
                _targets[counter++] = effected;
            }
        }
        StartCoroutine(LightningChain());
    }

    private IEnumerator LightningChain()
    {
        _currentConductors = 0;
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i]==null)
                continue;
            if (_currentConductors < _maxConductors)
            {
                _targets[i].StartEffect<Electricity>();
                _currentConductors++;
                yield return new WaitForSeconds(_electricInterval);
            }
            else
                break;
        }
        _targets = new IApplyableEffect[_maxTargets];
        _isCanStartLightningChain = true;
    }
}