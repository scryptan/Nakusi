using BombsTest.Common;
using UnityEngine;

namespace BombsTest.Bomb
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class BombBase : ObjectWithConstructor
    {
        [SerializeField] protected float explodeRadius;
        [SerializeField] protected float damage;
        [SerializeField] protected int maxCollidersToInteract = 20; // тут магическое число 20 из предположения, что рядом не будет более 20 объектов
        [SerializeField] private LayerMask maskToExplode;

        protected abstract void Explode();

        private void OnCollisionEnter(Collision collision)
        {
            if (maskToExplode != (maskToExplode | (1 << collision.gameObject.layer)))
                return;

            Explode();
        }
    }
}