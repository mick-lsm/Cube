using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Scene
{
    public class Door : MonoBehaviour
    {
        protected Interactable interactable;

        private void Awake()
        {
            interactable = GetComponent<Interactable>();
            interactable.OnInteracted += OnInteract;
        }
        private void OnInteract(InteractArgs args)
        {
            SceneManager.Instance.SetRandomMap();
        }
    }
}
