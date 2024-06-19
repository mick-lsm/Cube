using Cube.Components;
using Cube.Entity.AI;
using Cube.Inputs;
using Cube.Inventory;
using Cube.Registry;
using Cube.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Entity.Behaviours
{
    public class EntityPlayerBehaviour : MonoBehaviour
    {
        public PlayerInput input;

        //public float speedScale;
        //public float jumpForceScale;

        public int jumpCount;

        protected Moveable moveable;
        protected Jumpable jumpable;
        protected InteractionDoable interactionDoable;
        protected ShiftDownDoable shiftDownDoable;
        protected GroundDetectable groundDetectable;

        //protected MoveHelper moveHelper;
        //protected JumpHelper jumpHelper;
        //protected GroundDetectHelper groundDetect;
        //protected InteractHelper interactHelper;
        //protected ShiftDownHelper shiftDownHelper;

        private EntityInventory _inventory;
        private Collider2D _coll;

        private void Awake()
        {
            var rb = GetComponent<Rigidbody2D>();
            var coll = GetComponent<Collider2D>();
            this._coll = coll;
            _inventory = GetComponent<EntityInventory>();
            moveable = GetComponent<Moveable>();
            jumpable = GetComponent<Jumpable>();
            interactionDoable = GetComponent<InteractionDoable>();
            shiftDownDoable = GetComponent<ShiftDownDoable>();
            groundDetectable = GetComponent<GroundDetectable>();

            //moveHelper = new MoveHelper(rb);
            //jumpHelper = new JumpHelper(rb, transform.localScale);
            //groundDetect = new GroundDetectHelper(coll);
            //interactHelper = new InteractHelper(transform);
            //shiftDownHelper = new ShiftDownHelper(transform, transform.localScale);
        }
       
        private void Update()
        {
            if (input.jump && jumpCount > 0)
            {
                jumpCount--;
                jumpable.Jump();
            }
            if (input.interact && !input.holdShield)
            {
                interactionDoable.Interact();
            }
            interactionDoable.SelectSurround();
            if(input.switchWeapon != 0 && !input.holdShield)
            {
                _inventory.SwitchWeapon(input.switchWeapon);
            }
            if(_inventory.CurrentWeapon != null)
            {
                var weapon = _inventory.CurrentWeapon;
                if (input.attack) weapon.Attack();
                weapon.TargetAt(input.targetAt);
            }
            if (input.shift)
            {
                shiftDownDoable.ShiftDown();
                _coll.IgnoreToPlatform(true);
            }
            if(_inventory.CurrentWeapon != null)
            {
                if (input.holdShield)
                {
                    _inventory.CurrentWeapon.gameObject.SetActive(false);
                }
                else
                {
                    _inventory.CurrentWeapon.gameObject.SetActive(true);
                }
            }
        }
        private float _groundDetectAbleTime;
        private void FixedUpdate()
        {
            var moveDirection = 0;
            if (input.moveValue > 0) moveDirection = 1;
            if (input.moveValue < 0) moveDirection = -1;
            moveable.Move(moveDirection);

            if (groundDetectable.IsOnGround() && _groundDetectAbleTime <= Time.time)
            {
                jumpCount = Constants.PlayerJumpCountMax;
                _groundDetectAbleTime = Time.time + 0.2f;
            }
        }
    }
}