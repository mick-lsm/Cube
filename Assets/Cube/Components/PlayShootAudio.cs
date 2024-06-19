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
    public class PlayShootAudio : MonoBehaviour
    {
        private void Awake()
        {
            if (TryGetComponent<IShooter>(out var shooter)) shooter.OnShoot += () =>
            {
                AudioPlayer.PlayShoot();
                //source.PlayOneShot(clip.PickRandomElement());
            };
        }
    }
}
