using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project3D
{
    public class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Face[] Faces { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 PositionStep { get; set; }
        public Vector3 RotationStep { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 ProjectCoordinatesTransform { get; set; }
        public MeshLightParameters lightParameters;
        public PositionCalculator positionCalculator;

        public Mesh(string name, int verticesCount, int facesCount)
        {
            Vertices = new Vertex[verticesCount];
            Faces = new Face[facesCount];
            Name = name;
            ProjectCoordinatesTransform = Vector3.Zero;
        }
    }

    public struct Face
    {
        public int A;
        public int B;
        public int C;
    }

    public struct Vertex
    {
        public Vector3 Normal;
        public Vector3 Coordinates;
        public Vector3 WorldCoordinates;
    }

    public class MeshLightParameters
    {
        public Color color;
        public float ka;
        public float kd;
        public float ks;
        public int m;

        public MeshLightParameters(Color color, float ka, float kd, float ks, int m)
        {
            this.color = color;
            this.ka = ka;
            this.kd = kd;
            this.ks = ks;
            this.m = m;
        }

        public MeshLightParameters() { }
    }
}
