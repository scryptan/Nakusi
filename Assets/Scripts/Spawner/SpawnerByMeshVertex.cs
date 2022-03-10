using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BombsTest.Spawner
{
    public class SpawnerByMeshVertex : SpawnerBase
    {
        [SerializeField] [Range(0, 1)] private float boundsScaleFact = 0.9f;

        [SerializeField] private GameObject floor;
        [SerializeField] private LayerMask wallMask;
        [SerializeField] private float additionalHeightToSpawn;

        private MeshFilter m_FloorMeshFilter;
        private List<Vector3> m_Positions = new List<Vector3>();

        // На самом деле думаю спавнер и действия бомб можно было сделать удобнее и обстрактнее, но под текущую задачу кажется и такого хватит
        
        private void Start()
        {
            m_FloorMeshFilter = floor.GetComponent<MeshFilter>() ??
                                throw new ArgumentException($"{nameof(floor)} has no MeshFilter component");

            var bounds = m_FloorMeshFilter.sharedMesh.bounds;
            bounds.extents *= boundsScaleFact;

            m_Positions = GetPositionsForSpawn(bounds);
        }

        protected override Vector3? GetPosition()
        {
            var colliders = new Collider[1];
            var pos = m_Positions[Random.Range(0, m_Positions.Count)];

            if (Physics.OverlapSphereNonAlloc(pos, 0.1f, colliders) > 0)
                return null;

            return pos;
        }

        protected override Quaternion? GetRotation() => Quaternion.identity;

        private Vector3 GetScaledVertexPosition(Vector3 vertex)
        {
            var scaleFactor = floor.transform.localScale;
            return new Vector3(vertex.x * scaleFactor.x, vertex.y * scaleFactor.y, vertex.z * scaleFactor.z);
        }

        private List<Vector3> GetPositionsForSpawn(Bounds bounds)
        {
            var overlappingColliders = new Collider[1];
            var localPositions = new List<Vector3>();

            foreach (var position in m_FloorMeshFilter.sharedMesh.vertices.Where(bounds.Contains))
            {
                var resultPoint = GetScaledVertexPosition(position) + SpawnObjectHalfHeight;

                if (Physics.OverlapBoxNonAlloc(resultPoint, spawnObject.transform.localScale / 2, overlappingColliders,
                        Quaternion.identity, wallMask) == 0)
                {
                    localPositions.Add(resultPoint + Vector3.up * additionalHeightToSpawn);
                }
            }

            return localPositions;
        }
    }
}