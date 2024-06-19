using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public static class EntityToScoreTranslation
    {
        public static int GetEntityScore(GameObject entity)
        {
            if (entity.name == "EntityPistolEnemy") return 100;
            if (entity.name == "EntityMP5Enemy") return 200;
            if (entity.name == "EntityShotgunEnemy") return 350;
            if (entity.name == "EntityBombEnemy") return 600;
            if (entity.name == "EntityFlyEnemy") return 100;
            if (entity.name == "EntitySniperEnemy") return 400;
            if (entity.name == "EntityJumpDownEnemy") return 800;
            return 0;
            //throw new ArgumentOutOfRangeException("No score for this entity - " + entity.name);
        }
    }
}
