using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using PG2.Rendering;
using PG2.Mathematics;
using PG2.Modeling;
using PG2.Shading;
using PG2.Lighting;

namespace PG2.Cv02
{
    public partial class Form1 : Form
    {
        #region Properties

        World world = new World();

        Camera camera = new Camera(500, 500)  // Rendered picture size width x height
        {
            Position = new Vector3(20, 0, 0), // Camera position
            Target = new Vector3(0, 0, 0),    // Center point at which the camera is looking
            //zNear = 0,
            zFar = Double.MaxValue,
        };

        #region Lights

        PointLight point = new PointLight()
        {
            Intensity = 1.0,
            Origin = new Vector3(1, -2, 4.5),
        };


        #endregion


        #endregion

        public Form1()
        {
            InitializeComponent();
            InitSceneAndLights();
            WriteValues();
        }

        public void InitSceneAndLights()
        {
            #region Shaders

            // Diffuse color, specular color, ambient color, shininnes
            Shader red = new Phong(new Vector3(1, 0, 0), new Vector3(0, 0, 0));
            Shader green = new Phong(new Vector3(0, 1, 0), new Vector3(0, 0, 0));
            Shader blue = new Phong(new Vector3(0, 0, 1), new Vector3(0, 0, 0));
            Shader yellow = new Phong(new Vector3(1, 1, 0));
            Shader cyan = new Phong(new Vector3(0, 1, 1), new Vector3(1, 1, 1), new Vector3(0.1, 0.1, 0.1), 50);
            Shader magenta = new Phong(new Vector3(1, 0, 1));

            #endregion


            // Init scene
            #region Models

            // zFar plane: Shader + Origin + normal
            world.Models.Add(new Plane(red, new Vector3(-7, 0, 0), new Vector3(1, 0, 0)));
            // Left plane
            world.Models.Add(new Plane(green, new Vector3(0, -7, 0), new Vector3(0, 1, 0)));
            // Right plane
            world.Models.Add(new Plane(green, new Vector3(0, 7, 0), new Vector3(0, -1, 0)));
            // Bottom plane
            world.Models.Add(new Plane(blue, new Vector3(0, 0, -7), new Vector3(0, 0, 1)));
            // Top plane
            world.Models.Add(new Plane(blue, new Vector3(0, 0, 7), new Vector3(0, 0, -1)));
            // Sphere: Shader + Center + radius
            world.Models.Add(new Sphere(cyan, new Vector3(-1, 0, -6), 3));
            // Triangle: Shader + Vertex1, Vertex2, Vertex3
            world.Models.Add(new Triangle(yellow, new Vector3(0, 0, -5), new Vector3(4, 3, -5), new Vector3(0, 0, 2)));
            // Block: Shader + BoundingBox(LeftBottom, TopRight)
            world.Models.Add(new Block(magenta, new Vector3(-1, 1, -7), new Vector3(3, 5, -4)));
            // Circle: Shader
            world.Models.Add(new Circle(yellow, new Vector3(0, -3, -5), new Vector3(0.2, 0.5, 1), 3));

            #endregion


            world.Lights.Add(point);

            camera.World = world;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(camera.Bitmap, 0, 0);

        }

        private void bRender_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DateTime t0 = DateTime.Now;

            ReadValues();

            camera.Render();

            DateTime t1 = DateTime.Now;
            Cursor = Cursors.Default;
            lRenderTime.Text = "Rendering: " + (t1 - t0).TotalMilliseconds.ToString("F0") + " ms";
            Invalidate();
        }

        Double Parse(String text)
        {
            NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowDecimalPoint;
            CultureInfo provider = Thread.CurrentThread.CurrentCulture;
            double res = 0;
            float rr = 0;
            double.TryParse(text, styles, provider, out res);
            float.TryParse(text, styles, provider, out rr);
            return res;
        }

        private void ReadValues()
        {
            camera.FovY = Parse(textBox1.Text);
            camera.Position.X = Parse(textBox2.Text);
            camera.Position.Y = Parse(textBox3.Text);
            camera.Position.Z = Parse(textBox4.Text);

            camera.Target.X = Parse(textBox5.Text);
            camera.Target.Y = Parse(textBox6.Text);
            camera.Target.Z = Parse(textBox7.Text);

            point.Origin.X = Parse(textBox8.Text);
            point.Origin.Y = Parse(textBox9.Text);
            point.Origin.Z = Parse(textBox10.Text);
        }

        private void WriteValues()
        {
            textBox1.Text = camera.FovY.ToString();

            textBox2.Text = camera.Position.X.ToString();
            textBox3.Text = camera.Position.Y.ToString();
            textBox4.Text = camera.Position.Z.ToString();

            textBox5.Text = camera.Target.X.ToString();
            textBox6.Text = camera.Target.Y.ToString();
            textBox7.Text = camera.Target.Z.ToString();
             
            textBox8.Text = point.Origin.X.ToString();
            textBox9.Text = point.Origin.Y.ToString();
            textBox10.Text = point.Origin.Z.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Output";
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                camera.Bitmap.Save(sfd.FileName, format);
        }

    }
}
