using Cube.Components;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class Lava : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.IsDamageable())
            {
                if(collision.TryGetComponent<Damageable>(out var damageable))
                {
                    damageable.Damage(new DamageArgs { critChance=0, damage = 1000, source = new DamageSource { damager = null, type = KillerType.Lava } });
                }
            }
        }
    }
}
