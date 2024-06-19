using Cube.Components;
using Cube.Entity.AI;
using Cube.Inventory;
using Cube.Registry;
using Cube.Utils;
using Cube.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace Cube.Entity.Behaviours
{
    public class EntityAttackBehaviour : MonoBehaviour
    {
        public WeaponBehaviour weaponInstance;

        private EntityInventory _inventory;

        [SerializeField]
        private Vector2 _playerPos;

        private void Awake()
        {
            _inventory = GetComponent<EntityInventory>();
        }

        private void Start()
        {
            _inventory.JoinWeapon(weaponInstance);
        }


        private void Update()
        {
            var player = transform.GetNearestPlayer();
            if(player != null) _playerPos = player.transform.position;
            //if (UnityEngine.Random.value <= 0.025f) _inventory.CurrentWeapon.Attack();
            _inventory.CurrentWeapon.TargetAt(_playerPos);
        }
        private void FixedUpdate()
        {
            var player = transform.GetNearestPlayer();
            if (player != null) _playerPos = player.transform.position;
            if (UnityEngine.Random.value <= 0.01f) _inventory.CurrentWeapon.Attack();

        }
    }
}
