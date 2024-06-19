using Cube.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Cube.Scene
{
    public class BarrelGenerator : MonoBehaviour// Singleton<BarrelGenerator>   
    {
        private void Start()
        {
            StartCoroutine(StartGeneration());
        }
        private IEnumerator StartGeneration()
        {
            yield return new WaitForSeconds(UnityRandom.Range(50, 60));
            var randomVector = new Vector2(UnityRandom.Range(-20, 20), UnityRandom.Range(20, 30));
            var instance = ObjectPool.Instance.GetObject(PrefabResources.GetBarrel());
            instance.transform.position = randomVector;
        }
    }
}
