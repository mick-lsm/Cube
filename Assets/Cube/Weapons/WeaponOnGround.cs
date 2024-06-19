using Cube.Components;
using Cube.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Weapons
{
    [RequireComponent(typeof(Interactable))]
    public class WeaponOnGround : MonoBehaviour
    {
        private Interactable _interactable;
        public WeaponBehaviour source;

        private void Awake()
        {
            _interactable = GetComponent<Interactable>();
        }
        private void Start()
        {
            _interactable.OnInteracted += OnInteracted;
        }
        private void OnInteracted(InteractArgs args)
        {
            var inventory = args.interactorSource.GetComponent<EntityInventory>();
            source.gameObject.SetActive(true);
            if (inventory != null) inventory.JoinWeapon(source);

            Destroy(gameObject);
        }
    }
}
