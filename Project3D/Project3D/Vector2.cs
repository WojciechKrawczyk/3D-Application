using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3D
{
    public class Vector2
    {
        private float x;
        private float y;

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
