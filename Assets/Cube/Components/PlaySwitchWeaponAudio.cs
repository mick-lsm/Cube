using Cube.Inventory;
using Cube.Utils;
using UnityEngine;

namespace Cube.Components
{
    public class PlaySwitchWeaponAudio : MonoBehaviour
    {
        //public AudioClip[] clip;
        //public AudioSource source;

        private void Awake()
        {
            if (TryGetComponent<EntityInventory>(out var inventory)) inventory.OnSwitchWeapon += (a,b) =>
            {
                //source.PlayOneShot(clip.PickRandomElement());
                AudioPlayer.PlaySelect();
            };
        }
    }
}
