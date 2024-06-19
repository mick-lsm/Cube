using Cube.Components;
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
    public class ScoreUI : MonoBehaviour
    {
        public PlayerScoreboard scoreboard;
        public TextMeshProUGUI scoreUI;

        private int _lastScore;

        private void Update()
        {
            RepairScoreUI();
            if (scoreboard == null) return;
            scoreUI.text = "Score : " + scoreboard.score.ToString();

            if (scoreboard.score == _lastScore) return;
            _lastScore = scoreboard.score;
            scoreUI.fontSize = 180;
            var ea = scoreUI.transform.eulerAngles;
            ea.z = 25;
            scoreUI.transform.eulerAngles = ea;

        }
        private void RepairScoreUI()
        {
            scoreUI.fontSize = Mathf.Lerp(scoreUI.fontSize, 120, 0.5f * 10 * Time.deltaTime);
            scoreUI.transform.eulerAngles = Vector3.Lerp(scoreUI.transform.eulerAngles, Vector3.zero, 0.5f * 10 * Time.deltaTime);
        }
    }
}
