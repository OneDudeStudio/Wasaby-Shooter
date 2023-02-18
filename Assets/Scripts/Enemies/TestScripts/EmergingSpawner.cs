using UnityEngine;
using UnityEngine.AI;

namespace Enemies.TestScripts
{
    public class EmergingSpawner : MonoBehaviour
    {
        [SerializeField] private bool _build;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        
        private void Update()
        {
            if (_build)
            {
                _build = false;
                //_navMeshSurface.BuildNavMesh();
                //_navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
            }
        }
    }
}
