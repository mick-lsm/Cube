using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class MenuCameraEffector : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(StartDeathTextBehaviour());
        }
        private IEnumerator StartDeathTextBehaviour()
        {
            var ran1 = UnityEngine.Random.value;
            var ran2 = UnityEngine.Random.value;
            var ran3 = UnityEngine.Random.value;
            while (true)
            {
                var ea = transform.eulerAngles;
                ea.z = Mathf.Sin(Time.time * 2 + ran1) * 1.5f;
                transform.eulerAngles = ea;

                var pos = transform.position;
                pos.x = Mathf.Sin(Time.time * ran2) * 1.4f;
                pos.y = Mathf.Sin(Time.time * 0.8f * ran3) * 0.6f;
                pos.z = transform.position.z;
                transform.position = pos;

                yield return null;
            }
        }
    }
}
