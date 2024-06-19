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
    public class ShootSingleBulletBehaviour : WeaponComponent, IShooter, IHasPhysicsProjectile
    {
        public float attackInterval = 1.25f;
        public float speed = 25;
        public int damage = 2;
        public float critChance = 0.05f;
        public float knockbackForce = 3;

        public int burstShootCount = 1;
        public float burstShootInterval = 0;

        public UnityTimer timer;

        protected Transform muzzle;

        public Action OnShoot { get; set; }

        public GameObject LastProjectile { get; private set; }

        private void Awake()
        {
            timer = new UnityTimer(attackInterval);
            muzzle = transform.Find("Muzzle");
        }

        protected override void OnAttacked()
        {
            if (!timer.Finished) return;
            timer.Reset();

            StartCoroutine(Fire());
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            StopCoroutine(Fire());
        }
        private IEnumerator Fire()
        {
            for(var i = 0; i < burstShootCount; i++)
            {
                var bullet = BulletFactory.GetStraightFlyBullet(Main.Owner, speed, damage, critChance, knockbackForce, transform.right, muzzle.position);
                //bullet.transform.position = muzzle.position;

                LastProjectile = bullet.gameObject;
                OnShoot?.Invoke();

                yield return new WaitForSeconds(burstShootInterval);
            }
        }
        //public override WeaponDescription GetDescription()
        //{
        //    var des = new WeaponDescription();
        //    des.sprite = ArtResources.GetPistolSprite();
        //    return des;
        //}
    }
}
