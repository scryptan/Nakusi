using BombsTest.Spawner;
using UnityEngine;

namespace BombsTest.Helpers
{
    public class TimerSpawnerExecutioner : MonoBehaviour
    {
        [SerializeField] private SpawnerBase spawner;
        [SerializeField] private float timeToSpawn;
        private float countedTime;

        private void Update()
        {
            countedTime += Time.deltaTime;
            if (countedTime > timeToSpawn)
            {
                spawner.Spawn();
                countedTime = 0;
            }
        }
    }
}