using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project3D
{
    public class DetailsForShading
    {
        public Color globalColor;
        public Vertex v1;
        public Vertex v2;
        public Vertex v3;
        public Color v1Color;
        public Color v2Color;
        public Color v3Color;
        public double area;
        public Vector3 pa;
        public Vector3 pb;
        public Vector3 pc;
        public Vector3 pd;
        public int currentY;
        public MeshLightParameters lightParameters;
    }

    public abstract class ShadingMachine
    {
        public MeshLightParameters meshLightParameters;
        public Vector3 cameraPosition;
        public LightsManager lightsManager;
        public FogMachine fogMachine;

        public abstract DetailsForShading GetDetailsForShading(Vertex v1, Vertex v2, Vertex v3);

        public abstract Color GetColorPerPixel(Vertex vertex, DetailsForShading details); 

        protected Color ComputeColorPerVertex(Vertex v)
        {
            if (meshLightParameters == null)
                return Color.White;

            Vector3 vNormal = v.Normal;
            Vector3 vCoordinates = v.WorldCoordinates;
            Vector3 vViewer = cameraPosition - vCoordinates;

            float r;
            float g;
            float b;

            float ambient = lightsManager.ia * meshLightParameters.ka;

            float diffuseR = 0.0f;
            float diffuseG = 0.0f;
            float diffuseB = 0.0f;

            float specularR = 0.0f;
            float specularG = 0.0f;
            float specularB = 0.0f;

            foreach (var ls in lightsManager.lightSources)
            {
                Vector3 V = vCoordinates - ls.Position;
                V.Normalize();

                float cos = Math.Max(0, Vector3.Dot(V, ls.Direction));
                if (ls.CosLightAngle > cos)
                    continue;

                diffuseR += ComputeNDotL(vCoordinates, vNormal, ls.Position) * ls.color.R / 255;
                diffuseG += ComputeNDotL(vCoordinates, vNormal, ls.Position) * ls.color.G / 255;
                diffuseB += ComputeNDotL(vCoordinates, vNormal, ls.Position) * ls.color.B / 255;

                Vector3 vR = Vector3.Reflect(-ls.Position, vNormal);
                float vr = ComputeNDotL(vCoordinates, vR, cameraPosition);
                specularR += (float)Math.Pow(vr, meshLightParameters.m) * ls.color.R / 255;
                specularG += (float)Math.Pow(vr, meshLightParameters.m) * ls.color.G / 255;
                specularB += (float)Math.Pow(vr, meshLightParameters.m) * ls.color.B / 255;
            }

            r = (ambient + diffuseR * meshLightParameters.kd + specularR * meshLightParameters.ks) * meshLightParameters.color.R / 255;
            g = (ambient + diffuseG * meshLightParameters.kd + specularG * meshLightParameters.ks) * meshLightParameters.color.G / 255;
            b = (ambient + diffuseB * meshLightParameters.kd + specularB * meshLightParameters.ks) * meshLightParameters.color.B / 255;

            (r, g, b) = fogMachine.ApplyFogToColor(r, g, b, vCoordinates, cameraPosition);
            int R = (int)(r * 255);
            int G = (int)(g * 255);
            int B = (int)(b * 255);

            (R, G, B) = ValidateRGB(R, G, B);

            return Color.FromArgb(R, G, B);
        }

        protected (int R, int G, int B) ValidateRGB(int R, int G, int B)
        {
            R = ValidateColorComponent(R);
            G = ValidateColorComponent(G);
            B = ValidateColorComponent(B);

            return (R, G, B);
        }

        private int ValidateColorComponent(int c)
        {
            if (c > 255)
                c = 255;
            else if (c < 0)
                c = 0;
            return c;
        }

        protected (double alpha, double beta, double gamma) CalculateAlphaBetaGammaForVertex(Vector3 v, DetailsForShading details)
        {
            double a, b, g;
            double areaA, areaB, areaG;
            areaA = CalculateArea(v, details.v2.Coordinates, details.v3.Coordinates);
            areaB = CalculateArea(v, details.v1.Coordinates, details.v3.Coordinates);
            areaG = CalculateArea(v, details.v1.Coordinates, details.v2.Coordinates);
            a = areaA / details.area;
            b = areaB / details.area;
            g = areaG / details.area;
            return (a, b, g);
        }

        // Compute the cosine of the angle between the light vector and the normal vector
        // Returns a value between 0 and 1
        float ComputeNDotL(Vector3 vertex, Vector3 normal, Vector3 lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();

            return Math.Max(0, Vector3.Dot(normal, lightDirection));
        }

        protected double CalculateArea(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return Math.Abs((p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X)) / 2.0;
        }
    }

    public class FlatShadingMachine: ShadingMachine
    {
        public override DetailsForShading GetDetailsForShading(Vertex v1, Vertex v2, Vertex v3)
        {
            Vector3 vNormal = (v1.Normal + v2.Normal + v3.Normal) / 3;
            Vector3 vCenterPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3;

            Vertex v = new Vertex();
            v.Normal = vNormal;
            v.WorldCoordinates = vCenterPoint;

            DetailsForShading details = new DetailsForShading();
            details.globalColor = ComputeColorPerVertex(v);

            return details;
        }

        public override Color GetColorPerPixel(Vertex vertex, DetailsForShading details)
        {
            return details.globalColor;
        }
    }

    public class GouraudSchadingMachine: ShadingMachine
    {
        public override DetailsForShading GetDetailsForShading(Vertex v1, Vertex v2, Vertex v3)
        {
            DetailsForShading details = new DetailsForShading();
            details.v1Color = ComputeColorPerVertex(v1);
            details.v2Color = ComputeColorPerVertex(v2);
            details.v3Color = ComputeColorPerVertex(v3);
            details.v1 = v1;
            details.v2 = v2;
            details.v3 = v3;
            details.area = CalculateArea(v1.Coordinates, v2.Coordinates, v3.Coordinates);

            return details;
        }

        public override Color GetColorPerPixel(Vertex vertex, DetailsForShading details)
        {
            (double alpha, double beta, double gamma) = CalculateAlphaBetaGammaForVertex(vertex.Coordinates, details);
            float r = (float)(alpha * details.v1Color.R / 255 + beta * details.v2Color.R / 255 + gamma * details.v3Color.R / 255);
            float g = (float)(alpha * details.v1Color.G / 255 + beta * details.v2Color.G / 255 + gamma * details.v3Color.G / 255);
            float b = (float)(alpha * details.v1Color.B / 255 + beta * details.v2Color.B / 255 + gamma * details.v3Color.B / 255);
            int R = (int)(r * 255);
            int G = (int)(g * 255);
            int B = (int)(b * 255);
            (R, G, B) = ValidateRGB(R, G, B);

            return Color.FromArgb(R, G, B);
        }
    }

    public class PhongShadinMachine: ShadingMachine
    {
        public override DetailsForShading GetDetailsForShading(Vertex v1, Vertex v2, Vertex v3)
        {
            DetailsForShading details = new DetailsForShading();
            details.v1 = v1;
            details.v2 = v2;
            details.v3 = v3;
            details.area = CalculateArea(v1.Coordinates, v2.Coordinates, v3.Coordinates);
            details.lightParameters = meshLightParameters;

            return details;
        }

        public override Color GetColorPerPixel(Vertex vertex, DetailsForShading details)
        {
            (double alpha, double beta, double gamma) = CalculateAlphaBetaGammaForVertex(vertex.Coordinates, details);
            Vector3 normal = (float)alpha * details.v1.Normal + (float)beta * details.v2.Normal + (float)gamma * details.v3.Normal;
            normal.Normalize();
            Vector3 wcoordinates = (float)alpha * details.v1.WorldCoordinates + (float)beta * details.v2.WorldCoordinates + (float)gamma * details.v3.WorldCoordinates;
            vertex.Normal = normal;
            vertex.WorldCoordinates = wcoordinates;

            Color color = ComputeColorPerVertex(vertex);
            return color;
        }
    }
}
