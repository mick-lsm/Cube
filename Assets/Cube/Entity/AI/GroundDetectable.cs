using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.AI
{
    public class GroundDetectable : MonoBehaviour
    {
        protected Collider2D coll;
        protected bool grounded;
        public void Awake()
        {
            coll = GetComponent<Collider2D>();
        }
        public bool IsOnGround()
        {
            return grounded;
            //var colliders2 = new Collider2D[2];
            //var count = Physics2D.OverlapCircleNonAlloc((Vector2)coll.transform.position - new Vector2(0, coll.transform.localScale.y / 2f), 0.1f, colliders2);
            //return count > 1;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var contactPoint = collision.GetContact(0).point;
            if (contactPoint.y < transform.position.y)
            {
                grounded = true;
                StartCoroutine(nameof(ResetGrounded));
            }
        }
        private IEnumerator ResetGrounded()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            grounded = false;
        }
    }
}
