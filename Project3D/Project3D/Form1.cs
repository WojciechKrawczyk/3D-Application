using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Project3D
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;

        Device device;
        List<Mesh> meshes = new List<Mesh>();

        Mesh floor;
        bool floorActive = true;

        Camera activeCamera;
        Camera staticCamera = new Camera();
        Camera followingCamera = new Camera();
        Camera dynamicCamera = new Camera();

        BasicFogMachine basicFogMachine = new BasicFogMachine(Color.FromArgb(171, 174, 176), 30);

        public Form1()
        {
            InitializeComponent();

            //Main window settings
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            //render device initialize
            device = new Device(pictureBox1.Width, pictureBox1.Height);

            //camera section
            staticCamera.Position = new Vector3(3, 6, 15);
            staticCamera.Target = Vector3.Zero;

            followingCamera.Position = new Vector3(-5, 7, 20);
            followingCamera.Target = Vector3.Zero;

            dynamicCamera.Position = new Vector3(0, 0, 0);
            dynamicCamera.Target = new Vector3(0, 0, -6);

            //initialize objects on the scene
            //monkey
            Mesh monkey = device.LoadJSONFileAsync("monkey.babylon");
            MeshLightParameters monkeyLightParameters = new MeshLightParameters(Color.Red, 0.4f, 0.7f, 0.9f, 32);
            monkey.lightParameters = monkeyLightParameters;
            monkey.Rotation = new Vector3(0, 0, 0);
            monkey.RotationStep = new Vector3(0, 0.05f, 0);
            PositionCalculator monkeyCalculator = new PositionCalculator();
            Calculator xc = new Calculator(-3, 2.5f, 0, 0.05f);
            Calculator bc = new Calculator();
            monkeyCalculator.XCalculator = xc;
            monkeyCalculator.YCalculator = bc;
            monkeyCalculator.ZCalculator = bc;
            monkey.positionCalculator = monkeyCalculator;
            monkey.Scale = Vector3.One;
            meshes.Add(monkey);

            //cube
            Mesh cube = device.LoadJSONFileAsync("cube1.babylon");
            MeshLightParameters cube1LightParameters = new MeshLightParameters(Color.Gray, 0.9f, 0.8f, 0.9f, 60);
            cube.lightParameters = cube1LightParameters;
            cube.Rotation = new Vector3(0, 0.4f, 0);
            cube.RotationStep = new Vector3(0, 0, 0);
            PositionCalculator cubeCalculator = new PositionCalculator();
            Calculator xc2 = new Calculator(0, 5.1f, 5, 0);
            cubeCalculator.XCalculator = xc2;
            cubeCalculator.YCalculator = bc;
            cubeCalculator.ZCalculator = bc;
            cube.positionCalculator = cubeCalculator;
            cube.Scale = Vector3.One;
            meshes.Add(cube);

            //sphere
            Mesh sphere = device.LoadJSONFileAsync("sphere.babylon");
            MeshLightParameters sphereLightParameters = new MeshLightParameters(Color.Gray, 0.9f, 0.8f, 0.9f, 32);
            sphere.lightParameters = sphereLightParameters;
            sphere.Rotation = new Vector3(0, 0.4f, 0);
            sphere.RotationStep = new Vector3(0, 0, 0);
            PositionCalculator sphereCalculator = new PositionCalculator();
            Calculator xc3 = new Calculator(-6.1f, 0, -6, 0);
            sphereCalculator.XCalculator = xc3;
            sphereCalculator.YCalculator = bc;
            sphereCalculator.ZCalculator = bc;
            sphere.positionCalculator = sphereCalculator;
            sphere.Scale = Vector3.One;
            meshes.Add(sphere);

            //cylinder
            Mesh cylinder = device.LoadJSONFileAsync("cylinder.babylon");
            MeshLightParameters cylinderLightParameters = new MeshLightParameters(Color.Green, 0.9f, 0.8f, 0.9f, 32);
            cylinder.lightParameters = cylinderLightParameters;
            cylinder.Rotation = new Vector3(0, 0, 0);
            cylinder.RotationStep = new Vector3(0, 0, 0);
            PositionCalculator cylinderCalculator = new PositionCalculator();
            Calculator zc = new Calculator(0, -5.1f, -5, 0);
            cylinderCalculator.XCalculator = bc;
            cylinderCalculator.YCalculator = bc;
            cylinderCalculator.ZCalculator = zc;
            cylinder.positionCalculator = cylinderCalculator;
            cylinder.Scale = Vector3.One;
            meshes.Add(cylinder);

            //floor
            floor = device.LoadJSONFileAsync("floor.babylon");
            MeshLightParameters floorLightParameters = new MeshLightParameters(Color.Black, 0.9f, 0.8f, 0.9f, 32);
            floor.lightParameters = floorLightParameters;
            floor.Rotation = new Vector3(0, 0, 0);
            floor.RotationStep = new Vector3(0, 0, 0);
            PositionCalculator floorCalculator = new PositionCalculator();
            floorCalculator.XCalculator = bc;
            floorCalculator.YCalculator = bc;
            floorCalculator.ZCalculator = bc;
            floor.positionCalculator = floorCalculator;
            floor.Scale = new Vector3(1.1f, 1.5f, 1);
            floor.ProjectCoordinatesTransform = new Vector3(0, 0, 1);
            meshes.Add(floor);

            //light sources initialize
            LightSource reflector1 = new LightSource()
            {
                color = Color.Yellow,
                Position = new Vector3(-6, 4, 2),
                Direction = new Vector3(0, -1, 0),
                CosLightAngle = 0.5f
            };

            LightSource reflector2 = new LightSource()
            {
                color = Color.Red,
                Position = new Vector3(5.5f, 3, 0),
                Direction = new Vector3(0, -1, 0),
                CosLightAngle = 0.2f
            };

            LightSource globalLight = new LightSource()
            {
                color = Color.White,
                Position = new Vector3(0, 100000, 0),
                Direction = new Vector3(0, -1, 0),
                CosLightAngle = -1f
            };

            LightsManager lightsManager = new LightsManager(0.5f);
            lightsManager.AddLightSource(reflector1);
            lightsManager.AddLightSource(reflector2);
            lightsManager.AddLightSource(globalLight);
            //lightsManager.AddLightSource(new LightSource() { color = Color.Yellow, Position = new Vector3(-5, 3, 0), Direction = new Vector3(-0.6f, -0.8f, 0), CosLightAngle = 0.5f });
            device.lightsManager = lightsManager;

            //default settings
            radioButtonCameraStatic.Checked = true;
            device.shadingMachine = new FlatShadingMachine();
            device.shadingMachine.fogMachine = new NoFogMachine();
            radioButtonFogDisabled.Checked = true;

            //start rendering
            SetTimer();
        }

        public void StartRendering()
        {
            device.Clear();

            // rotating pbjects during each frame rendered
            foreach (var m in meshes)
            {
                m.Rotation = new Vector3(m.Rotation.X + m.RotationStep.X, m.Rotation.Y + m.RotationStep.Y, m.Rotation.Z + m.RotationStep.Z);
                m.Position = new Vector3(m.positionCalculator.XCalculator.GetNext(), m.positionCalculator.YCalculator.GetNext(), m.positionCalculator.ZCalculator.GetNext());
            }

            for (int i = 0; i < meshes.Count; i++)
            {
                if (meshes[i].Name == "Suzanne")
                {
                    //change followingCamera settings
                    followingCamera.Target = meshes[i].Position;

                    //change dynamicCamera settings
                    dynamicCamera.Position = new Vector3(meshes[i].Position.X, meshes[i].Position.Y + 8, meshes[i].Position.Z);
                    var m = Matrix.RotationYawPitchRoll(0.05f, meshes[i].Rotation.X, meshes[i].Rotation.Z);
                    dynamicCamera.Target = Vector3.TransformCoordinate(dynamicCamera.Target, m);

                    //device.lightsManager.lightSources[0].Position = new Vector3(meshes[i].Position.X, meshes[i].Position.Y + 8, meshes[i].Position.Z);
                    //device.lightsManager.lightSources[0].Direction = (dynamicCamera.Target - device.lightsManager.lightSources[0].Position);
                    //device.lightsManager.lightSources[0].Direction.Normalize();
                    break;
                }
            }

            device.Render(activeCamera, meshes.ToArray());

            pictureBox1.Image = device.bmp.Bitmap;
        }

        public void SetTimer()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            StartRendering();
        }

        private void radioButtonFlat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFlat.Checked)
            {
                device.UseFlatShading();
            }
            return;
        }

        private void radioButtonGouraud_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGouraud.Checked)
            {
                device.UseGouraudShading();
            }
            return;
        }

        private void radioButtonPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPhong.Checked)
            {
                device.UsePhongShading();
            }
            return;
        }

        private void radioButtonCameraStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCameraStatic.Checked)
            {
                activeCamera = staticCamera;
            }
            return;
        }

        private void radioButtonFollowing_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFollowing.Checked)
            {
                activeCamera = followingCamera;
            }
            return;
        }

        private void radioButtonDynamic_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDynamic.Checked)
            {
                activeCamera = dynamicCamera;
            }
            return;
        }

        private void radioButtonFogEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFogEnabled.Checked)
            {
                device.AddFog(basicFogMachine);
            }
            return;
        }

        private void radioButtonFogDisabled_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFogDisabled.Checked)
            {
                device.RemoveFog();
            }
            return;
        }

        private void buttonAddFloor_Click(object sender, EventArgs e)
        {
            if (floorActive)
                return;
            meshes.Add(floor);
            floorActive = true;
        }

        private void buttonRemoveFloor_Click(object sender, EventArgs e)
        {
            if (!floorActive)
                return;
            meshes.Remove(floor);
            floorActive = false;
        }
    }
}
