using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    [RequireComponent(typeof(Interactable))]
    public class HighLightOnSelecting : MonoBehaviour
    {
        //public Material material;
        public Renderer ren;

        private bool _selected;
        private void Start()
        {
            GetComponent<Interactable>().OnSelecting += OnSelecting;
            ren = GetComponent<Renderer>();
            //ren.material = material;
        }
        private void Update()
        {
            if (_selected) _selected = false;
            else ren.material.SetInt("_IsHighLighting", 0);
            //else ren.SetMaterials(new List<Material> { ren.material });
            //else ren.materials.
        }

        private void OnSelecting(InteractArgs args)
        {
            _selected = true;
            ren.material.SetInt("_IsHighLighting", 1);
        }
    }
}
