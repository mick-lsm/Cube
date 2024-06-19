using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {
        private static T _instance;
        public static T Instance => _instance;//{ get; private set; }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
