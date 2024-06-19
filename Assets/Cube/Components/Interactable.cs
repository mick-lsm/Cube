using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Components
{
    public class Interactable : MonoBehaviour
    {
        public event Action<InteractArgs> OnInteracted;
        public event Action<InteractArgs> OnSelecting;

        public void Interact(GameObject interactor)
        {
            var args = new InteractArgs();
            args.interactorSource = interactor;
            OnInteracted?.Invoke(args);
        }
        public void Selecting(GameObject selector)
        {
            var args = new InteractArgs();
            args.interactorSource = selector;
            OnSelecting?.Invoke(args);
        }
    }
}