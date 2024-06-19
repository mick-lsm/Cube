using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Utils
{
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        private readonly Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();
        private GameObject _pool;
        public static ObjectPool Instance => _instance;//??= new ObjectPool();

        private void Awake()
        {
            _instance = this;
        }
        public GameObject GetObject(GameObject prefab)
        {
            GameObject gameObject;
            if (!_objectPool.ContainsKey(prefab.name) || _objectPool[prefab.name].Count == 0)
            {
                if (_pool == null)
                    _pool = new GameObject("ObjectPool");
                var childPool = GameObject.Find(prefab.name + "Pool");
                if (!childPool)
                {
                    childPool = new GameObject(prefab.name + "Pool");
                    childPool.transform.SetParent(_pool.transform);
                }
                gameObject = UnityEngine.Object.Instantiate(prefab);
                PushObject(gameObject);
            }
            gameObject = _objectPool[prefab.name].Dequeue();
            if (gameObject == null) GetObject(prefab);
            gameObject.transform.SetParent(null);
            gameObject.SetActive(true);
            return gameObject;
        }
        public GameObject GetObject(string name, Func<GameObject> create)
        {
            GameObject gameObject;
            if (!_objectPool.ContainsKey(name) || _objectPool[name].Count == 0)
            {
                if (_pool == null)
                    _pool = new GameObject("ObjectPool");
                var childPool = GameObject.Find(name + "Pool");
                if (!childPool)
                {
                    childPool = new GameObject(name + "Pool");
                    childPool.transform.SetParent(_pool.transform);
                }
                gameObject = create.Invoke();
                PushObject(gameObject);
            }
            gameObject = _objectPool[name].Dequeue();
            gameObject.SetActive(true);
            gameObject.transform.SetParent(null);
            return gameObject;
        }

        public void PushObject(GameObject prefab)
        {
            if (_pool == null)
                _pool = new GameObject("ObjectPool");
            var name = prefab.name.Replace("(Clone)", string.Empty);
            if (!_objectPool.ContainsKey(name))
                _objectPool.Add(name, new Queue<GameObject>());
            _objectPool[name].Enqueue(prefab);
            var childPool = GameObject.Find(name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(name + "Pool");
                childPool.transform.SetParent(_pool.transform);
            }

            prefab.transform.SetParent(childPool.transform);
            prefab.SetActive(false);
        }
    }
}
