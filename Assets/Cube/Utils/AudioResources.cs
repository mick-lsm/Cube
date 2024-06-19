using UnityEngine;

namespace Cube.Utils
{
    public static class AudioResources
    {
        public static AudioClip Get(string name) => Resources.Load<AudioClip>("Audio/" + name);
        public static AudioClip GetHurt1() => Get("hurt1");
        public static AudioClip GetHurt2() => Get("hurt2");
        public static AudioClip GetHurt3() => Get("hurt3");
        public static AudioClip GetJump1() => Get("jump1");
        public static AudioClip GetJump2() => Get("jump2");
        public static AudioClip GetPickUp1() => Get("pickup1");
        public static AudioClip GetPickUp2() => Get("pickup2");
        public static AudioClip GetSelect1() => Get("select1");
        public static AudioClip GetDeath1() => Get("death1");
        public static AudioClip GetShoot1() => Get("shoot1");
        public static AudioClip GetBulletDestroy() => Get("collide1");
    }
}
