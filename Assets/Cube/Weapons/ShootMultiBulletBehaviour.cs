using Cube.Utils;
using Cube.Weapons.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    public class ShootMultiBulletBehaviour : WeaponComponent, IShooter, IHasPhysicsProjectile
    {
        public float speed = 25;
        public int damage = 2;
        public float critChance = 0.05f;
        public float knockbackForce = 3;

        public int count;
        public float maxAngle;

        public UnityTimer timer;

        protected Transform muzzle;

        public Action OnShoot { get; set; }

        public GameObject LastProjectile { get; private set; }

        private void Awake()
        {
            timer = new UnityTimer(1.25f);
            muzzle = transform.Find("Muzzle");
        }

        protected override void OnAttacked()
        {
            if (!timer.Finished) return;
            timer.Reset();

            var points = GameObjectUtility.EvenlyDistributeLinear1On0(count);
            foreach(var point in points)
            {
                var angle = point * maxAngle;
                var bullet = BulletFactory.GetStraightFlyBullet(Main.Owner, speed, damage, critChance, knockbackForce, transform.right, muzzle.position);
                //bullet.transform.position = muzzle.position;

                //print(point);

                var ea = bullet.transform.eulerAngles;
                ea.z += angle;
                bullet.transform.eulerAngles = ea;

                LastProjectile = bullet.gameObject;
                OnShoot?.Invoke();
            }
        }

    }
}
