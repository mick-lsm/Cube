using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class PoolAfterSecondsEveryEnabled : MonoBehaviour
    {
        public float seconds;

        private void OnEnable()
        {
            Invoke(nameof(Pool), seconds);
        }
        private void OnDisable()
        {
            CancelInvoke(nameof(Pool));
        }
        private void Pool()
        {
            //Debug.Log("Destroy - time");
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
