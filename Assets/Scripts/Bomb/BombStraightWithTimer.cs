using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BombsTest.Bomb
{
    public class BombStraightWithTimer: BombBase
    {
        [SerializeField]
        private float secondsToExplode;
        private bool m_StartedTimer;
        private List<Npc.Npc> npcsToDamage;
        protected override void Explode()
        {
            if(m_StartedTimer)
                return;
            
            var colliders = new Collider[20]; // тут магическое число 20 из предположения, что рядом не будет более 20 объектов
            if (Physics.OverlapSphereNonAlloc(transform.position, explodeRadius, colliders) > 0)
            {
#pragma warning disable CS4014
                StartToExplode();
#pragma warning restore CS4014
                npcsToDamage = colliders.Select(x => x.GetComponent<Npc.Npc>()).Where(x => x != null).ToList();
            }
        }

        private async Task StartToExplode()
        {
            m_StartedTimer = true;
            await Task.Delay(TimeSpan.FromSeconds(secondsToExplode));
            foreach (var npc in npcsToDamage)
            {
                npc.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }
}