using UnityEngine;

public class PoisonSpawner : MonoBehaviour
{
    [SerializeField] private Transform _centralPoint;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Poison _poisonPrefab;
    [SerializeField] private LayerMask _layer;

    public void ProjectSpawnpoints()
    {
        foreach(Transform point in _spawnPoints)
        {
            if (RayCheck(point))
                ProjectPoint(point);    
        }
        ProjectPoint(_centralPoint);
        Destroy(gameObject);
    }

   private void ProjectPoint(Transform point)
   {
        if (Physics.Raycast(point.transform.position, Vector3.down, out RaycastHit hit, 100f, _layer))
            Instantiate(_poisonPrefab, hit.point, Quaternion.identity);
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
