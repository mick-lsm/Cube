using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Entity.AI
{
    public class InteractionDoable : MonoBehaviour
    {
        public event Action<Interactable> OnInteractWith;

        public float interactRadius = 3;
        public void Interact()
        {
            var colliders = new Collider2D[10];
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, interactRadius, colliders);

            Interactable firstInteractable = null;

            var ordered = colliders.OrderBy(c => c != null ? Vector2.Distance(c.transform.position, transform.position) : 1000).ToArray();

            for (var i = 0; i < count; i++)
            {
                var collider = ordered[i];
                if (collider.TryGetComponent<Interactable>(out var interactable))
                {
                    firstInteractable = interactable;
                    break;
                }
            }

            if (firstInteractable != null)
            {
                firstInteractable.Interact(gameObject);
                OnInteractWith?.Invoke(firstInteractable);
            }
        }

        public void SelectSurround()
        {
            var colliders = new Collider2D[10];
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, interactRadius, colliders);

            Interactable firstInteractable = null;

            var ordered = colliders.OrderBy(c => c != null ? Vector2.Distance(c.transform.position, transform.position) : 1000).ToArray();

            for (var i = 0; i < count; i++)
            {
                var collider = ordered[i];
                if (collider.TryGetComponent<Interactable>(out var interactable))
                {
                    firstInteractable = interactable;
                    break;
                }
            }

            if (firstInteractable != null)
            {
                //firstInteractable.Interact(gameObject);
                firstInteractable.Selecting(gameObject);
            }
        }
    }
}
