using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project3D
{
    public class LightSource
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public float CosLightAngle { get; set; }
        public Color color;
    }

    public class LightsManager
    {
        public List<LightSource> lightSources;
        public float ia;

        public LightsManager(float ia)
        {
            this.ia = ia;
            this.lightSources = new List<LightSource>();
        }

        public void AddLightSource(LightSource lightSource)
        {
            this.lightSources.Add(lightSource);
        }
    }
}
