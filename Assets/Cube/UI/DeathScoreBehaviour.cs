using Cube.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cube.UI
{
    public class DeathScoreBehaviour : MonoBehaviour
    {
        public PlayerScoreboard scoreboard;

        private void OnEnable()
        {
            GetComponent<TextMeshProUGUI>().text = "Your Score : " + scoreboard.score.ToString();
        }
    }
}
