using Cube.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Registry
{
    public class RegisterAsEnemy : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.Instance.RegisterEnemy(gameObject);
            //GameSceneRegister.RegisterEnemy(gameObject);
        }
        private void OnDisable()
        {
            SceneManager.Instance.UnregisterEnemy(gameObject);
            //GameSceneRegister.UnregisterEnemy(gameObject);

        }
    }
}
