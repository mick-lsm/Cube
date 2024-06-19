using Cube.Utils;
using Cube.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class PlayBulletDestroyAudio : MonoBehaviour
    {
        private void OnDisable()
        {
            AudioPlayer.PlayBulletDestroy();
        }
    }
}
