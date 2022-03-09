using System.Linq;
using UnityEngine;

namespace BombsTest.Bomb
{
    public class BombNotDamageThroughWalls: BombBase
    {
        protected override void Explode()
        {
            var currentPos = transform.position;
            var colliders = new Collider[20]; // тут магическое число 20 из предположения, что рядом не будет более 20 объектов
            if (Physics.OverlapSphereNonAlloc(currentPos, explodeRadius, colliders) > 0)
            {
                foreach (var npc in colliders.Select(x => x?.GetComponent<Npc.Npc>()).Where(x => x != null))
                {
                    if(Physics.Raycast(new Ray(currentPos, npc.transform.position - currentPos), out var hit, 15) && hit.collider.GetComponent<Npc.Npc>() != null)
                        npc.TakeDamage(damage);
                }
            }
            
            Destroy(gameObject);
        }
    }
}