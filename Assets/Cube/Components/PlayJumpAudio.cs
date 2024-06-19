using Cube.Entity.AI;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class PlayJumpAudio : MonoBehaviour
    {
        //public AudioClip[] clip;
        //public AudioSource source;

        private void Awake()
        {
            if (TryGetComponent<Jumpable>(out var jumpable)) jumpable.OnJumped += () =>
            {
                //source.PlayOneShot(clip.PickRandomElement());
                AudioPlayer.PlayJump();
            };
        }
    }
}
