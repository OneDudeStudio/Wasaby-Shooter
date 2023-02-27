using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    [SerializeField] private Transform _centralPoint;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PoisonCaster _poisonPrefab;
    [SerializeField] private LayerMask _layer;

    private List<PoisonCaster> _poisonCasters = new List<PoisonCaster>();
    private int _ticks;
    private float _interval;


    private void Start()
    {
        EffectsConfig config = FindObjectOfType<ConfigsLoader>().RootConfig.EffectsConfig;
        _ticks = config.Poison.Ticks;
        _interval = config.Poison.Interval;
        ProjectSpawnpoints();
    }

    private IEnumerator PoisonCoroutine()
    {
        for (int i = 0; i < _ticks; i++)
        {
            yield return new WaitForSeconds(_interval);
            List<IApplyableEffect> victims = new List<IApplyableEffect>();

            foreach (PoisonCaster caster in _poisonCasters)
            {
                if (caster)
                {
                    caster.Cast().ForEach(item =>
                    {
                        if (!victims.Contains(item))
                        {
                            victims.Add(item);
                        }
                    });
                }
            }

            foreach (IApplyableEffect victim in victims)
            {
                victim.StartEffect<Poison>();
            }
        }

        foreach (PoisonCaster caster in _poisonCasters)
        {
            Destroy(caster.gameObject);
        }

        Destroy(gameObject);
    }

    private void ProjectSpawnpoints()
    {
        foreach (Transform point in _spawnPoints)
        {
            if (RayCheck(point))
            {
                ProjectPoint(point);
            }
        }
        ProjectPoint(_centralPoint);
        StartCoroutine(PoisonCoroutine());
    }

    private IEnumerator DelayedPoisonSpawn(float delay, RaycastHit hit)
    {
        yield return new WaitForSeconds(delay);
        _poisonCasters.Add(Instantiate(_poisonPrefab, hit.point + hit.normal * .1f, Quaternion.LookRotation(hit.normal)));
    }

    private void ProjectPoint(Transform point)
    {
        if (Physics.Raycast(point.position, Vector3.down, out RaycastHit hit, 60f, _layer))
        {
            StartCoroutine(DelayedPoisonSpawn(hit.distance/20f, hit));
        }
    }

    private bool RayCheck(Transform point)
    {
        Vector3 direction = (point.position - _centralPoint.position).normalized;
        if (Physics.Raycast(_centralPoint.position, direction, direction.magnitude, _layer))
        {
            Destroy(point.gameObject);
            return false;
        }
        return true;
    }
}
