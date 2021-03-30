using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Windows.Foundation.Metadata;
using Windows.Storage;
using System.IO;
using Newtonsoft.Json;

namespace Project3D
{
    public class Device
    {
        public DirectBitmap bmp;
        public DirectBitmap basicBmp;
        private int bmpWidth;
        private int bmpHeight;
        private float[,] depthBuffer;
        private float[,] depthBufferCopy;
        public LightsManager lightsManager;
        private Vector3 cameraPosition;
        public ShadingMachine shadingMachine;

        public Device(int width, int height)
        {
            this.bmp = new DirectBitmap(width, height);
            this.bmpWidth = width;
            this.bmpHeight = height;

            depthBuffer = new float[bmpWidth, bmpHeight];
            depthBufferCopy = new float[bmpWidth, bmpHeight];

            for (int i = 0; i < bmpWidth; i++)
            {
                for (int j = 0; j < bmpHeight; j++)
                {
                    depthBufferCopy[i, j] = float.MaxValue;
                }              
            }
        }

        public void UseFlatShading()
        {
            FlatShadingMachine machine = new FlatShadingMachine();
            machine.fogMachine = this.shadingMachine.fogMachine;
            shadingMachine = machine;
        }

        public void UseGouraudShading()
        {
            GouraudSchadingMachine machine = new GouraudSchadingMachine();
            machine.fogMachine = this.shadingMachine.fogMachine;
            shadingMachine = machine;
        }

        public void UsePhongShading()
        {
            PhongShadinMachine machine = new PhongShadinMachine();
            machine.fogMachine = this.shadingMachine.fogMachine;
            shadingMachine = machine;
        }

        public void RemoveFog()
        {
            this.shadingMachine.fogMachine = new NoFogMachine();
        }

        public void AddFog(FogMachine machine)
        {
            this.shadingMachine.fogMachine = machine;
        }

        public void Clear()
        {
            this.bmp.Dispose();
            this.bmp = new DirectBitmap(bmpWidth, bmpHeight);

            depthBuffer = (float[,])depthBufferCopy.Clone();
        }

        public Vertex Project(Vertex vertex, Matrix transMat, Matrix world)
        {
            var point2d = Vector3.TransformCoordinate(vertex.Coordinates, transMat);

            var point3dWorld = Vector3.TransformCoordinate(vertex.Coordinates, world);
            var normal3dWorld = Vector3.TransformCoordinate(vertex.Normal, world);

            var x = point2d.X * bmpWidth + bmpWidth / 2.0f;
            var y = -point2d.Y * bmpHeight + bmpHeight / 2.0f;

            return new Vertex
            {
                Coordinates = new Vector3(x, y, point2d.Z),
                Normal = normal3dWorld,
                WorldCoordinates = point3dWorld
            };
        }

