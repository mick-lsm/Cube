using Cube.Components;
using Cube.Entity;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Bullet
{
    public class StraightFlyBullet : MonoBehaviour
    {
        public Team team;
        public float speed;
        public int damage;
        public float critChance;
        public float knockbackForce;
        public GameObject emitter;
        //public Vector2 direction;

        private Rigidbody2D _rigidbody2D;

        //private Vector2 dir;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            GetComponent<Collider2D>().IgnoreToPlatform(true);
        }
        //protected virtual void OnEnable()
        //{
        //    dir = transform.right;
        //}
        protected virtual void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.right * speed;
        }

        //protected virtual void OnCollisionEnter2D(Collision2D other)
        //{
        //    var teamOther = other.gameObject.GetTeam();
        //    if (team.IsOpontTo(teamOther))
        //    {
        //        if (other.gameObject.TryGetComponent<Knockbackable>(out var knockbackable))
        //        {
        //            knockbackable.Knockback(new KnockbackArgs { force = ((Vector2)transform.position).DirectionTo2D(other.transform.position) * knockbackForce });
        //        }
        //        if (other.gameObject.TryGetComponent<Damageable>(out var damageable))
        //        {
        //            var args = new DamageArgs();
        //            var source = new DamageSource();
        //            source.type = Source.Entity;
        //            source.entity = emitter;
        //            args.source = source;
        //            args.damage = damage;
        //            args.critChance = critChance;
        //            damageable.Damage(args);
        //        }
        //        ObjectPool.Instance.PushObject(gameObject); 
        //        //Destroy(gameObject);
        //    }
        //    else if (other.gameObject.IsObstacle())
        //    {
        //        ObjectPool.Instance.PushObject(gameObject);
        //    }
        //    //Destroy(gameObject);
        //}
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var teamOther = other.gameObject.GetTeam();
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
                    source.damager = emitter;
                    args.source = source;
                    args.damage = damage;
                    args.critChance = critChance;
                    damageable.Damage(args);
                }
                ObjectPool.Instance.PushObject(gameObject);
                Effector.GenerateBulletDestroyEffect(gameObject);
                //Destroy(gameObject);
            }
            else if (other.gameObject.IsObstacle())
            {
                Effector.GenerateBulletDestroyEffect(gameObject);
                ObjectPool.Instance.PushObject(gameObject);
            }
            //Destroy(gameObject);
        }
    }
}
