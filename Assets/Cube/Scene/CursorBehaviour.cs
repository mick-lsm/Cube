using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class CursorBehaviour : MonoBehaviour
    {
        public Texture2D aim;
        public Damageable playDamageable;

        private void Start()
        {
            playDamageable.OnDamaged += a => ResetCursor();
        }
        private void OnEnable()
        {
            Cursor.SetCursor(aim, new Vector2(5, 5), CursorMode.Auto);
        }
        private void ResetCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
