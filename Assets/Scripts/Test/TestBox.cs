using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestBox : MonoBehaviour, IApplyableDamage, IApplyableFreeze
{
    [SerializeField] private float _health = 10;
    private Renderer _renderer;
    [SerializeField] private Material _hitMaterial;
    private Material _defaultMaterial;

    //private void OnDestroy()
    //{
    //    FindObjectOfType<EffectsController>().RemoveVictim(this);
    //}

    private float _electricDamage = .5f;


    private List<Effect> _applyableEffects = new List<Effect>();

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.material;

        _applyableEffects.Add(new Burning(this));
        _applyableEffects.Add(new Freeze(this));
        _applyableEffects.Add(new Poison(this));
    }

    private bool _isCanApplyDamage = true;
    public bool TryApplyDamage(float damage)
    {
        if (!_isCanApplyDamage)
            return false;
        if (damage < 0)
            return true;
        _health -= damage;
        if (_health <= 0)
        {
            Die();
            _isCanApplyDamage = false;
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
        Destroy(gameObject);
    }

    public void Electric(bool isStartPoint)
    {
        TryApplyDamage(_electricDamage);
        if (isStartPoint)
            GlobalEventManager.SendLightningChain(transform);
    }

    public void ModifySpeed(float modifier)
    {
        
    }

    public void ResetSpeed()
    {
        
    }




    public void StartEffect<T>() where T : Effect
    {
        var effect = _applyableEffects.OfType<T>().FirstOrDefault();
        if (effect != null)
            effect.StartEffect();
    }

    public void ApplyEffects(float currentTime)
    {
        foreach (var item in _applyableEffects)
        {
            item.Apply(currentTime);
        }
    }
}
