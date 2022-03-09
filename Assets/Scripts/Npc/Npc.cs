using System;
using BombsTest.Common;
using UnityEngine;

namespace BombsTest.Npc
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Npc : ObjectWithConstructor, IDamageable
    {
        private Health Health { get; set; }

        [SerializeField] private float startHealth;
        [SerializeField] private float minHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public override void Init()
        {
            Health = new Health(startHealth, minHealth, maxHealth);
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        private void Update()
        {
            currentHealth = Health.Value;
        }

        private void Start()
        {
            Init();
        }

        public void TakeDamage(float amount)
        {
            Health.Subtract(amount);
            if (Health.Value <= Health.MinValue)
                Destroy(gameObject);
        }
        
        public void TakeHeal(float amount)
        {
            Health.Add(amount);
        }
    }
}