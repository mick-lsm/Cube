using System;
using System.Linq;
using Cube.Utils;
using Cube.Weapons.Behaviours;
using UnityEngine;

namespace Cube.Weapons
{
    public class ShootMultiNavigationMissileBehaviour : WeaponComponent, IShooter, IHasPhysicsProjectile
    {
        public float attackInterval = 1.4f;

        public float speed = 30;
        public float lerp = 0.6f;
        public int damage = 4;
        public float critChance = 0.2f;
        public float knockbackForce = 6;

        public int missileCount = 3;
        public float missileAngleRange = 45;
        public UnityTimer timer;

        private Vector2 _targetPos;
        //
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

            var angles = GameObjectUtility.EvenlyDistributeLinear1On0(missileCount).Select(s => s * missileAngleRange);
            var owner = Main.Owner;
            foreach(var angle in angles)
            {
                var missile = BulletFactory.GetNavigateMissile(Main.Owner, speed, damage, critChance, knockbackForce, _targetPos, 0.6f, muzzle.position);
                //missile.transform.position = muzzle.position;
                var ea = missile.transform.eulerAngles;
                ea.z = angle + transform.eulerAngles.z;
                missile.transform.eulerAngles = ea;

                LastProjectile = missile.gameObject;
                OnShoot?.Invoke();
            }
        }

        protected override void OnTargetAt(Vector2 look)
        {
            _targetPos = look;
        }
    }
}
