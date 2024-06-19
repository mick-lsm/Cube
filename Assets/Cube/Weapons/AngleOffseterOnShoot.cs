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
    public class AngleOffseterOnShoot : WeaponComponent
    {
        public float angleBoundary = 10;

        protected IShooter shooter;
        protected IHasPhysicsProjectile hasPhysicsProjectile;

        protected override void AfterEnter()
        {
            if (!Main.TryGetComponent(out shooter)) throw new ArgumentNullException("Weapon " + name + " is missing a IShooter component!");
            if (!Main.TryGetComponent(out hasPhysicsProjectile)) throw new ArgumentNullException("Weapon " + name + " is missing a IHasPhysicsProjectile component!");

            shooter.OnShoot += OnShoot;
        }

        private void OnShoot()
        {
            var ea = hasPhysicsProjectile.LastProjectile.transform.eulerAngles;
            ea.z += UnityEngine.Random.Range(-angleBoundary, angleBoundary);
            hasPhysicsProjectile.LastProjectile.transform.eulerAngles = ea;
        }
    }
}
