using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    private int _health;
    private float _duration;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IApplyableEffect victim))
        {
            _health--;
            victim.StartEffect<Electricity>();
        }
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EffectsConfig config = FindObjectOfType<ConfigsLoader>().RootConfig.EffectsConfig;
        _health = config.Electric.MaxConductors;
        _duration = config.Electric.Interval;
    }

    public void FollowTarget(Transform taret)
    {
        StartCoroutine(MoveToTarget(taret));
    }

    private IEnumerator MoveToTarget(Transform taret)
    {
        Vector3 startPosition = transform.position;
        float t = 0;

        while (t < 1)
        {
            if (taret == null)
            {
                break;
            }

            transform.position = Vector3.Lerp(startPosition, taret.position + Vector3.up * 2, t); ;
            t += Time.deltaTime / _duration;
            yield return null;
        }
    }
}
