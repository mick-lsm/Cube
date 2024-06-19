using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Collections;
using Cube.Inventory;
using UnityEngine.Profiling;

namespace Cube.UI
{
    public class DebugUI : MonoBehaviour
    {
        public TextMeshProUGUI debugUI;
        public string version;
        public EntityInventory playerInventory;

        protected bool toggled;

        private int fps;

        private void Update()
        {
            if (toggled)
            {
                var debugText = string.Empty;

                var fpsColor = "<color=green>";
                if (fps <= 60) fpsColor = "<color=yellow>";
                if (fps <= 30) fpsColor = "<color=red>";

                var fpsText = string.Empty;
                var fps1 = Mathf.FloorToInt(fps / 1 % 10);
                var fps2 = Mathf.FloorToInt(fps / 10 % 10);
                var fps3 = Mathf.FloorToInt(fps / 100 % 10);

                if(fps > 999) //1000
                {
                    fpsText = "999<";
                }
                else if(fps > 99) //100
                {
                    fpsText = fps3.ToString() + fps2.ToString() + fps1.ToString() + " ";
                }
                else if(fps > 9)
                {
                    fpsText = fps2.ToString() + fps1.ToString() + "  ";
                }
                else
                {
                    fpsText = fps1.ToString() + "   ";
                }



                debugText += "Version : " + version + "\n";
                debugText += fpsColor + fpsText + "</color>fps\n";

                debugText += "Inventory Capacity : " + playerInventory.capacity + "\n";
                debugText += "Current Weapon : " + (playerInventory.CurrentWeapon == null ? "Null" : playerInventory.CurrentWeapon) + "\n";
                debugText += "Inventory Holding : " + playerInventory.GetWeapons().Length + "\n";


                debugUI.text = debugText;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                toggled = !toggled;
                if (!toggled) Clear();
                else
                {
                    StartCoroutine(StartFPS());
                }
            }
        }
        private void Clear()
        {
            debugUI.text = string.Empty;
        }
        private IEnumerator StartFPS()
        {
            while (true)
            {
                var lastCount = Time.renderedFrameCount;
                yield return new WaitForSeconds(1);
                var newCount = Time.renderedFrameCount;
                fps = newCount - lastCount;
            }
        }
    }
}
