using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Cube.Scene
{
    public class PostProcessVolumeVignetteRedWithPlayerHealth : MonoBehaviour
    {
        public Health health;
        protected PostProcessVolume volume;
        protected Vignette vignette;

        protected static Color WhenFull = Color.black;
        protected static Color WhenEmpty = Color.red;
        protected static float Condition = 0.4f;

        private void Awake()
        {
            volume = GetComponent<PostProcessVolume>();
            vignette = volume.profile.GetSetting<Vignette>();
        }
        private void Update()
        {
            if (health.currentHealth < health.maxHealth * Condition) //if lesser than 20% of max health
            {
                var start = health.maxHealth * Condition;
                var value = 1 - health.currentHealth / start;
                var color = Color.Lerp(WhenFull, WhenEmpty, value);
                vignette.color.value = color;
            }
            else vignette.color.value = WhenFull;
        }
    }
}
