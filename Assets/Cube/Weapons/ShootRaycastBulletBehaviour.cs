using Cube.Components;
using Cube.Entity;
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
    public class ShootRaycastBulletBehaviour : WeaponComponent, IShooter
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
        protected TrailRenderer ren;

        public Action OnShoot { get; set; }

        private void Awake()
        {
            timer = new UnityTimer(attackInterval);
            muzzle = transform.Find("Muzzle");
            ren = GetComponent<TrailRenderer>();
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
        protected void TrailRender(Vector2 start, Vector2 dir, float maxDis)
        {
            StartCoroutine(Effector.CreateWeaponTrace(start, dir, maxDis));
        }
        private IEnumerator Fire()
        {
            for (var i = 0; i < burstShootCount; i++)
            {
                var rays = new RaycastHit2D[10];
                var count = Physics2D.Raycast(muzzle.position, transform.right, new ContactFilter2D { useTriggers = false }, rays);
                var firstcoll = rays.First();
                OnShoot?.Invoke();
                var hit = firstcoll.collider != null ? firstcoll.point : (Vector2)(muzzle.position + transform.right * 100);

                TrailRender(muzzle.position, transform.right, Vector2.Distance(muzzle.position, hit));
                if (firstcoll.collider != null)
                {
                    var other = firstcoll.collider;

                    var teamOther = other.gameObject.GetTeam();
                    var team = gameObject.GetTeam();

                    if (team.IsOpontTo(teamOther))
                    {
                        if (other.TryGetComponent<Knockbackable>(out var knockbackable))
                        {
                            knockbackable.Knockback(new KnockbackArgs { force = ((Vector2)transform.position).DirectionTo2D(other.transform.position) * knockbackForce });
                        }
                        if (other.TryGetComponent<Damageable>(out var damageable))
                        {
                            var args = new DamageArgs();
                            var source = new DamageSource();
                            source.type = KillerType.Entity;
                            source.damager = Main.Owner;
                            args.source = source;
                            args.damage = damage;
                            args.critChance = critChance;
                            damageable.Damage(args);
                        }
                        Destroy(gameObject);
                    }
                }

                yield return new WaitForSeconds(burstShootInterval);
            }
        }
    }
}
