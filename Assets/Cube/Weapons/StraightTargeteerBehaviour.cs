using Cube.Weapons.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    public class StraightTargeteerBehaviour : WeaponComponent
    {
        protected override void OnTargetAt(Vector2 look)
        {
            Main.DefaultTargetAt(look);
        }
    }
}
