using Cube.Inventory;
using Cube.Utils;
using UnityEngine;

namespace Cube.Components
{
    public class PlayPickupWeaponAudio : MonoBehaviour
    {
        //public AudioClip[] clip;
        //public AudioSource source;

        private void Awake()
        {
            if (TryGetComponent<EntityInventory>(out var inventory)) inventory.OnJoinedWeapon += a =>
            {
                //source.PlayOneShot(clip.PickRandomElement());
                AudioPlayer.PlayPickup();
            };
        }
    }
}
