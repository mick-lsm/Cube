using Cube.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Registry
{
    public class RegisterAsPlayer : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.Instance.RegisterPlayer(gameObject);
            //GameSceneRegister.RegisterPlayer(gameObject);
        }
        private void OnDisable()
        {
            SceneManager.Instance.UnregisterPlayer(gameObject);
            //GameSceneRegister.UnregisterPlayer(gameObject);
        }
    }
}
