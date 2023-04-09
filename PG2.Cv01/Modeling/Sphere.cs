using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;

namespace PG2.Modeling
{
    public class Sphere : Model
    {
        #region Properties

        // TODO: Define object properties Origin, Radius
        Vector3 Origin;
        double Radius;

        #endregion


        #region Init

        public Sphere()
        {
        }

        public Sphere(Vector3 origin, Double radius)
        {
            // TODO: Initialize class members Origin, Radius
            Origin = origin;
            Radius = radius;
        }

        #endregion


        #region Raytracing

        public override void Collide(Ray ray)
        {
            Collide(ray, this);
        }

        // Collide ray with object and return intersection ray.HitParameter and intersected object ray.HitModel
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
                    //jeden priesencnik
                    ray.HitParameter = t_0;
                    ray.HitModel = sphere;

                }
                else if (x > Eps)
                {
                    //dva priesecniky, chceme z nich vybrat ten, ktory vidime
                    double t;
                    if ((t_0 + x) * (t_0 - x) < Eps)
                    {
                        t = Math.Max(t_0 - x , t_0 + x);
                    }
                    else
                    {
                        t = Math.Min(t_0 - x, t_0 + x);
                    }

                    if (t > Eps && ray.HitParameter > t)
                    {
                        ray.HitParameter = t;
                        ray.HitModel = sphere;
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
