using BombsTest.Common;
using UnityEngine;

namespace BombsTest.Spawner
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [SerializeField] protected ObjectWithConstructor spawnObject;
        [SerializeField] protected Transform objectsStorage;

        private void Awake()
        {
            SpawnObjectHalfHeight = spawnObject.transform.localScale;
            SpawnObjectHalfHeight.Scale(Vector3.up / 2);
        }

        protected Vector3 SpawnObjectHalfHeight;

        protected abstract Vector3? GetPosition();
        protected abstract Quaternion? GetRotation();

        public void Spawn()
        {
            var position = GetPosition();
            var rotation = GetRotation();
            
            if(position == null || rotation == null)
                return;
            
            var spawned = Instantiate(spawnObject, (Vector3)position!, (Quaternion)rotation!, objectsStorage);
            spawned.Init();
        }
    }
}