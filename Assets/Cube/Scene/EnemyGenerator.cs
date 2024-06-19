using Cube.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    [Serializable]
    public struct EnemyGenerationData
    {
        public GameObject prefab;
        public float generateStartAtTime;
        public float weight;

    }
    public class EnemyGenerator : MonoBehaviour
    {
        public float spawnsPerSeconds;
        public EnemyGenerationData[] enemies;

        private float _startTime;

        private void Awake()
        {
            //base.Awake();
            _startTime = Time.time + 0.001f;
        }
        private void Start()
        {
            StartCoroutine(nameof(StartTryingGeneration));
        }
        private IEnumerator StartTryingGeneration()
        {
            yield return null;
            yield return null;
            while (true)
            {
                if (UnityEngine.Random.value <= spawnsPerSeconds / 10 - SceneManager.Instance.enemies.Count * 0.01f)
                RandomGeneration();
                yield return new WaitForSeconds(0.1f);
            }
        }
        private void RandomGeneration()
        {
            //var time = Time.time;
            var pass = Time.time - _startTime;
            var oks = enemies.Where(d => pass >= d.generateStartAtTime);

            if (oks.Count() == 0) return;

            var totalWeight = oks.Sum(d => d.weight);
            var ran = UnityEngine.Random.Range(0, totalWeight);
            var chance = 0f;
            EnemyGenerationData selected = default;

            foreach(var okData in oks)
            {
                chance += okData.weight;
                //ran -= okData.weight;
                if(ran <= chance)
                {
                    selected = okData;
                    break;
                }
            }
            //if(ran < chance) selected = oks.Last();

            GenerateEntity(selected);
        }
        //private IEnumerator StartGeneration()
        //{
        //    yield return new WaitForSeconds(3f);
        //    while (true)
        //    {
        //        if(UnityEngine.Random.value <= 0.4f)
        //        {
        //            var ranEle = enemies[UnityEngine.Random.Range(0, enemies.Length)];
        //            GenerateEntity(ranEle.GetComponent<SpriteRenderer>().color, ranEle);
        //        }

        //        yield return new WaitForSeconds(1f);
        //    }
        //}
        public GameObject GenerateEntity(EnemyGenerationData data)
        {
            var instance = ObjectPool.Instance.GetObject(PrefabResources.GetEntityGeneratingEffectPrefab());
            
            var generating = instance.GetComponent<GeneratingEntity>();
            //generating.Color = data.color;
            generating.prefab = data.prefab;

            //Debug.Log(data.prefab);

            var ps = instance.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = data.prefab.GetComponent<SpriteRenderer>().color;

            var pos = Vector2.zero;
            if(SceneManager.Instance.spawnPoints.Count != 0) pos = SceneManager.Instance.spawnPoints[UnityEngine.Random.Range(0, SceneManager.Instance.spawnPoints.Count - 1)];
            instance.transform.position = pos;

            instance.name = instance.name.Remove(instance.name.Length - 7);
            

            return instance;
        }

    }
}
