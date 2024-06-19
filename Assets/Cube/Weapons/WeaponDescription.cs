using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    [Serializable]
    public struct WeaponDescription
    {
        public string name;
        public string description;
        public int damage;
        public float critChance;
        public float speedSlowDown;
        public Sprite sprite;
    }
}
