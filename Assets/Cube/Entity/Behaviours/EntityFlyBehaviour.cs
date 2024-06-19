using Cube.Components;
using Cube.Entity.AI;
using Cube.Registry;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.Behaviours
{
    public class EntityFlyBehaviour : MonoBehaviour
    {
        protected Moveable moveable;
        protected Jumpable jumpable;

        //protected MoveHelper moveHelper;
        //protected JumpHelper jumpHelper;
        //protected ShiftDownHelper shiftDownHelper;

        private Collider2D _coll;

        [SerializeField]
        private Vector2 _playerPos;

        private void Awake()
        {
            var coll = GetComponent<Collider2D>();
            _coll = coll;

            moveable = GetComponent<Moveable>();
            jumpable = GetComponent <Jumpable>();

            //moveHelper = new MoveHelper(rb);
            //jumpHelper = new JumpHelper(rb, transform.localScale);
            //shiftDownHelper = new ShiftDownHelper(transform, transform.localScale);
        }

        //private void Start()
        //{
        //    _coll.IgnoreToPlatform(true);
        //}

        //private void OnEnable()
        //{
        //    GameSceneRegister.RegisterEnemy(gameObject);
        //}
        //private void OnDisable()
        //{
        //    GameSceneRegister.UnregisterEnemy(gameObject);

        //}
        private void Update()
        {
            _coll.IgnoreToPlatform(true);
            var player = transform.GetNearestPlayer();
            if (player != null) _playerPos = player.transform.position;
        }
        private void FixedUpdate()
        {
            var playerX = _playerPos.x;
            var myPos = transform.position;

            moveable.MoveTo(playerX);

            if (playerX + 3 > myPos.x && UnityEngine.Random.value < 0.04f)
            {
                jumpable.Jump();
            }
        }
    }
}
