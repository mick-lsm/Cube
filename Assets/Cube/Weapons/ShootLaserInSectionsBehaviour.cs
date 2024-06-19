using Cube.Utils;
using Cube.Weapons.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    public class ShootLaserInSectionsBehaviour : WeaponComponent, IShooter
    {
        public float attackInterval;

        public int damage;
        public float critChance;
        public float knockbackForce;

        public UnityTimer timer;

        protected Transform muzzle;

        public Action OnShoot { get; set; }

        private void Awake()
        {
            timer = new UnityTimer(attackInterval);
            muzzle = transform.Find("Muzzle");
        }
        protected override void OnAttacked()
        {
            if (!timer.Finished) return;
            timer.Reset();

            var laser = BulletFactory.GetLaserOnce(Main.Owner, damage, critChance, knockbackForce, muzzle.position, transform.right);
            laser.transform.position = muzzle.position;
            laser.Shoot();
            OnShoot?.Invoke();
        }
    }
}
