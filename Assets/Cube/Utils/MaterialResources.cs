using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cube.Utils
{
    public static class MaterialResources
    {
        public static Material GetHighLighter() => Resources.Load<Material>("Materials/ItemMaterial");
    }
}
