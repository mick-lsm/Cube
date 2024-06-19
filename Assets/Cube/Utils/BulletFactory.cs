using Cube.Bullet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public static class BulletFactory
    {
        private static GameObject StraightFlyBulletPrefab = PrefabResources.GetStraightFlyBulletPrefab();
        private static GameObject NavigationMissileLauncher = PrefabResources.GetNavigationMissilePrefab();
        private static GameObject LaserOncePrefab = PrefabResources.GetLaserOncePrefab();

        public static StraightFlyBullet GetStraightFlyBullet(GameObject emitter, float speed, int damage, float critChance, float knockbackForce, Vector2 direction, Vector2 pos)
        {
            var instance = ObjectPool.Instance.GetObject(StraightFlyBulletPrefab);
            var sfb=instance.GetComponent<StraightFlyBullet>();
            sfb.emitter = emitter;
            sfb.speed = speed;
            sfb.damage = damage;
            sfb.critChance = critChance;
            sfb.knockbackForce = knockbackForce;
            sfb.team = emitter.gameObject.GetTeam();
            //sfb.direction = direction;
            sfb.transform.right = direction;

            var ren = instance.GetComponent<SpriteRenderer>();
            ren.color = emitter.GetRepresentativeColor();

            var tRen = instance.GetComponent<TrailRenderer>();

            tRen.emitting = false;
            tRen.startColor = ren.color;
            tRen.endColor = ren.color;
            instance.transform.position = pos;
            sfb.StartCoroutine(ResetTrailRendererEmitting(tRen));
            //tRen.emitting = true;

            return sfb;
        }
        public static NavigateMissile GetNavigateMissile(GameObject emitter, float speed, int damage, float critChance, float knockbackForce, Vector2 targetPos, float lerp, Vector2 pos)
        {
            var instance = ObjectPool.Instance.GetObject(NavigationMissileLauncher);
            //instance.SetActive(false);
            var sfb = instance.GetComponent<NavigateMissile>();
            sfb.emitter = emitter;
            sfb.speed = speed;
            sfb.damage = damage;
            sfb.critChance = critChance;
            sfb.knockbackForce = knockbackForce;
            sfb.targetPos = targetPos;
            sfb.team = emitter.gameObject.GetTeam();
            sfb.lerp = lerp;

            var ren = instance.GetComponent<SpriteRenderer>();
            ren.color = emitter.GetRepresentativeColor();

            var tRen = instance.GetComponent<TrailRenderer>();

            //tRen.enabled = false;
            tRen.emitting = false;
            tRen.startColor = ren.color;
            tRen.endColor = ren.color;
            instance.transform.position = pos;
            sfb.StartCoroutine(ResetTrailRendererEmitting(tRen));
            //tRen.enabled = true;
            //instance.SetActive(true);

            return sfb;
        }
        private static IEnumerator ResetTrailRendererEmitting(TrailRenderer tRen)
        {
            yield return null;
            tRen.emitting = true;
        }
        public static LaserOnce GetLaserOnce(GameObject emitter, int damage, float critChance, float knockbackForce, Vector2 start, Vector2 direction)
        {
            var instance = ObjectPool.Instance.GetObject(LaserOncePrefab);
            var lo = instance.GetComponent<LaserOnce>();
            lo.emitter = emitter;
            lo.damage = damage;
            lo.critChance = critChance;
            lo.knockbackForce = knockbackForce;
            lo.transform.position = start;
            lo.transform.right = direction;

            return lo;
        }
    }
}
