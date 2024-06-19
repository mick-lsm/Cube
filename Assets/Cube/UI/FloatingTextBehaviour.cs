using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cube.UI
{
    public class FloatingTextBehaviour : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private Color _defaultColor;
        private float _defaultFontSize;

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
            _defaultColor = text.color;
            _defaultFontSize = text.fontSize;
            StartCoroutine(StartDeathTextBehaviour());
        }
        private IEnumerator StartDeathTextBehaviour()
        {
            var ran1 = UnityEngine.Random.value;
            var ran2 = UnityEngine.Random.value;
            var ran3 = UnityEngine.Random.value;
            while (true)
            {
                var ea = text.transform.eulerAngles;
                ea.z = Mathf.Sin(Time.time * 2 + ran1) * 2.5f;
                text.transform.eulerAngles = ea;

                var value = Mathf.Sin(Time.time * 3 + ran2) * 2 + _defaultFontSize;
                text.fontSize = Mathf.Lerp(text.fontSize, value, 0.2f);

                var colorValue = Mathf.Sin(Time.time * 1.5f + ran3) * 0.05f;
                text.color = new Color(_defaultColor.r, _defaultColor.g + colorValue, _defaultColor.b);

                yield return null;
            }
        }
    }
}
