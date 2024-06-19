using Cube.Utils;
using UnityEngine;

namespace Cube.Components
{
    public class PlayHurtAudio : MonoBehaviour 
    {
        //public AudioClip[] clip;
        //public AudioSource source;

        private void Awake()
        {
            if (TryGetComponent<Damageable>(out var damageable)) damageable.OnDamaged += a =>
            {
                AudioPlayer.PlayHurt();
                //source.PlayOneShot(clip.PickRandomElement());
            };
        }
    }

}
