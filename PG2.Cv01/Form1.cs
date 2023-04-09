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

namespace PG2.Cv01
{
    public partial class Form1 : Form
    {
        World world = new World();

        #region Properties

        // TODO: Do you have the guts to uncomment me? Go on, try
        
        Camera camera = new Camera(500, 500)  // Rendered picture size width x height
        {
            Position = new Vector3(20, 0, 0), // Camera position
            Target = new Vector3(0, 0, 0),    // Center point at which the camera is looking
            zNear = 0,
            zFar = Double.MaxValue,
        };

        #endregion


        public Form1()
        {
            InitializeComponent();
            WriteValues();
        }

        public void InitSceneAndLights()
        {
            // Init scene
            #region Models

            // zFar plane (Origin + normal), Color (R,G,B)
            world.Models.Add(new Plane(new Vector3(-5, 0, 0), new Vector3(1, 0, 0)) { Color = new Vector3(1, 0, 0) });
            // Left plane
            world.Models.Add(new Plane(new Vector3(0, -5, 0), new Vector3(0, 1, 0)) { Color = new Vector3(0, 1, 0) });
            // Right plane
            world.Models.Add(new Plane(new Vector3(0, 5, 0), new Vector3(0, -1, 0)) { Color = new Vector3(0, 1, 0) });
            // Bottom plane
            world.Models.Add(new Plane(new Vector3(0, 0, -5), new Vector3(0, 0, 1)) { Color = new Vector3(0, 0, 1) });
            // Top plane
            world.Models.Add(new Plane(new Vector3(0, 0, 5), new Vector3(0, 0, -1)) { Color = new Vector3(0, 0, 1) });
            // Sphere (Center + radius), Color (R,G,B)
            world.Models.Add(new Sphere(new Vector3(0, 0, -5), 3) { Color = new Vector3(0, 1, 1) });
            // Triangle (Vertex1, Vertex2, Vertex3), Color (R,G,B)
            world.Models.Add(new Triangle(new Vector3(0, 0, -4), new Vector3(4, 3, -4), new Vector3(0, 0, 3)) { Color = new Vector3(1, 1, 0) });

            #endregion

            // TODO: Do you have the guts to uncomment me? Go on, try
            
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
            Cursor = Cursors.WaitCursor;
            DateTime t0 = DateTime.Now;

            ReadValues();

            InitSceneAndLights();
                
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
