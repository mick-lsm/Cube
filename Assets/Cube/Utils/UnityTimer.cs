using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public class UnityTimer
    {
        public float interval;
        private float _nextAbleToShootTime;

        public bool Finished => _nextAbleToShootTime <= Time.time;
        public UnityTimer(float interval) 
        {
            this.interval = interval;
        }
        public void Reset() => _nextAbleToShootTime = Time.time + interval;
    }
}
