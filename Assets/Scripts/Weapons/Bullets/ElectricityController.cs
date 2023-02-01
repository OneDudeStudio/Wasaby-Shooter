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
    private List<IApplyableEffect> _targetsList = new List<IApplyableEffect>();
    private IApplyableEffect[] _targets;
    private IApplyableEffect _tmpTarget;

    private AudioSource _source;
    [SerializeField] private AudioClip _zapSound;
    private void Start()
    {
        _targets = new IApplyableEffect[_maxTargets];
        GlobalEventManager.OnStartLightningChain.AddListener(TryStartLightningChain);
        //GlobalEventManager.OnDie.AddListener(DestroyObject);
        _source = GetComponent<AudioSource>();
    }
    public void TryStartLightningChain(Transform targetTransform)
    {
        if (!_isCanStartLightningChain)
            return;
        _isCanStartLightningChain = false;
        Collider[] colliders = new Collider[_maxTargets];
        int collidersNumber = Physics.OverlapSphereNonAlloc(targetTransform.position, _maxRadius, colliders);
        int counter = 0;
        for (int i = 0; i < collidersNumber; i++)
        {
            if (colliders[i].TryGetComponent(out IApplyableEffect effected))
            {
                _targetsList.Add(effected);
                _targets[counter++] = effected;
            }
        }
        StartCoroutine(LightningChain());
    }

    private IEnumerator LightningChain()
    {
        _currentConductors = 0;
        Shuffle();
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i]==null)
                continue;
            if (_targets[i] is IApplyableElectric electric && _currentConductors < _maxConductors)
            {
                _source.PlayOneShot(_zapSound);
                electric.Electric(false);
                _currentConductors++;
                yield return new WaitForSeconds(_electricInterval);
            }
            else
                break;
        }
        _targetsList.Clear();
        _targets = new IApplyableEffect[_maxTargets];
        _isCanStartLightningChain = true;
    }
    private void DestroyObject(Transform destroyable)
    {
        IApplyableEffect effectable = destroyable.GetComponent<IApplyableEffect>();
        if (_targetsList.Contains(effectable))
            _targetsList.Remove(effectable);
    }

    public void Shuffle()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            int rnd = Random.Range(0, _targets.Length);
            _tmpTarget = _targets[rnd];
            _targets[rnd] = _targets[i];
            _targets[i] = _tmpTarget;
        }
    }
}