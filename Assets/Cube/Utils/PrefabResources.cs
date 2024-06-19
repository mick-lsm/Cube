using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public static class PrefabResources
    {
        public static GameObject GetStraightFlyBulletPrefab() => Resources.Load<GameObject>("Prefabs/StraightFlyBullet");
        public static GameObject GetNavigationMissilePrefab() => Resources.Load<GameObject>("Prefabs/NavigationMissile");
        public static GameObject GetEntityDamageEffectPrefab() => Resources.Load<GameObject>("Prefabs/EntityDamageEffect");
        public static GameObject GetEntityDeathEffectPrefab() => Resources.Load<GameObject>("Prefabs/EntityDeathEffect");
        public static GameObject GetWeaponTraceEffectPrefab() => Resources.Load<GameObject>("Prefabs/WeaponTraceEffect");
        public static GameObject GetLaserOncePrefab() => Resources.Load<GameObject>("Prefabs/LaserOnce");
        public static GameObject GetEntityGeneratingEffectPrefab() => Resources.Load<GameObject>("Prefabs/EntityGeneratingEffect");
        public static GameObject GetBarrelBlowEffect() => Resources.Load<GameObject>("Prefabs/BarrelBlowEffect");
        public static GameObject GetBombEnemyBlowEffect() => Resources.Load<GameObject>("Prefabs/BombEnemyBlowEffect");
        public static GameObject GetBarrel() => Resources.Load<GameObject>("Prefabs/Barrel");
        public static GameObject GetBulletDestroyEffect() => Resources.Load<GameObject>("Prefabs/BulletDestroyEffect");
    }
}
