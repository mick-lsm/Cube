using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.AI
{
    public class Moveable : MonoBehaviour
    {
        public float speedScale;

        protected Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void Move(int direction)
        {
            if (!enabled) return;
            const float moveSpeed = Constants.EntityMoveSpeed;
            if (Mathf.Abs(rb.velocity.x) < moveSpeed * speedScale)
                rb.AddForce(direction * moveSpeed * speedScale * Vector2.right / Time.fixedDeltaTime * 0.25f);
        }
        public void MoveTo(float xPos)
        {
            var direction = 0;
            float x = transform.position.x;
            if (x < xPos) direction = 1;
            if (xPos < x) direction = -1;
            if (Mathf.Abs(x - xPos) <= 0.5f) direction = 0;
            Move(direction);
        }
    }
}
