using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    private float _electricInterval = .2f;
    private float _maxRadius = 10f;

    private bool _isCanStartLightningChain = true;

    private EnemyController _enemyController;
    private AudioSource _source;
    [SerializeField] private AudioClip _zapSound;
    [SerializeField] private Lightning _lightningPrefab;
    [SerializeField] private LayerMask _obstacles;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    public void TryStartLightningChain(IApplyableDamage victim)
    {
        if (!_isCanStartLightningChain)
            return;

        Transform firstTargetTransform = ((MonoBehaviour)victim).transform;
        _isCanStartLightningChain = false;

        List<Transform> targets = _enemyController.AffectAreaVictims(firstTargetTransform.position, _maxRadius);
        targets.Remove(firstTargetTransform);

        if (targets.Count == 0)
            return;

        StartCoroutine(LightningChain(firstTargetTransform, targets));
    }

    private IEnumerator LightningChain(Transform firstTarget, List<Transform> targets)
    {
        Lightning lightning = Instantiate(_lightningPrefab, firstTarget.position, Quaternion.identity);
        foreach (var target in targets)
        {
            if (lightning == null)
            {
                break;
            }

            if (target != null && Physics.Raycast(lightning.transform.position, (target.position - lightning.transform.position).normalized, _maxRadius, _obstacles))
            {
                continue;
            }

            lightning.FollowTarget(target);
            yield return new WaitForSeconds(_electricInterval);
        }
        if (lightning != null)
        {
            Destroy(lightning.gameObject);
        }
        _isCanStartLightningChain = true;
    }
}