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
    public class NavigateMissile : MonoBehaviour
    {
        public float lerp;
        public float speed;
        public int damage;
        public float critChance;
        public float knockbackForce;
        public GameObject emitter;
        public Vector2 targetPos;
        public Team team;

        private Rigidbody2D rb;

        private bool _isArrived;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            GetComponent<Collider2D>().IgnoreToPlatform(true);
        }
        protected virtual void OnEnable()
        {
            _isArrived = false;
        }
        protected virtual void FixedUpdate()
        {
            if (_isArrived) return;
            var direction = (targetPos - (Vector2)transform.position).normalized;

            transform.right = Vector3.Slerp(transform.right, direction, lerp / Vector2.Distance(transform.position, targetPos));
            rb.velocity = transform.right * speed;

            _isArrived = Vector2.Distance(targetPos, transform.position) < 1.5f;
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
        //        //ObjectPool.Instance.PushObject(gameObject);
        //        //Destroy(gameObject);
        //    }
        //    if(other.gameObject != gameObject && other.gameObject != emitter) ObjectPool.Instance.PushObject(gameObject);
        //    //else if (other.gameObject.IsObstacle()) ObjectPool.Instance.PushObject(gameObject); 
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
                //Destroy(gameObject);
            }
            else if (other.gameObject.IsObstacle()) ObjectPool.Instance.PushObject(gameObject);
            //Destroy(gameObject);
        }
    }
}
