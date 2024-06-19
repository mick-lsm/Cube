using Cube.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cube.UI
{
    public class DeathPanelController : MonoBehaviour
    {
        public Damageable subscribe;
        public GameObject deathPanel;

        //public TextMeshProUGUI deathText;
        private void Awake()
        {
            subscribe.OnDeath += OnDeath;
        }

        private void OnDeath(DeathArgs args)
        {
            StartCoroutine(StartDeathUI());
        }
        private IEnumerator StartDeathUI()
        {
            yield return new WaitForSeconds(1);
            deathPanel.SetActive(true);
            //StartCoroutine(StartDeathTextBehaviour());
        }
        //private IEnumerator StartDeathTextBehaviour()
        //{
        //    var ran1 = UnityEngine.Random.value;
        //    var ran2 = UnityEngine.Random.value;
        //    var ran3 = UnityEngine.Random.value;
        //    while (true)
        //    {
        //        var ea = deathText.transform.eulerAngles;
        //        ea.z = Mathf.Sin(Time.time * 2 + ran1) * 2.5f;
        //        deathText.transform.eulerAngles = ea;

        //        var value = Mathf.Sin(Time.time * 3 + ran2) * 2 + 180;
        //        deathText.fontSize = Mathf.Lerp(deathText.fontSize, value,0.2f);

        //        var colorValue = Mathf.Sin(Time.time * 1.5f + ran3) * 0.05f;
        //        deathText.color = new Color(1, 0.7f + colorValue, 0);

        //        yield return null;
        //    }
        //}

    }
}
