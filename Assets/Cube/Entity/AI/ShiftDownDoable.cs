using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.AI
{
    public class ShiftDownDoable : MonoBehaviour
    {
        protected Vector3 standardScale;
        protected Collider2D coll;

        private bool _shiftDown;
        private void Awake()
        {
            standardScale = transform.localScale;
            coll = GetComponent<Collider2D>();
        }
        private void Update()
        {
            if (!_shiftDown)
            {
                coll.IgnoreToPlatform(false);
            }
            if (_shiftDown)
            {
                _shiftDown = false;
            }
        }
        public void ShiftDown()
        {
            var angleMod = transform.transform.rotation.eulerAngles.z % 180;

            if (angleMod < 45 || angleMod > 135)
                transform.transform.localScale = new Vector2(transform.transform.localScale.x, standardScale.y / 2);
            else
                transform.transform.localScale = new Vector2(standardScale.x / 2, transform.transform.localScale.y);

            _shiftDown = true;
            coll.IgnoreToPlatform(true);
        }
    }
}
