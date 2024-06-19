using Cube.Utils;
using Cube.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Components
{
    public class WeaponLootable : MonoBehaviour
    {
        public WeaponBehaviour prefab;
        public float chance = 0.6f;

        private void Start()
        {
            GetComponent<Damageable>().OnDeath += OnDeath;
        }
        private void OnDeath(DeathArgs args)
        {
            if (args.source.type != KillerType.Entity) return;
            if (UnityEngine.Random.value <= chance) return;
            var instance = Instantiate(prefab);
            var packaged = instance.PackageWeapon();
            packaged.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-3, 3), 0);
        }
    }
}