using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{

    public enum KillerType
    {
        Void,
        Entity,
        Lava,
    }
    public struct DamageSource
    {
        //public enum Source
        //{
        //    Void,
        //    Entity,
        //    Lava,
        //}
        public KillerType type;
        public GameObject damager;
    }
    public struct DamageArgs
    {
        public DamageSource source;
        public int damage;
        public float critChance;
    }
    public struct DeathArgs
    {
        public DamageSource source;
    }
    public struct KnockbackArgs
    {
        public Vector2 force;
    }
}
