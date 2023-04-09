using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;

namespace PG2.Modeling
{
    public class Plane : Model
    {
        #region Properties

        // TODO: Define object properties Origin, Normal
        Vector3 Origin;
        Vector3 Normal;

        #endregion


        #region Init

        public Plane()
        {
        }

        public Plane(Vector3 origin, Vector3 normal)
        {
            // TODO: Initialize class members Origin, Normal
            Origin = origin;
            Normal = normal.Normalized;
        }

        #endregion


        #region Raytracing

        public override void Collide(Ray ray)
        {
            Collide(ray, this);
        }

        // Collide ray with object and return intersection ray.HitParameter and intersected object ray.HitModel
        public static void Collide(Ray ray, Plane plane)
        {
            // TODO: Compute ray-plane intersection
            double numerator = (plane.Origin - ray.Origin) * plane.Normal;
            double denominator = ray.Direction * plane.Normal;
            if (Math.Abs(denominator) > Eps)
            {
                double t = numerator / denominator;
                if (t >= Eps && ray.HitParameter > t)
                {
                    ray.HitParameter = t;
                    ray.HitModel = plane;
                }
            }           
        }

        #endregion
    }
}
