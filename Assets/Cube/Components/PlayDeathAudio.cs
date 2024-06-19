using Cube.Utils;
using UnityEngine;

namespace Cube.Components
{
    public class PlayDeathAudio : MonoBehaviour
    {
        //public AudioClip[] clip;
        //public AudioSource source;

        private void Awake()
        {
            if (TryGetComponent<Damageable>(out var damageable)) damageable.OnDeath += (a) =>
            {
                AudioPlayer.PlayDeath();
                //source.PlayOneShot(clip.PickRandomElement());
            };
        }
    }
}
