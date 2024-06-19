using Cube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{
    public class PlayerScoreboard : MonoBehaviour
    {
        public int score;

        private void Awake()
        {
            if (TryGetComponent<KillOtherDoable>(out var kill)) kill.OnKilledEntity += k =>
            {
                
                score += EntityToScoreTranslation.GetEntityScore(k.gameObject);
            };
        }
    }
}
