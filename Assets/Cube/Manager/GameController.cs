using UnityEngine;
using Cube.Inputs;
using Cube.Utils;
using Cube.Entity.Behaviours;

namespace Cube.Managers
{
    public sealed class GameController : MonoBehaviour
    {
        public EntityPlayerBehaviour player;

        public Camera cam;

        private void Awake()
        {
            //base.Awake();
            cam = Camera.main;
        }
        private void Update()
        {
            var input = new PlayerInput();

            input.interact = Input.GetKeyDown(KeyCode.E);
            input.moveValue = Input.GetAxisRaw("Horizontal");
            input.attack = Input.GetMouseButton(0);
            input.jump = Input.GetKeyDown(KeyCode.Space);
            input.shift = Input.GetKey(KeyCode.LeftShift);
            input.holdShield = Input.GetMouseButton(1);
            input.switchWeapon = Input.GetKeyDown(KeyCode.R) ? 1 : 0;
            input.targetAt = cam.ScreenToWorldPoint(Input.mousePosition);

            player.input = input;
        }
    }
}
