using Cube.Components;
using Cube.Entity.AI;
using Cube.Inventory;
using Cube.Registry;
using Cube.Utils;
using System.Collections;
using UnityEngine;

namespace Cube.Entity.Behaviours
{
    public class EntityLandNavigationBehaviour : MonoBehaviour
    {
        //public float speedScale;
        //public float jumpForceScale;

        //public int jumpCount;
        //public int jumpCountMax;

        protected Moveable moveable;
        protected Jumpable jumpable;
        protected GroundDetectable groundDetectable;
        protected ShiftDownDoable shiftDownDoable;

        //protected MoveHelper moveHelper;
        //protected JumpHelper jumpHelper;
        //protected GroundDetectHelper groundDetect;
        //protected ShiftDownHelper shiftDownHelper;

        private Collider2D _coll;

        [SerializeField]
        private Vector2 _playerPos;

        private void Awake()
        {
            var rb = GetComponent<Rigidbody2D>();
            var coll = GetComponent<Collider2D>();
            this._coll = coll;

            moveable = GetComponent<Moveable>();
            jumpable = GetComponent<Jumpable>();
            groundDetectable = GetComponent<GroundDetectable>();
            shiftDownDoable = GetComponent<ShiftDownDoable>();

            //moveHelper = new MoveHelper(rb);
            //jumpHelper = new JumpHelper(rb, transform.localScale);
            //groundDetect = new GroundDetectHelper(coll);
            //shiftDownHelper = new ShiftDownHelper(transform, transform.localScale);
        }

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
            var player = transform.GetNearestPlayer();
            if (player != null) _playerPos = player.transform.position;
        }
        private void FixedUpdate()
        {
            var playerX = _playerPos.x;
            var myPos = transform.position;

            moveable.MoveTo(playerX);

            if (groundDetectable.IsOnGround() && UnityEngine.Random.value < 0.005f)
            {
                if (_playerPos.y - 2 > myPos.y) jumpable.Jump();
            }
            if (_playerPos.y + 2 < myPos.y && UnityEngine.Random.value < 0.005f)
                StartCoroutine(ShiftDown());
        }
        private IEnumerator ShiftDown()
        {
            var till = Time.time + 0.4f;
            _coll.IgnoreToPlatform(true);
            while (till > Time.time)
            {
                shiftDownDoable.ShiftDown();
                yield return null;
            }
            _coll.IgnoreToPlatform(false);
        }
    }
}