        public void DrawPoint(Vector3 point, Color color)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X < bmpWidth && point.Y < bmpHeight)
            {
                if (point.Z < depthBuffer[(int)point.X, (int)point.Y])
                {
                    depthBuffer[(int)point.X, (int)point.Y] = point.Z;
                    bmp.SetPixel((int)point.X, (int)point.Y, color);
                }
            }
        }

        // The main method of the device that re-compute each vertex projection during each frame
        public void Render(Camera camera, params Mesh[] meshes)
        {
            this.cameraPosition = camera.Position;

            Matrix viewMatrix = Matrix.LookAtLH(camera.Position, camera.Target, Vector3.UnitY);
            Matrix projectionMatrix = Matrix.PerspectiveFovLH(camera.fov, (float)bmpWidth / bmpHeight, camera.znear, camera.zfar);

            foreach (Mesh mesh in meshes)
            {
                Matrix worldMatrix = Matrix.RotationYawPitchRoll(mesh.Rotation.Y, mesh.Rotation.X, mesh.Rotation.Z) * Matrix.Translation(mesh.Position) * Matrix.Scaling(mesh.Scale);

                Matrix transformMatrix = worldMatrix * viewMatrix * projectionMatrix;

                shadingMachine.cameraPosition = camera.Position;
                shadingMachine.lightsManager = lightsManager;
                shadingMachine.meshLightParameters = mesh.lightParameters;

                Parallel.For(0, mesh.Faces.Length, faceIndex =>
                {
                    var face = mesh.Faces[faceIndex];
                    var vertexA = mesh.Vertices[face.A];
                    var vertexB = mesh.Vertices[face.B];
                    var vertexC = mesh.Vertices[face.C];

                    var pixelA = Project(vertexA, transformMatrix, worldMatrix);
                    var pixelB = Project(vertexB, transformMatrix, worldMatrix);
                    var pixelC = Project(vertexC, transformMatrix, worldMatrix);

                    pixelA.Coordinates += mesh.ProjectCoordinatesTransform;
                    pixelB.Coordinates += mesh.ProjectCoordinatesTransform;
                    pixelC.Coordinates += mesh.ProjectCoordinatesTransform;

                    DrawTriangle(pixelA, pixelB, pixelC, mesh.lightParameters);

                    faceIndex++;
                });
            }
        }

        //Clamping values to keep them between 0 and 1
        float Clamp(float value, float min = 0, float max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        // Interpolating the value between 2 vertices 
        // min is the starting point, max the ending point
        // and gradient the % between the 2 points
        float Interpolate(float min, float max, float gradient)
        {
            return min + (max - min) * Clamp(gradient);
        }

        void ProcessScanLine(DetailsForShading details)
        {
            var gradient1 = details.pa.Y != details.pb.Y ? (details.currentY - details.pa.Y) / (details.pb.Y - details.pa.Y) : 1;
            var gradient2 = details.pc.Y != details.pd.Y ? (details.currentY - details.pc.Y) / (details.pd.Y - details.pc.Y) : 1;

            int sx = (int)Interpolate(details.pa.X, details.pb.X, gradient1);
            int ex = (int)Interpolate(details.pc.X, details.pd.X, gradient2);

            // starting Z & ending Z
            float z1 = Interpolate(details.pa.Z, details.pb.Z, gradient1);
            float z2 = Interpolate(details.pc.Z, details.pd.Z, gradient2);

            // drawing a line from left (sx) to right (ex) 
            for (var x = sx; x < ex; x++)
            {
                float gradient = (x - sx) / (float)(ex - sx);
                var z = Interpolate(z1, z2, gradient);

                Vertex current = new Vertex();
                current.Coordinates = new Vector3(x, details.currentY, z);

                Color color = shadingMachine.GetColorPerPixel(current, details);
                DrawPoint(current.Coordinates, color);
            }
        }

        public void DrawTriangle(Vertex v1, Vertex v2, Vertex v3, MeshLightParameters lightParameters)
        {
            // Sorting the points in order to always have this order on screen p1, p2 & p3
            // with p1 always up (thus having the Y the lowest possible to be near the top screen)
            // then p2 between p1 & p3
            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            if (v2.Coordinates.Y > v3.Coordinates.Y)
            {
                var temp = v2;
                v2 = v3;
                v3 = temp;
            }

            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            Vector3 p1 = v1.Coordinates;
            Vector3 p2 = v2.Coordinates;
            Vector3 p3 = v3.Coordinates;

            DetailsForShading details = shadingMachine.GetDetailsForShading(v1, v2, v3);

            // computing lines' directions
            float dP1P2, dP1P3;

            // http://en.wikipedia.org/wiki/Slope
            // Computing slopes
            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            // First case where triangles are like that:
            // P1
            // -
            // -- 
            // - -
            // -  -
            // -   - P2
            // -  -
            // - -
            // -
            // P3
            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    //data.currentY = y;
                    details.currentY = y;
                    if (y < p2.Y)
                    {
                        details.pa = v1.Coordinates;
                        details.pb = v3.Coordinates;
                        details.pc = v1.Coordinates;
                        details.pd = v2.Coordinates;
                        //ProcessScanLine(data, v1, v3, v1, v2, details.globalColor);
                        ProcessScanLine(details);
                    }
                    else
                    {
                        details.pa = v1.Coordinates;
                        details.pb = v3.Coordinates;
                        details.pc = v2.Coordinates;
                        details.pd = v3.Coordinates;
                        // ProcessScanLine(data, v1, v3, v2, v3, details.globalColor);
                        ProcessScanLine(details);
                    }
                }
            }
            // First case where triangles are like that:
            //       P1
            //        -
            //       -- 
            //      - -
            //     -  -
            // P2 -   - 
            //     -  -
            //      - -
            //        -
            //       P3
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    //data.currentY = y;
                    details.currentY = y;
                    if (y < p2.Y)
                    {
                        details.pa = v1.Coordinates;
                        details.pb = v2.Coordinates;
                        details.pc = v1.Coordinates;
                        details.pd = v3.Coordinates;
                        //ProcessScanLine(data, v1, v2, v1, v3, details.globalColor);
                        ProcessScanLine(details);
                    }
                    else
                    {
                        details.pa = v2.Coordinates;
                        details.pb = v3.Coordinates;
                        details.pc = v1.Coordinates;
                        details.pd = v3.Coordinates;
                        // ProcessScanLine(data, v2, v3, v1, v3, details.globalColor);
                        ProcessScanLine(details);
                    }
                }
            }
        }

        public Mesh LoadJSONFileAsync(string fileName)
        {
            var meshes = new List<Mesh>();
            var data = System.IO.File.ReadAllText(fileName);
            dynamic jsonObject = JsonConvert.DeserializeObject(data);

            for (var meshIndex = 0; meshIndex < jsonObject.meshes.Count; meshIndex++)
            {
                var verticesArray = jsonObject.meshes[meshIndex].vertices;
                // Faces
                var indicesArray = jsonObject.meshes[meshIndex].indices;

                var uvCount = jsonObject.meshes[meshIndex].uvCount.Value;
                var verticesStep = 1;

                // Depending of the number of texture's coordinates per vertex
                // we're jumping in the vertices array  by 6, 8 & 10 windows frame
                switch ((int)uvCount)
                {
                    case 0:
                        verticesStep = 6;
                        break;
                    case 1:
                        verticesStep = 8;
                        break;
                    case 2:
                        verticesStep = 10;
                        break;
                }

                var verticesCount = verticesArray.Count / verticesStep;
                // number of faces is logically the size of the array divided by 3 (A, B, C)
                var facesCount = indicesArray.Count / 3;
                var mesh = new Mesh(jsonObject.meshes[meshIndex].name.Value, verticesCount, facesCount);

                // Filling the Vertices array of mesh first
                for (var index = 0; index < verticesCount; index++)
                {
                    var x = (float)verticesArray[index * verticesStep].Value;
                    var y = (float)verticesArray[index * verticesStep + 1].Value;
                    var z = (float)verticesArray[index * verticesStep + 2].Value;
                    // Loading the vertex normal exported by Blender
                    var nx = (float)verticesArray[index * verticesStep + 3].Value;
                    var ny = (float)verticesArray[index * verticesStep + 4].Value;
                    var nz = (float)verticesArray[index * verticesStep + 5].Value;
                    mesh.Vertices[index] = new Vertex { Coordinates = new Vector3(x, y, z), Normal = new Vector3(nx, ny, nz) };
                }

                // Then filling the Faces array
                for (var index = 0; index < facesCount; index++)
                {
                    var a = (int)indicesArray[index * 3].Value;
                    var b = (int)indicesArray[index * 3 + 1].Value;
                    var c = (int)indicesArray[index * 3 + 2].Value;
                    mesh.Faces[index] = new Face { A = a, B = b, C = c };
                }

                // Getting the position set in Blender
                var position = jsonObject.meshes[meshIndex].position;
                mesh.Position = new Vector3((float)position[0].Value, (float)position[1].Value, (float)position[2].Value);
                meshes.Add(mesh);
            }
            return meshes[0];
        }
    }
}
