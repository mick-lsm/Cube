using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class GeneratingEntity : MonoBehaviour
    {
        public GameObject prefab;

        //protected ParticleSystem ps;
        //public Color Color
        //{
        //    get => ps.main.startColor.color;
        //    set
        //    {
        //        var main = ps.main;
        //        main.startColor = value;
        //    }
        //}

        //private void Awake()
        //{
        //    ps = GetComponent<ParticleSystem>();
        //}
        private void Start()
        {
            StartCoroutine(StartGeneration());
        }
        private IEnumerator StartGeneration()
        {
            yield return new WaitForSeconds(1.5f);
            var instance = Instantiate(prefab);
            instance.name = instance.name.Remove(instance.name.Length - 7);
            instance.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
