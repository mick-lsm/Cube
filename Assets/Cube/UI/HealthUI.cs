using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI
{
    public class HealthUI : MonoBehaviour
    {
        public Health health;
        public Image healthBar;
        private Vector2 _vel = Vector2.zero;
        private float _smoothTime = 0.3f;

        private void Start()
        {
            healthBar.fillAmount = 0;
        }

        private void Update()
        {
            float newScale;

            if (health == null) newScale = 0;
            else newScale = (float)health.currentHealth / health.maxHealth;
            if (newScale < 0) newScale = 0;

            var desiredScale = new Vector2(newScale, 1);
            healthBar.fillAmount = Vector2.SmoothDamp(new Vector2(healthBar.fillAmount, 1), desiredScale, ref _vel, _smoothTime).x;
        }
    }
}
