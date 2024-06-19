using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class Floating : MonoBehaviour
    {
        public float speed = 2f;

        protected Vector3 origin;

        private void Start()
        {
            origin = transform.position;
            StartCoroutine(StartFloating());
        }
        private System.Collections.IEnumerator StartFloating()
        {
            var ran1 = UnityEngine.Random.value;
            while (true)
            {
                var pos = origin;
                pos.y += Mathf.Sin(Time.time * speed + ran1) * 0.5f;
                transform.position = pos;

                yield return null;
            }
        }
    }
}
