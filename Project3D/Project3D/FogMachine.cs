using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project3D
{
    public abstract class FogMachine
    {
        protected Color fogColor;
        protected float maxDistance;
    
        public abstract (float r, float g, float b) ApplyFogToColor(float r, float g, float b, Vector3 vector, Vector3 cameraPosition);
    }

    public class NoFogMachine: FogMachine
    {
        public override (float r, float g, float b) ApplyFogToColor(float r, float g, float b, Vector3 vector, Vector3 cameraPosition)
        {
            return (r, g, b);
        }
    }

    public class BasicFogMachine: FogMachine
    {
        public BasicFogMachine(Color fogColor, float maxDistance)
        {
            this.fogColor = fogColor;
            this.maxDistance = maxDistance;
        }

        public override (float r, float g, float b) ApplyFogToColor(float r, float g, float b, Vector3 vector, Vector3 cameraPosition)
        {
            float dist = Vector3.Distance(vector, cameraPosition);
            float f = (maxDistance - dist) / maxDistance;
            float R = ((r * f) + (1 - f) * fogColor.R / 255);
            float G = ((g * f) + (1 - f) * fogColor.G / 255);
            float B = ((b * f) + (1 - f) * fogColor.B / 255);

            return (R, G, B);
        }
    }
}
