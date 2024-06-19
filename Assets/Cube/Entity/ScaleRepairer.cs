using Cube.Entity.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity
{
    public class ScaleRepairer : MonoBehaviour
    {
        public const float Fix = 0.03f;
        //protected ScaleRepairHelper scaleRepairHelper;
        protected Vector3 standardScale;

        private void Awake()
        {
            //scaleRepairHelper = new ScaleRepairHelper(transform, transform.localScale);
            standardScale = transform.localScale;
        }
        private void FixedUpdate()
        {
            Repair();
        }
        public void Repair()
        {
            if (transform.localScale.y < standardScale.y)
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + Fix);
            if (transform.localScale.x < standardScale.x)
                transform.localScale = new Vector2(transform.localScale.x + Fix, transform.localScale.y);
            var lc = transform.localScale;
            var x = transform.localScale.x;
            var y = transform.localScale.y;
            if (Mathf.Abs(x - standardScale.x) < 0.1f) lc.x = standardScale.x;
            if (Mathf.Abs(y - standardScale.y) < 0.1f) lc.y = standardScale.y;
        }
    }
}
