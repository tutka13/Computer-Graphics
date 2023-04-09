using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using System.Drawing;
using PG2.Modeling;
using PG2.Shading;
using PG2.Lighting;

namespace PG2.Rendering
{
    public class Camera
    {
        public struct HitPoint
        {
            // TODO: Define HitPoint structure variables: Position, Color, Normal
            public Vector3 Position, Color, Normal;
        }

        #region Properties

        // TODO: Define camera properties Position, Target
        public Vector3 Position;
        public Vector3 Target;

        // TODO: Declare Up vector to (0, 0, 1), FovY value to 45
        Vector3 Up = new Vector3(0, 0, 1);
        public double FovY = 45;

        // TODO: Define U, V, W vectors camera to world space
        Vector3 U, V, W;

        // TODO: Define frame(picture) properties Bitmap, Width, Height, Pixels buffer
        public Bitmap Bitmap;
        Vector3[] Pixels;
        int Width, Height;

        // TODO: Declare BgColor to (0, 0, 0)
        Vector3 BgColor = Vector3.Zero;

        public World World;

        // TODO: Define clipping planes zNear, zFar
        public double zFar;
        //public double zNear = 0;

        // TODO: Declare UseShadows to control shadows rendering to true
        public Boolean UseShadows = true;
        // TODO: Declare UseAttenuation to decrease light intensity by attenuation to true
        public Boolean UseLightAttenuation = true;

        #endregion

        #region Init

        public Camera(Int32 width, Int32 height)
        {
            // TODO: Initialize class members Width, Height, Bitmap, Pixels buffer
            Width = width;
            Height = height;
            Bitmap = new Bitmap(width, height);
            Pixels = new Vector3[width * height];
        }

        #endregion

        #region Buffer Acess

        public Vector3 GetPixel(Int32 i, Int32 j)
        {
            return Pixels[i + j * Width];
        }

        public void SetPixel(Int32 i, Int32 j, Vector3 color)
        {
            Pixels[i + j * Width] = color;
        }

        #endregion

        #region Rendering

        public void Render()
        {
            RayTrace();
            PresentFrame();
        }

        /// <summary>Derived from Computer Graphics - David Mount. /n
        /// Implementations can differ - make your own from scratch. 
        /// See http://goo.gl/q6Sz0 (page 84) and http://goo.gl/rB8J6 (page 9-10)
        /// </summary>
        public void RayTrace()
        {
            // TODO: Initialize camera (U, V, W)
            W = (Position - Target).Normalized;
            U = (Up % W).Normalized;
            V = (U % W).Normalized;

            // TODO: Compute perspective projection with FovY as a field of view
            double aspectRatio = Width / Height;
            double heightWindow = 2.0 * Math.Tan(MathEx.DegToRad(FovY) / 2.0);
            double widthWindow = heightWindow * aspectRatio;

            // TODO: Ray trace the scene. One ray is enough for one pixel
            for (int r = 0; r < Height; r++)
            {
                double rCamera = heightWindow * (1.0 * r / Height) - 1.0 * heightWindow / 2;
                for (int c = 0; c < Width; c++)
                {
                    // TODO: Create ray and calculate color with RayTrace()
                    //       Store color to Pixels bufer with SetPixel()
                    double cCamera = widthWindow * (1.0 * c / Width) - 1.0 * widthWindow / 2;

                    Vector3 vectorDirection = (cCamera * U + rCamera * V - W).Normalized;
                    Ray currentRay = new Ray(Position, vectorDirection, zFar);

                    Vector3 Color = RayTrace(currentRay);
                    SetPixel(c, r, Color);
                }
            }
        }

        public Vector3 RayTrace(Ray ray)
        {
            // TODO: Calculate ray intersection with all models (primitives) in World, use World.Collide()
            //       Return background color if intersection does not exists else calculate hitpoint color
            World.Collide(ray);
            HitPoint hitPoint = new HitPoint();

            if (ray.HitModel == null)
            {
                return BgColor;
            }
            else //vypocet farby
            {
                // TODO: Calculate hitPoint.Position, use ray.GetHitPoint()
                hitPoint.Position = ray.GetHitPoint();

                //hitPoint.Normal = ray.HitNormal;
                hitPoint.Normal = ray.HitNormal;

                // TODO: Set hitPoint.Color to ambient color for ray.HitModel object
                hitPoint.Color = ray.HitModel.Shader.GetAmbientColor(hitPoint.Position);

                // TODO: Create light ray
                Ray luc = new Ray();

                // TODO: Create and set view direction

                // For each light in the world do
                foreach (Light light in World.Lights)
                {
                    // TODO: setup light ray to current light, use light.SetLightRayAt()
                    light.SetLightRayAt(hitPoint.Position, luc);

                    // TODO: Collide light ray with scene to check for shadows, use World.Collide()
                    World.Collide(luc);

                    // TODO: Check if the nearest occlusion object is between light and hit point
                    bool inShadow = false;

                    if (UseShadows == true && hitPoint.Normal * luc.Direction > 0)
                    {
                        if ((hitPoint.Position - luc.GetHitPoint()).Length < (hitPoint.Position - light.Origin).Length) //objekt nie je v tieni
                        {
                            inShadow = true;
                        }
                    }

                        // TODO: Calculate attenuation factor
                        double attenuationFactor = 1;
                        if (UseLightAttenuation == true)
                        {
                            attenuationFactor = light.GetAttenuationFactor(hitPoint.Position);
                        }

                        // TODO: Evaluate local shading (e.g. phong-model) and accumulate color, use ray.HitModel.Shader.GetColor()
                        //       Don't forget set color to shadow color if hit point is inside a shadow

                        if (inShadow == false)
                        {
                            hitPoint.Color += 1.0 * ray.HitModel.Shader.GetColor(hitPoint.Position, hitPoint.Normal, (Position - hitPoint.Position).Normalized, luc.Direction, attenuationFactor, light);
                        }
                        else
                        {
                            double ambientIntensity = ray.HitModel.Shader.GetAmbientColor(hitPoint.Position).Length;
                            hitPoint.Color += ambientIntensity * ray.HitModel.Shader.GetColor(hitPoint.Position, hitPoint.Normal, (Position - hitPoint.Position).Normalized, luc.Direction, attenuationFactor, light);
                        }   
                    }
                    return hitPoint.Color;             
            }
        }

        // Create picture. Copy all the pixels from pixel buffer to the Bitmap
        // Color is clamped in post process
        public void PresentFrame()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // TODO: Retrieve color from Pixels buffer, use GetPixel()
                    //       Don't forget clamp color to max 1.0
                    //       Store pixel color to the Bitmap, use appropriate procedure
                    Vector3 CurrentColor = GetPixel(x, y);
                    CurrentColor.X = Math.Min(CurrentColor.X, 1);
                    CurrentColor.Y = Math.Min(CurrentColor.Y, 1);
                    CurrentColor.Z = Math.Min(CurrentColor.Z, 1);

                    CurrentColor.X = Math.Max(CurrentColor.X, 0);
                    CurrentColor.Y = Math.Max(CurrentColor.Y, 0);
                    CurrentColor.Z = Math.Max(CurrentColor.Z, 0);

                    Bitmap.SetPixel(x, y, Color.FromArgb(Convert.ToInt32(CurrentColor.X * 255), Convert.ToInt32(CurrentColor.Y * 255), Convert.ToInt32(CurrentColor.Z * 255)));
                }
            }
        }

        #endregion
    }
}
