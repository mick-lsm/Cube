using Cube.Utils;
using Cube.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Inventory
{
    public class EntityInventory : MonoBehaviour
    {
        public event Action<WeaponBehaviour, WeaponBehaviour> OnSwitchWeapon;
        public event Action<WeaponBehaviour> OnJoinedWeapon;

        public Transform weaponHolder;
        public int capacity;

        private WeaponBehaviour _currentWeapon;
        private int index;

        public WeaponBehaviour CurrentWeapon => _currentWeapon;

        private List<WeaponBehaviour> _weapons = new List<WeaponBehaviour>();

        public void JoinWeapon(WeaponBehaviour weapon)
        {
            var lastWeapon = _currentWeapon;
            var currentWeapon = weapon;
            _currentWeapon = weapon;

            var insertIndex = index;

            if(lastWeapon != null && weaponHolder.childCount + 1 > capacity)
            {
                insertIndex = lastWeapon.transform.GetSiblingIndex();
                lastWeapon.BeforeExit();
                var p = lastWeapon.PackageWeapon();
                p.transform.position += new Vector3(UnityEngine.Random.Range(-3, 3), 0);
                _weapons.Remove(lastWeapon);
            }
            else if (lastWeapon != null)
            {
                lastWeapon.BeforeExit();
                lastWeapon.gameObject.SetActive(false);
            }
            var cwTfm = currentWeapon.transform;
            cwTfm.SetParent(weaponHolder);
            cwTfm.localPosition = Vector3.zero;   
            cwTfm.SetSiblingIndex(insertIndex);
            currentWeapon.AfterEnter();

            _weapons.Insert(insertIndex, weapon);

            index = insertIndex;

            OnJoinedWeapon?.Invoke(_currentWeapon);
        }
        public void SwitchWeapon(int direction)
        {
            if(direction != -1 && direction != 0 && direction != 1) throw new ArgumentOutOfRangeException(nameof(direction));
            if (weaponHolder.childCount is 0 or 1) return; //No needs for switch weapon
            var lastIndex = index;
            var newIndex = index + direction;
            if (newIndex < 0) newIndex = weaponHolder.childCount - 1;
            if (newIndex >= weaponHolder.childCount) newIndex = 0;

            var lastWeapon = weaponHolder.GetChild(lastIndex).GetComponent<WeaponBehaviour>();
            var newWeapon = weaponHolder.GetChild(newIndex).GetComponent<WeaponBehaviour>();

            lastWeapon.gameObject.SetActive(false);
            lastWeapon.BeforeExit();

            newWeapon.gameObject.SetActive(true);
            newWeapon.AfterEnter();

            _currentWeapon = newWeapon;
            index = newIndex;

            OnSwitchWeapon?.Invoke(lastWeapon, newWeapon);
        }
        public WeaponBehaviour[] GetWeapons()
        {
            return _weapons.ToArray();
        }
    }
}