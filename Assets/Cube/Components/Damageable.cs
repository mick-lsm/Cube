using Cube.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class Damageable : MonoBehaviour
    {
        public event Action<DamageArgs> OnDamaged;
        public event Action<DeathArgs> OnDeath;

        protected Health _health;

        protected virtual void Awake()
        {
            _health = GetComponent<Health>();
        }

        public virtual void Damage(DamageArgs args)
        {
            var crited = UnityEngine.Random.value < args.critChance;
            var damage = args.damage;
            if (crited) damage *= 2;
            _health.currentHealth -= damage;
            OnDamaged?.Invoke(args);
            if (_health.currentHealth <= 0)
            {
                var dArgs = new DeathArgs { source = args.source };
                //Debug.Log(gameObject.name + " is killed by " + args.source.damager.name);
                if (args.source.type == KillerType.Entity && args.source.damager != null) args.source.damager.GetComponent<KillOtherDoable>()?.Killed(this);
                OnDeath?.Invoke(dArgs);
                Destroy(gameObject);
            }
        }
    }
}
