using Cube.Components;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.Behaviours
{
    public class EntityBombBehaviour : MonoBehaviour
    {
        public const int Damage = 8;
        public const float Distance = 8;

        protected Damageable damageable;
        //protected Collider2D coll;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            //coll = GetComponent<Collider2D>();
        }
        private void Start()
        {
            damageable.OnDeath += OnDeath;
        }

        private void OnDeath(DeathArgs args)
        {
            var count = GameObjectUtility.CreateRoundRaycastCollision(gameObject.transform.position, Distance, out var results);

            for(var i = 0; i < count; i++)
            {
                var exploded = results[i];
                if (exploded.gameObject == gameObject) continue;
                var distance = Vector2.Distance(exploded.transform.position, transform.position);
                var value = Mathf.Clamp01(1 - (distance - 2) / Distance);

                if (exploded.TryGetComponent<Knockbackable>(out var knockbackable))
                {
                    knockbackable.Knockback(new KnockbackArgs { force = ((Vector2)transform.position).DirectionTo2D(exploded.transform.position) * 48 * value});
                }
                if (exploded.TryGetComponent<Damageable>(out var damageable))
                {
                    var damageArgs = new DamageArgs();
                    var source = new DamageSource();
                    source.type = KillerType.Entity;
                    source.damager = gameObject;
                    damageArgs.source = source;
                    damageArgs.damage = (int)(Damage * value);
                    damageArgs.critChance = 0;
                    damageable.Damage(damageArgs);
                }
            }
            gameObject.GenerateBombEnemyBlowEffect();
        }
    }
}
