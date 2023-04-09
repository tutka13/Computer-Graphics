using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;
using PG2.Shading;

namespace PG2.Modeling
{
    public class Sphere : Model
    {
        #region Properties

        // TODO: Define object properties: Origin, Radius
        public Vector3 Origin;
        public double Radius;

        #endregion


        #region Init

        public Sphere()
        {
        }

        public Sphere(Shader shader, Vector3 origin, Double radius)
        {
            // TODO: Initialize class members Shader (inherited from base Model object), Origin, Radius
            Origin = origin;
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
        public static void Collide(Ray ray, Sphere sphere)
        {
            // TODO: Compute ray-sphere intersection
            Vector3 u = ray.Direction;
            Vector3 v = sphere.Origin - ray.Origin;

            double t_0 = u * v;
            double ySquared = v * v - Math.Pow(t_0, 2);
            double xSquared = Math.Pow(sphere.Radius, 2) - ySquared;

            if (xSquared >= Eps)
            {
                double x = Math.Sqrt(xSquared);
                if (x < Eps && ray.HitParameter > x)
                {
                    ray.HitParameter = 0;
                    ray.HitModel = sphere;

                }
                else if (x > Eps)
                {
                    //dva priesecniky, chceme z nich vybrat ten, ktory vidime
                    double t;
                    if ((t_0 + x) * (t_0 - x) < Eps)
                    {
                        t = Math.Max(t_0 - x, t_0 + x);
                    }
                    else
                    {
                        t = Math.Min(t_0 - x, t_0 + x);
                    }

                    if (t > Eps && ray.HitParameter > t)
                    {
                        ray.HitParameter = t;
                        ray.HitModel = sphere;

                        Vector3 hitPoint = ray.GetHitPoint();

                        //vyratanie normaly: stred sfery a bod prieniku
                        Vector3 normalVector = (hitPoint - sphere.Origin).Normalized;
                        ray.HitNormal = normalVector;
                    }
                }
                else
                {
                    //bez priesecnikov
                }

            }
        }

        #endregion
    }
}
