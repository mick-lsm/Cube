using Cube.Entity.AI;
using Cube.Entity.Behaviours;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    public class WeaponBehaviour : MonoBehaviour
    {
        public WeaponDescription description;

        public event Action AfterEntered;
        public event Action BeforeExited;
        public event Action<Vector2> OnTargetAt;
        public event Action OnAttacked;
      

        public GameObject Owner => transform.root.gameObject;

        public float speedScale;

        protected virtual void Awake() { }
        public virtual void AfterEnter() 
        {
            if(Owner.TryGetComponent<Moveable>(out var moveable))
            {
                moveable.speedScale *= 1 - speedScale;
            }
            AfterEntered?.Invoke();
        }
        public virtual void BeforeExit() 
        {
            if (Owner.TryGetComponent<Moveable>(out var moveable))
            {
                moveable.speedScale /= 1 - speedScale;
            }
            BeforeExited?.Invoke();
        }
        public virtual void Attack()
        {
            OnAttacked?.Invoke();
        }
        public virtual void TargetAt(Vector2 look)
        {
            OnTargetAt?.Invoke(look);
        }

        public WeaponDescription GetDescription()
        {
            return description;
        }
        public void DefaultTargetAt(Vector2 look)
        {
            transform.TargetAt2D(look);

            var flip = 1;
            var flipped = Mathf.Sign(transform.localScale.y) == -1;
            if (flipped && transform.position.x < look.x) flip = -1;
            if (!flipped && look.x < transform.position.x) flip = -1;
            if(flip == -1)
            {
                var lc = transform.localScale;
                lc.y *= flip;
                transform.localScale = lc;
            }
        }
    }
}
