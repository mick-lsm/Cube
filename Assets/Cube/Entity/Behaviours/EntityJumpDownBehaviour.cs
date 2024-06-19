using Cube.Components;
using Cube.Entity.AI;
using Cube.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Cube.Entity.Behaviours
{
    public class EntityJumpDownBehaviour : MonoBehaviour
    {
        public const int Damage = 6;
        public const float Distance = 5;

        protected Rigidbody2D rb;
        protected Jumpable jumpable;
        protected GroundDetectable groundDetectable;
        protected ShiftDownDoable shiftDownDoable;

        protected bool isJumped;
        protected Collider2D coll;

        protected Vector2 playerPos;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
            groundDetectable = GetComponent<GroundDetectable>();
            shiftDownDoable = GetComponent<ShiftDownDoable>();
            jumpable = GetComponent<Jumpable>();
        }
        private void Start()
        {
            StartCoroutine(StartJump());
        }
        private void Update()
        {
            var player = transform.GetNearestPlayer();

            if (player != null) playerPos = player.transform.position;

            if (isJumped && groundDetectable.IsOnGround())
            {
                isJumped = false;
                var count = GameObjectUtility.CreateRoundRaycastCollision(transform.position, 8, out var results);
                for(var i = 0; i < count; i++)
                {
                    var exploded = results[i];
                    if (exploded == coll) continue;
                    var distance = Vector2.Distance(exploded.transform.position, transform.position);
                    var value = Mathf.Clamp01(1 - (distance - 2) / Distance);

                    if (exploded.TryGetComponent<Knockbackable>(out var knockbackable))
                    {
                        knockbackable.Knockback(new KnockbackArgs { force = ((Vector2)transform.position).DirectionTo2D(exploded.transform.position) * 30 * value });
                    }
                    if (exploded.TryGetComponent<Damageable>(out var damageable))
                    {
                        var damageArgs = new DamageArgs();
                        var source = new DamageSource();
                        source.type = KillerType.Entity;
                        source.damager = gameObject;
                        damageArgs.source = source;
                        damageArgs.damage = (int)(Damage * value);
                        damageArgs.critChance = 0;
                        damageable.Damage(damageArgs);
                    }
                }
            }

        }
        private void FixedUpdate()
        {
            var myPos = transform.position;


            if (playerPos.y + 2 < myPos.y && UnityEngine.Random.value < 0.005f)
                StartCoroutine(ShiftDown());
        }
        private IEnumerator StartJump()
        {
            while (true)
            {
                if (groundDetectable.IsOnGround())
                {
                    var direction = 0;
                    var xPos = playerPos.x;
                    float x = transform.position.x;
                    if (x < xPos) direction = 1;
                    if (xPos < x) direction = -1;
                    jumpable.Jump();
                    rb.AddForce(Vector2.right * direction * 8 / Time.fixedDeltaTime);
                    yield return new WaitForSeconds(0.1f);
                    isJumped = true;
                    yield return new WaitForSeconds(2.9f);
                }
                yield return null;
            }
        }
        private IEnumerator ShiftDown()
        {
            var till = Time.time + 0.4f;
            coll.IgnoreToPlatform(true);
            while (till > Time.time)
            {
                shiftDownDoable.ShiftDown();
                yield return null;
            }
            coll.IgnoreToPlatform(false);
        }
    }
}
