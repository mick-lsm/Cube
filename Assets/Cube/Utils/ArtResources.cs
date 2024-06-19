using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public static class ArtResources
    {
        public static Sprite GetPistolSprite() => Resources.Load<Sprite>("Arts/Pistol");
        public static Sprite GetMP5Sprite() => Resources.Load<Sprite>("Arts/Fox");
        public static Sprite GetRocketLauncherSprite() => Resources.Load<Sprite>("Arts/RocketLauncher");
    }
}
