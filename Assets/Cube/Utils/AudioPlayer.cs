using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Cube.Utils.AudioResources;
using static Cube.Scene.AudioSourceManger;

namespace Cube.Utils
{
    public static class AudioPlayer
    {
        public static AudioClip[] Hurts = new AudioClip[] { GetHurt1(), GetHurt2(), GetHurt3() };
        public static AudioClip[] Jumps = new AudioClip[] { GetJump1(), GetJump2() };
        public static AudioClip[] Pickups = new AudioClip[] { GetPickUp1(), GetPickUp2() };
        public static AudioClip[] Deaths = new AudioClip[] { GetDeath1() };
        public static AudioClip[] Selects = new AudioClip[] { GetSelect1() };
        public static AudioClip[] Shoots = new AudioClip[] { GetShoot1() };
        public static AudioClip[] BulletDestroy = new AudioClip[] { GetBulletDestroy() };

        public static void PlayHurt() => Instance.PlayAudio(Hurts.PickRandomElement());
        public static void PlayJump() => Instance.PlayAudio(Jumps.PickRandomElement());
        public static void PlayPickup() => Instance.PlayAudio(Pickups.PickRandomElement());
        public static void PlayDeath() => Instance.PlayAudio(Deaths.PickRandomElement());
        public static void PlaySelect() => Instance.PlayAudio(Selects.PickRandomElement());
        public static void PlayShoot() => Instance.PlayAudio(Shoots.PickRandomElement());
        public static void PlayBulletDestroy() => Instance.PlayAudio(BulletDestroy.PickRandomElement());
    }
}
