using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project3D
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public float fov;
        public float znear;
        public float zfar;

        public Camera()
        {
            this.fov = 1.4f;
            this.znear = 0.01f;
            this.zfar = 1.0f;
        }
    }
}
