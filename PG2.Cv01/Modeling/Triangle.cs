using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;

namespace PG2.Modeling
{
    public class Triangle : Model
    {
        #region Properties

        // TODO: Define object properties Vertex1, Vertex2, Vertex3
        Vector3 Vertex1;
        Vector3 Vertex2;
        Vector3 Vertex3;

        #endregion


        #region Init

        public Triangle()
        {
        }

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            // TODO: Initialize class members Vertex1, Vertex2, Vertex3
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
        }

        #endregion


        #region Raytracing

        public override void Collide(Ray ray)
        {
            Collide(ray, this);
        }

        // Collide ray with object and return intersection ray.HitParameter and intersected object ray.HitModel
        public static void Collide(Ray ray, Triangle triangle)
        {
            // TODO: Compute ray-sphere intersection 
            // You can use Möller–Trumbore intersection algorithm (http://en.wikipedia.org/wiki/Möller–Trumbore_intersection_algorithm)
            
            //Moller-Trumbore algorithm
            double a, f, u, v;
            Vector3 edge1, edge2, h, s, q;
            
            edge1 = triangle.Vertex2 - triangle.Vertex1;
            edge2 = triangle.Vertex3 - triangle.Vertex1;

            h = edge2 % ray.Direction;
            a = edge1 * h;

            if (Math.Abs(a) > Eps)
            {
                f = 1.0 / a;
                s = ray.Origin - triangle.Vertex1;
                u = f * h * s;
                if (u > 0 && u < 1)
                {
                    q = edge1 % s;
                    v = f * q * ray.Direction;
                    if (v > 0 && u + v < 1)
                    {
                        double t = f * q * edge2;
                        if (ray.HitParameter > t && t > Eps)
                        {
                            ray.HitParameter = t;
                            ray.HitModel = triangle;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
