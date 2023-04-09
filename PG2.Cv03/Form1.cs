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

namespace PG2.Cv03
{
    public partial class Form1 : Form
    {
        #region Properties

        World world = new World();

        // TODO: Do you have the guts to uncomment me? Go on, try
        
        Camera camera = new Camera(500, 500)  // Rendered picture size width x height
        {
            Position = new Vector3(20, 0, 0), // Camera position
            Target = new Vector3(0, 0, 0),    // Center point at which the camera is looking
            //zNear = 0,
            zFar = Double.MaxValue,
        };
        

        #region Lights

        // TODO: Do you have the guts to uncomment me? Go on, try
        
        PointLight point1 = new PointLight()
        {
            Intensity = 0.5,
            Origin = new Vector3(4, -3, 6),
        };

        PointLight point2 = new PointLight()
        {
            Intensity = 0.5,
            Origin = new Vector3(4, +3, 6),
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

            // Diffuse color
            Shader red = new Phong(new Vector3(1, 0, 0));
            Shader green = new Phong(new Vector3(0, 1, 0));
            Shader blue = new Phong(new Vector3(0, 0, 1));
            Shader yellow = new Phong(new Vector3(1, 1, 0));
            Shader magenta = new Phong(new Vector3(1, 0, 1));
            // Diffuse color, specular color, ambient color, shininnes
            Shader cyan = new Phong(new Vector3(0, 1, 1), new Vector3(1, 1, 1), new Vector3(0.1, 0.1, 0.1), 50);
            Shader white = new Phong(new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0.1, 0.1, 0.1), 0);
            Shader wGreen = new Phong(new Vector3(0.3, 0.5, 0.3), new Vector3(0, 0, 0), new Vector3(0.1, 0.1, 0.1), 0);

            Shader checker = new Checker(white, wGreen, 1.251);

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
            world.Models.Add(new Plane(checker, new Vector3(0, 0, -6), new Vector3(0, 0, 1)));
            // Top plane
            world.Models.Add(new Plane(blue, new Vector3(0, 0, 7), new Vector3(0, 0, -1)));
            // Sphere: Shader + Center + radius
            world.Models.Add(new Sphere(cyan, new Vector3(0, 0, -2), 2));
            // Triangle: Shader + Vertex1, Vertex2, Vertex3
            world.Models.Add(new Triangle(yellow, new Vector3(0, 0, -4), new Vector3(4, 3, -4), new Vector3(0, 0, 3)));
            // Block: Shader + BoundingBox(LeftBottom, TopRight)
            world.Models.Add(new Block(magenta, new Vector3(-1, -5, -5), new Vector3(3, -1, -2)));

            #endregion

            // TODO: Do you have the guts to uncomment me? Go on, try
            
            world.Lights.Add(point1);
            world.Lights.Add(point2);

            camera.World = world;
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // TODO: Do you have the guts to uncomment me? Go on, try
            
            g.DrawImage(camera.Bitmap, 0, 0);
            
        }

        private void bRender_Click(object sender, EventArgs e)
        {
            // TODO: Do you have the guts to uncomment me? Go on, try
            
            camera.UseShadows = cbUseShadows.Checked;
            camera.UseLightAttenuation = cbUseLightAttenuation.Checked;
            

            Cursor = Cursors.WaitCursor;
            DateTime t0 = DateTime.Now;

            ReadValues();

            // TODO: Do you have the guts to uncomment me? Go on, try
            
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
            // TODO: Do you have the guts to uncomment me? Go on, try
            
            camera.FovY = Parse(textBox1.Text);
            camera.Position.X = Parse(textBox2.Text);
            camera.Position.Y = Parse(textBox3.Text);
            camera.Position.Z = Parse(textBox4.Text);

            camera.Target.X = Parse(textBox5.Text);
            camera.Target.Y = Parse(textBox6.Text);
            camera.Target.Z = Parse(textBox7.Text);

            point1.Origin.X = Parse(textBox8.Text);
            point1.Origin.Y = Parse(textBox9.Text);
            point1.Origin.Z = Parse(textBox10.Text);

            point2.Origin.X = Parse(textBox11.Text);
            point2.Origin.Y = Parse(textBox12.Text);
            point2.Origin.Z = Parse(textBox13.Text);
            
        }

        private void WriteValues()
        {
            // TODO: Do you have the guts to uncomment me? Go on, try
            
            textBox1.Text = camera.FovY.ToString();

            textBox2.Text = camera.Position.X.ToString();
            textBox3.Text = camera.Position.Y.ToString();
            textBox4.Text = camera.Position.Z.ToString();

            textBox5.Text = camera.Target.X.ToString();
            textBox6.Text = camera.Target.Y.ToString();
            textBox7.Text = camera.Target.Z.ToString();
             
            textBox8.Text = point1.Origin.X.ToString();
            textBox9.Text = point1.Origin.Y.ToString();
            textBox10.Text = point1.Origin.Z.ToString();

            textBox11.Text = point2.Origin.X.ToString();
            textBox12.Text = point2.Origin.Y.ToString();
            textBox13.Text = point2.Origin.Z.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: Do you have the guts to uncomment me? Go on, try
            
           SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Output";
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               camera.Bitmap.Save(sfd.FileName, format);
            
        }

    }
}
