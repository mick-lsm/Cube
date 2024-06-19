using Cube.Components;
using Cube.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Bullet
{
    public class LaserOnce : MonoBehaviour
    {
        public int damage;
        public float critChance;
        public float knockbackForce;
        public GameObject emitter;

        protected LineRenderer lineRenderer;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
        }
        public void Shoot()
        {
            if(lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null) throw new ArgumentNullException("No line renderer on object!");
            var hit = Physics2D.Raycast(transform.position, transform.right, 50);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider == null) lineRenderer.SetPosition(1, transform.position + transform.right * 50);

            var other = hit.collider;
            if (other != null && other.gameObject.GetTeam().IsOpontTo(emitter.gameObject.GetTeam()))
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
                    source.damager = emitter;
                    args.source = source;
                    args.damage = damage;
                    args.critChance = critChance;
                    damageable.Damage(args);
                }
            }
        }
    }
}
