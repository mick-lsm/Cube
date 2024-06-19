using UnityEngine;

namespace Cube.Inputs
{
    public struct PlayerInput
    {
        public float moveValue;
        public bool jump;
        public bool attack;
        public bool interact;
        public int switchWeapon;
        public bool holdShield;
        public bool shift;
        public Vector2 targetAt;
    }
}
