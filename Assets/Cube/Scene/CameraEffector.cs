using Cinemachine;
using Cube.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class CameraEffector : MonoBehaviour
    {
        public Transform player;

        private CinemachineVirtualCamera _cvc;

        private float defaultViewSize;

        public float smoothTime;

        private float _velocity = 3f;
        private Vector3 _velocity3 = Vector3.one * 3;
        private Vector2 _playerPos;
        private Vector2 ClosestMonsterPosition
        {
            get
            {
                var monster = SceneManager.Instance.enemies.OrderBy(m => Vector2.Distance(m.transform.position, _playerPos)).FirstOrDefault();
                //GameSceneRegister.Enemies.OrderBy(m => Vector2.Distance(m.transform.position, _playerPos)).FirstOrDefault();

                if (monster != null)
                {
                    return monster.transform.position;
                }
                
                return Vector2.zero;
            }
        }

        private void Start()
        {
            _cvc = GetComponent<CinemachineVirtualCamera>();
            defaultViewSize = _cvc.m_Lens.OrthographicSize;
            _cvc.m_Lens.OrthographicSize = 1;
        }
        private void Update()
        {
            if (player != null)
            {
                _playerPos = player.transform.position;
            }

            //var origin = Vector2.zero;
            //var distance = (_playerPos - origin);
            //var scaled = distance / 1.2f;

            //var pos = transform.position;
            //pos.x = Mathf.Lerp(pos.x, scaled.x, 2.4f);
            //pos.y = Mathf.Lerp(pos.y, scaled.y, 2.4f);
            //transform.position = pos;

            //var value = Vector2.Distance(distance, origin) * 1.2f;
            //value = Mathf.Clamp(value, 8, defaultViewSize * 2);
            //_cvc.m_Lens.OrthographicSize = Mathf.SmoothDamp(_cvc.m_Lens.OrthographicSize, value, ref _velocity, 0.4f);

            var playerPos = _playerPos;

            var distance = Vector2.Distance(ClosestMonsterPosition, _playerPos);

            distance = Mathf.Clamp(distance, defaultViewSize * 1.4f, defaultViewSize * 2f);
            _cvc.m_Lens.OrthographicSize = Mathf.SmoothDamp(_cvc.m_Lens.OrthographicSize, distance * 0.7f, ref _velocity, 0.4f);

            var directionToClosestMonster = (ClosestMonsterPosition - _playerPos).normalized;

            var offset = 5 * Vector2.Distance(ClosestMonsterPosition, _playerPos) * directionToClosestMonster / 20;

            var pos = (Vector3)_playerPos + (Vector3)offset;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(pos.x, pos.y, transform.position.z), ref _velocity3, 0.4f);
        }
    }
}
