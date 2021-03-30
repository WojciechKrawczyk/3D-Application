using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3D
{
    class Vector3D
    {
        private double x;
        private double y;
        private double z;
        private double w;
         
        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public double Z
        {
            get { return z; }
        }

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1.0;
        }

        public Vector3D GetNormalized()
        {
            double s = Math.Sqrt(x * x + y * y + z * z);
            return new Vector3D(x / s, y / s, z / s);
        }

        public void Normalize()
        {
            double s = Math.Sqrt(x * x + y * y + z * z);
            x = x / s;
            y = y / s;
            z = z / s;
            w = 1;
        }
    }
}
