using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;
using PG2.Shading;

namespace PG2.Modeling
{
    public class Circle : Model
    {
        #region Properties

        // TODO: Define object properties: Origin, Normal, Radius
        Vector3 Origin;
        Vector3 Normal;
        double Radius;

        #endregion


        #region Init

        public Circle()
        {
        }

        public Circle(Shader shader, Vector3 origin, Vector3 normal, Double radius)
        {
            // TODO: Initialize class members Shader (inherited from base Model object), Origin, Normal, Radius;
            Origin = origin;
            Normal = normal;
            Radius = radius;
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
        public static void Collide(Ray ray, Circle circle)
        {
            // TODO: Compute ray-circle intersection
            double numerator = (circle.Origin - ray.Origin) * circle.Normal;
            double denominator = ray.Direction * circle.Normal;
            if (Math.Abs(denominator) > Eps)
            {
                double t = numerator / denominator;

                Vector3 condition = ray.Origin + t * ray.Direction - circle.Origin;

                if (t >= Eps && ray.HitParameter > t && condition.Length < circle.Radius)
                {
                    ray.HitParameter = t;
                    ray.HitModel = circle;

                    //vypocet normaly
                    if (ray.Direction * circle.Normal > 0)
                    {
                        ray.HitNormal = -circle.Normal;
                    }
                    else
                    {
                        ray.HitNormal = circle.Normal;
                    }
                }
            }
        }

        #endregion
    }
}
