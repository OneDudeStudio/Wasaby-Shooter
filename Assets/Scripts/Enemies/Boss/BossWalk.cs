using PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : MonoBehaviour
{
    [SerializeField] private Transform _boss;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    private bool _isAnimated = false;

    private Animator _animator;
    private Transform _player;

    private void Start()
    {
        _animator = _boss.GetComponent<Animator>();
        _animator.SetBool("IsWalking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAnimated)
            return;
        if (other.TryGetComponent(out PlayerManager player))
        {
            _player = other.transform;
            _animator.SetBool("IsWalking", true);
            StartWalking();
            _isAnimated = true;
        }
    }

    public void StartWalking()
    {
        StartCoroutine(Walking(_boss, _target.position, _duration));
    }

    private IEnumerator Walking(Transform obj, Vector3 target, float duration)
    {
        Vector3 startPos = obj.position;
        float t = 0;
        while(t<1)
        {
            obj.position = Vector3.Lerp(startPos, target, t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        
        _animator.SetBool("IsWalking", false);
        Debug.Log(_player.position);
    }
}
