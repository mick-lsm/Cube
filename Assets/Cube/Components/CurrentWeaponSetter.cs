using Cube.Inventory;
using Cube.Weapons;
using UnityEngine;

namespace Cube.Components
{
    public class CurrentWeaponSetter : MonoBehaviour
    {
        public WeaponBehaviour instance;

        private void Start()
        {
            var inventory = GetComponent<EntityInventory>();
            inventory.JoinWeapon(instance);
        }
    }
}
