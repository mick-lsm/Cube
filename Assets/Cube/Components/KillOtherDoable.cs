using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Components
{

    public class KillOtherDoable : MonoBehaviour
    {
        public event Action<Damageable> OnKilledEntity;

        public void Killed(Damageable entity) => OnKilledEntity?.Invoke(entity);
    }
}
