using Cube.Utils;
using Cube.Components;
using System.Collections;
using UnityEngine;

namespace Cube.Entity
{
    public class EntityEffector : MonoBehaviour
    {
        protected Damageable damageable;
        protected SpriteRenderer flasher;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            flasher = new GameObject("Flasher").AddComponent<SpriteRenderer>();
            flasher.transform.SetParent(transform);
            flasher.sprite = GetComponent<SpriteRenderer>().sprite;
            flasher.color = Color.clear;
            flasher.sortingLayerName = "Effect";
            flasher.transform.localScale = Vector3.one;
            flasher.transform.localPosition = Vector3.zero;
        }
        private void OnEnable()
        {
            damageable.OnDamaged += OnDamaged;
            damageable.OnDeath += OnDeath;
        }
        private void OnDisable()
        {
            damageable.OnDamaged -= OnDamaged;
            damageable.OnDeath -= OnDeath;

        }
        private void OnDamaged(DamageArgs args)
        {
            Effector.GenerateEntityDamageEffect(gameObject);
            if (gameObject.GetTeam() == Entity.Team.Player)
                Effector.ShakeCamera(0.2f, 0.2f);

            StartCoroutine(Flash());
        }
        private void OnDeath(DeathArgs args)
        {
            Effector.GenerateEntityDeathEffect(gameObject);
            //Effector.ShakeCamera(0.5f, 0.25f);
        }
        private IEnumerator Flash()
        {
            flasher.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            flasher.color = Color.clear;
        }
    }
}
