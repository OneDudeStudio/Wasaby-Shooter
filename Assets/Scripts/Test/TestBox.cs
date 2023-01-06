using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour, IApplyableDamage, IApplyableBurning, IApplyableFreeze, IApplyablePoison, IapplyableElectric
{
    [SerializeField] private float _health = 10;
    private Renderer _renderer;
    [SerializeField] private Material _hitMaterial;
    private Material _defaultMaterial;

    public bool _isBurning = false;
    private int _ticks = 5;
    private float _interval = .5f;
    private float _fireDamage = 1f;

    private bool _isFreeze = false;
    private float _freezeDuration = 3f;
    private float _freezeSpeedModifier = .5f;

    private bool _isCanPoisoned = true;
    private float _poisonDamage = 1f;
    private float _poisonInterval = .8f;

    private Dictionary<Type, bool> _isApplyableEffect = new Dictionary<Type, bool>();

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.material;
        CheckEffects();
    }

    public bool TryApplyDamage(float damage)
    {
        if (damage < 0)
            return true;
        _health -= damage;
        if (_health <= 0)
        {
            Die();
            return false;
        }
        StartCoroutine(hit());
        return true;
    }

    private IEnumerator hit()
    {
        _renderer.material = _hitMaterial;
        yield return new WaitForSeconds(.05f);
        _renderer.material = _defaultMaterial;
    }

    public void Die()
    {
        GlobalEventManager.SendDie(transform);
        gameObject.SetActive(false);
        /// 
        Destroy(gameObject);
    }

    public void ApplyEffect(Type type)
    {
        if (type == typeof(IApplyableBurning) && _isApplyableEffect.ContainsKey(type))
            StartBurning();
        if (type == typeof(IApplyableFreeze) && _isApplyableEffect.ContainsKey(type))
            StartFreeze();
        if (type == typeof(IApplyablePoison) && _isApplyableEffect.ContainsKey(type))
            Poison(_poisonDamage);
    }

    public void StartFreeze()
    {
        StartCoroutine(Freeze());
    }
    
    private IEnumerator Freeze()
    {
        _isFreeze = true;
        //
        yield return new WaitForSeconds(_freezeDuration);
        _isFreeze = false;
    }

    public void StartBurning()
    {
        if (_isBurning)
            StopCoroutine("Burning");
        StartCoroutine("Burning");
        _isBurning = true;
    }

    private IEnumerator Burning()
    {
        for (int i = 0; i < _ticks; i++)
        {
            yield return new WaitForSeconds(_interval);
            TryApplyDamage(_fireDamage);
        }
        _isBurning = false;
    }
    

    private void CheckEffects()
    {
        foreach (Type type in GetType().GetInterfaces())
        {
            _isApplyableEffect.Add(type, true);
        }        
    }

    public void Poison(float damage)
    {
        if (_isCanPoisoned)
        {
            StartCoroutine(PoisonInterval());
            TryApplyDamage(damage);            
        }
    }
    private IEnumerator PoisonInterval()
    {
        _isCanPoisoned = false;
        yield return new WaitForSeconds(_poisonInterval);
        _isCanPoisoned = true;
    }

    public void Electric()
    {
        throw new NotImplementedException();
    }
}
