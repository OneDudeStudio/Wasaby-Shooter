using System;
using System.Collections.Generic;
using UnityEngine;

public enum ImpactType
{
    Stone,
    Wood,
    Metal,
    Flesh,
    Default
}

public class MaterialChoise : MonoBehaviour
{
    [SerializeField] private Material[] _stone;
    [SerializeField] private Material[] _wood;
    [SerializeField] private Material[] _metal;
    [SerializeField] private Material[] _flesh;

    private static PoolsController _poolsController;

    public PoolsController PoolsController => _poolsController;

    private Dictionary<Material, Func<ImpactParticle>> _particlesDictionary = new Dictionary<Material, Func<ImpactParticle>>();

    private void Start()
    {
        _poolsController = GetComponent<PoolsController>();
        FillParticlesDictionary();
    }

    private void FillParticlesDictionary()
    {
        Material[][] materials = new[] { _stone, _wood, _metal, _flesh };
        Func<ImpactParticle>[] particles = new Func<ImpactParticle>[]
        {
            () => _poolsController.GetHitImpact(ImpactType.Stone),
            () => _poolsController.GetHitImpact(ImpactType.Wood),
            () => _poolsController.GetHitImpact(ImpactType.Metal),
            () => _poolsController.GetHitImpact(ImpactType.Flesh)
        };

        for (int i = 0; i < materials.Length; i++)
        {
            Material[] materialType = materials[i];

            foreach (Material singleMAterial in materialType)
            {
                _particlesDictionary.Add(singleMAterial, particles[i]);
            }
        }
    }

    public ImpactParticle GetMaterialsParticles(Material material) =>
    _particlesDictionary.ContainsKey(material) ? _particlesDictionary[material]() : _poolsController.GetHitImpact(ImpactType.Default);
}
