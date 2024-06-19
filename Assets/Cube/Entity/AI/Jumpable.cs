using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.AI
{
    public class Jumpable : MonoBehaviour
    {
        public event Action OnJumped;

        public float jumpForceScale;

        protected Rigidbody2D rb;
        protected Vector3 standardScale;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            standardScale = transform.localScale;
        }

        public void Jump()
        {
            if (!enabled) return;
            var force = Constants.EntityJumpForce * jumpForceScale / Time.fixedDeltaTime;
            var upV = Mathf.Clamp(rb.velocity.y, 0, Mathf.Infinity);
            var value = force / (force + upV / Time.fixedDeltaTime);
            force *= value;

            rb.AddForce(force * Vector2.up);
            var tfm = rb.transform;

            rb.angularVelocity += UnityEngine.Random.Range(-15f, 15f);

            var angleMod = rb.transform.rotation.eulerAngles.z % 180;

            if (angleMod < 45 || angleMod > 135)
                rb.transform.localScale = new Vector2(rb.transform.localScale.x, standardScale.y * 0.3f);
            else
                rb.transform.localScale = new Vector2(standardScale.x * 0.3f, rb.transform.localScale.y);
            OnJumped?.Invoke();
        }
    }
}
