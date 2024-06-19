using Cube.Components;
using Cube.Weapons.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    [RequireComponent(typeof(IShooter))]
    public class SelfKnockbackOnShootBehaviour : WeaponComponent
    {
        public float force = 2;

        protected Knockbackable ownerKnockbackable;
        protected IShooter shooter;

        protected override void AfterEnter()
        {
            if (!Main.Owner.TryGetComponent<Knockbackable>(out ownerKnockbackable)) throw new ArgumentNullException("Owner " + Main.Owner.name + " is missing a Knockbackable component!");
            if (!Main.TryGetComponent<IShooter>(out shooter)) throw new ArgumentNullException("Weapon " + name + " is missing a IShooter component!");
            shooter.OnShoot += Knockback;
        }

        private void Knockback()
        {
            ownerKnockbackable.Knockback(new KnockbackArgs { force = -transform.right * force });
        }
    }
}
