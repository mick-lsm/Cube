using Cube.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public List<Image> images;
        public EntityInventory inventory;

        public static Color EnableColor = new Color(1, 1, 1, 0.8f);
        public static Color DisableColor = new Color(1, 1, 1, 0.2f);

        private void Update()
        {
            if (inventory == null) return;
            foreach (var image in images) image.sprite = null;
            Weapons.WeaponBehaviour[] array = inventory.GetWeapons();
            for (int i = 0; i < array.Length; i++) 
            {
                Weapons.WeaponBehaviour weapon = array[i];
                var sprite = weapon.GetDescription().sprite;
                var image = images[i];
                image.sprite = sprite;
                image.SetNativeSize();
                if (weapon.gameObject.activeSelf) image.color = EnableColor;
                else image.color = DisableColor;
            }
        }
    }
}
