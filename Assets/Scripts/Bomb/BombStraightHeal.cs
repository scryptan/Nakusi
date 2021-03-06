using System.Linq;
using UnityEngine;

namespace BombsTest.Bomb
{
    public class BombStraightHeal: BombBase
    {
        protected override void Explode()
        {
            var colliders = new Collider[maxCollidersToInteract];
            if (Physics.OverlapSphereNonAlloc(transform.position, explodeRadius, colliders) > 0)
            {
                foreach (var npc in colliders.Select(x => x.GetComponent<Npc.Npc>()).Where(x => x != null))
                    npc.TakeHeal(damage);
            }
            
            Destroy(gameObject);
        }
    }
}