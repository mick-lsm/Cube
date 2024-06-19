using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class Knockbackable : MonoBehaviour
    {
        public event Action<KnockbackArgs> OnKnockbacked;

        protected Rigidbody2D _rigidbody2D;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void Knockback(KnockbackArgs args)
        {
            _rigidbody2D.AddForce(args.force / Time.fixedDeltaTime);

            OnKnockbacked?.Invoke(args);
        }
    }
}
