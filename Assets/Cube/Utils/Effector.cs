using Cinemachine;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;

namespace Cube.Utils
{
    public static class Effector
    {
        private static Func<float, float> PeriodicFunction = new((x) =>
        {
            var value = x * 3.14f;
            var sin1 = Mathf.Sin(value);
            var sin2 = Mathf.Sin(value * 4);
            var sin3 = Mathf.Sin(value * 8);
            return (sin1 + sin2 + sin3) * 0.36f;
        });
        

        private static GameObject EntityDamageEffect = PrefabResources.GetEntityDamageEffectPrefab();
        private static GameObject EntityDeathEffect = PrefabResources.GetEntityDeathEffectPrefab();
        private static GameObject WeaponTraceEffect = PrefabResources.GetWeaponTraceEffectPrefab();
        private static GameObject BarrelBlowEffect = PrefabResources.GetBarrelBlowEffect();
        private static GameObject BombEnemyBlowEffect = PrefabResources.GetBombEnemyBlowEffect();
        private static GameObject BulletDestroyEffect = PrefabResources.GetBulletDestroyEffect();

        private static CinemachineVirtualCamera VirtualCamera;
        private static CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelsPerlin;
        public static void GenerateEntityDamageEffect(this GameObject gameObject)
        {
            var ede = ObjectPool.Instance.GetObject(EntityDamageEffect);
            ede.transform.position = gameObject.transform.position;
            var color = gameObject.GetComponent<SpriteRenderer>().color;
            var p = ede.GetComponent<ParticleSystem>();
            var main = p.main;
            main.startColor = color;
        }
        public static void GenerateEntityDeathEffect(this GameObject gameObject)
        {
            var ede = ObjectPool.Instance.GetObject(EntityDeathEffect);
            ede.transform.position = gameObject.transform.position;
            var color = gameObject.GetComponent<SpriteRenderer>().color;
            var p = ede.GetComponent<ParticleSystem>();
            var main = p.main;
            main.startColor = color;
        }
        public static void GenerateBulletDestroyEffect(this GameObject gameObject)
        {
            var ede = ObjectPool.Instance.GetObject(BulletDestroyEffect);
            ede.transform.position = gameObject.transform.position;
            var color = gameObject.GetComponent<SpriteRenderer>().color;
            var p = ede.GetComponent<ParticleSystem>();
            var main = p.main;
            main.startColor = color;
        }
        public static void GenerateBombEnemyBlowEffect(this GameObject gameObject)
        {
            var effect = ObjectPool.Instance.GetObject(BombEnemyBlowEffect);
            effect.transform.position = gameObject.transform.position;
        }
        public static void GenerateBarrelBlowEffect(Vector2 pos)
        {
            var effect = ObjectPool.Instance.GetObject(BarrelBlowEffect);
            effect.transform.position = pos;
        }
        public static void ShakeCamera(float force, float duration)
        {
            if (VirtualCamera == null) VirtualCamera = UnityObject.FindObjectOfType<CinemachineVirtualCamera>();
            if (VirtualCamera != null)
            {
                CinemachineBasicMultiChannelsPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                if(CinemachineBasicMultiChannelsPerlin != null)
                {
                    VirtualCamera.StartCoroutine(ShakeCameraEnumerator(force, duration));
                }
            }
        }
        private static IEnumerator ShakeCameraEnumerator(float force, float duration)
        {
            //var origin = (Vector2)VirtualCamera.transform.position;

            var till = Time.time + duration;
            //var start = Time.time;

            //const float interval = 0.05f;

            //var ranFuncX = GenerateRandomPeriodicFunction(duration, force);
            //var resultsX = GetAllInterval(interval, ranFuncX);

            //var ranFuncY = GenerateRandomPeriodicFunction(duration, force);
            //var resultsY = GetAllInterval(interval, ranFuncY);

            //Debug.Log("Start " + Time.time);
            //for(var i = 0; i < resultsX.Length; i++)
            //{
            //    VirtualCamera.transform.position += new Vector3(resultsX[i], resultsY[i]);
            //    yield return new WaitForSeconds(interval);
            //}
            //Debug.Log("End " + Time.time);

            while (till > Time.time)
            {
                VirtualCamera.transform.position += new Vector3(UnityRandom.Range(-force,force),UnityRandom.Range(-force,force));
                yield return null;
                //VirtualCamera.transform.position = origin + Random.insideUnitCircle * Random.Range(0, force);
                //VirtualCamera.transform.position = new Vector3(VirtualCamera.transform.position.x, VirtualCamera.transform.position.y, -60);
                //yield return null;
            }
        }
        public static IEnumerator CreateWeaponTrace(Vector2 start, Vector2 dir, float maxDis)
        {
            var effect = ObjectPool.Instance.GetObject(WeaponTraceEffect);
            var ren = effect.GetComponent<TrailRenderer>();

            ren.enabled = false;
            effect.transform.position = start;
            ren.enabled = true;

            const float speed = 150;
            var startTime = Time.time;

            var mag = maxDis;
            var curMag = mag;

            while(curMag <= mag)
            {
                var value = Time.time - startTime;
                effect.transform.position = start + value * dir * speed;
                curMag = Vector2.Distance(effect.transform.position, start);
                yield return null;
            }
            yield return new WaitForSeconds(ren.time);
            ObjectPool.Instance.PushObject(effect);
        }


        //private static Func<float, float> GenerateRandomPeriodicFunction(float xScale, float yScale)
        //{
        //    var ran1 = UnityRandom.Range(-yScale, yScale);
        //    var ran2 = UnityRandom.Range(-yScale, yScale);
        //    var ran3 = UnityRandom.Range(-yScale, yScale);
        //    var ran4 = UnityRandom.Range(-yScale, yScale);
        //    var ran5 = UnityRandom.Range(-yScale, yScale);

        //    var func = new Func<float, float>(x =>
        //    {
        //        var value = x * 3.14f / xScale;
        //        var sin1 = Mathf.Sin(value * ran1);
        //        var sin2 = Mathf.Sin(value * ran2);
        //        var sin3 = Mathf.Sin(value * ran3);
        //        var sin4 = Mathf.Sin(value * ran4);
        //        var sin5 = Mathf.Sin(value * ran5);

        //        return sin1 + sin2 + sin3 + sin4 + sin5;
        //    });
        //    return func;
        //}
        //private static float[] GetAllInterval(float interval, Func<float, float> func)
        //{
        //    var count = (int)(1 / interval);
        //    var results = new float[count];
        //    for(var i = 0; i < count; i++)
        //    {
        //        var value = func(i * interval);
        //        var nextValue = func(i * interval + interval);

        //        var difference = nextValue - value;

        //        results[i] = difference;
        //    }
        //    return results;
        //}
    }
}
