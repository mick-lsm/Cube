using Cube.Entity;
using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons.Behaviours
{
    [RequireComponent(typeof(WeaponBehaviour))]
    public class WeaponComponent : MonoBehaviour
    {
        private WeaponBehaviour _weaponBehaviour;
        public WeaponBehaviour Main => _weaponBehaviour;

        private Team _team;

        protected virtual void OnEnable()
        {
            _weaponBehaviour = GetComponent<WeaponBehaviour>();
            _weaponBehaviour.AfterEntered += AfterEnter;
            _weaponBehaviour.BeforeExited += BeforeExit;
            _weaponBehaviour.OnAttacked += OnAttacked;
            _weaponBehaviour.OnTargetAt += OnTargetAt;
        }
        protected virtual void OnDisable()
        {
            _weaponBehaviour.AfterEntered -= AfterEnter;
            _weaponBehaviour.BeforeExited -= BeforeExit;
            _weaponBehaviour.OnAttacked -= OnAttacked;
            _weaponBehaviour.OnTargetAt -= OnTargetAt;
        }

        protected virtual void AfterEnter() { }
        protected virtual void BeforeExit() { }
        protected virtual void OnAttacked() { }
        protected virtual void OnTargetAt(Vector2 look) { }
    }
}
