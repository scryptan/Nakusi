using System.Linq;
using UnityEngine;

namespace BombsTest.Bomb
{
    public class BombStraight: BombBase
    {
        protected override void Explode()
        {
            var colliders = new Collider[20]; // тут магическое число 20 из предположения, что рядом не будет более 20 объектов
            if (Physics.OverlapSphereNonAlloc(transform.position, explodeRadius, colliders) > 0)
            {
                foreach (var npc in colliders.Select(x => x?.GetComponent<Npc.Npc>()).Where(x => x != null))
                    npc.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}