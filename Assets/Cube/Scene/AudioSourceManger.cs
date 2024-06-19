using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class AudioSourceManger : MonoBehaviour
    {
        public AudioSource source;
        public static AudioSourceManger Instance { get; private set; }
        private void Awake()
        {
            Instance = this;   
            source = GetComponent<AudioSource>();
        }
        public void PlayAudio(AudioClip clip) 
        {
            if (source == null) return;
            source.PlayOneShot(clip); 
        }
    }
}
