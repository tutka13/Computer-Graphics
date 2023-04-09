using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;
using PG2.Shading;

namespace PG2.Modeling
{
    public class Block : Model
    {
        #region Properties

        // TODO: Define AABB block object properties Min, Max (top-left and bottom-right)
        public Vector3 Min, Max;

        #endregion


        #region Init

        public Block()
        {
        }

        public Block(Shader shader, Vector3 min, Vector3 max)
        {
            // TODO: Initialize class members Shader (inherited from base Model object), Min, Max
            Min = min;
            Max = max;
            Shader = shader;
        }

        #endregion


        #region Raytracing

        public override void Collide(Ray ray)
        {
            Collide(ray, this);
        }

        // Collide ray with object and return:
        //   intersection ray.HitParameter, surface normal at intersection point ray.HitNormal and intersected object ray.HitModel
        public static void Collide(Ray ray, Block box)
        {
            // TODO: Compute ray-block intersection
            double t_0x = (box.Min.X - ray.Origin.X) / ray.Direction.X;
            double t_0y = (box.Min.Y - ray.Origin.Y) / ray.Direction.Y;
            double t_0z = (box.Min.Z - ray.Origin.Z) / ray.Direction.Z;
            double t_1x = (box.Max.X - ray.Origin.X) / ray.Direction.X;
            double t_1y = (box.Max.Y - ray.Origin.Y) / ray.Direction.Y;
            double t_1z = (box.Max.Z - ray.Origin.Z) / ray.Direction.Z;

            double t_minx = Math.Min(t_0x, t_1x);
            double t_miny = Math.Min(t_0y, t_1y);
            double t_minz = Math.Min(t_0z, t_1z);
            double t_maxx = Math.Max(t_0x, t_1x);
            double t_maxy = Math.Max(t_0y, t_1y);
            double t_maxz = Math.Max(t_0z, t_1z);

            double t_min = MathEx.Max3(t_minx, t_miny, t_minz);
            double t_max = MathEx.Min3(t_maxx, t_maxy, t_maxz);

            if (t_min <= t_max)  //prienik nastava len v tomto pripade
            {
                double t = t_min; 
                if (0 <= t && t < ray.HitParameter)
                {
                    ray.HitParameter = t;
                    ray.HitModel = box;

                    Vector3 normalVector = new Vector3();
                    Vector3 hitPoint = ray.GetHitPoint();

                    //vyratanie normaly
                    if (Math.Abs(hitPoint.X - box.Max.X) < Eps)
                    {
                        normalVector = new Vector3(1, 0, 0);
                    }
                    else if (Math.Abs(hitPoint.Y - box.Max.Y) < Eps)
                    {
                        normalVector = new Vector3(0, 1, 0);
                    }
                    else if (Math.Abs(hitPoint.Z - box.Max.Z) < Eps)
                    {
                        normalVector = new Vector3(0, 0, 1);
                    }
                    else if (Math.Abs(hitPoint.X - box.Min.X) < Eps)
                    {
                        normalVector = new Vector3(-1, 0, 0);
                    }
                    else if (Math.Abs(hitPoint.Y - box.Min.Y) < Eps)
                    {
                        normalVector = new Vector3(0, -1, 0);
                    }
                    else if (Math.Abs(hitPoint.Z - box.Min.Z) < Eps)
                    {
                        normalVector = new Vector3(0, 0, -1);
                    }
                    ray.HitNormal = normalVector;
                }
            }
        }
        #endregion
    }
}
