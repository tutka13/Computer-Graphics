using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Modeling;

namespace PG2.Rendering
{
    public class Ray
    {
        #region Properties

        // TODO: Define ray properties Origin, Direction, HitParameter, HitNormal
        public Vector3 Origin;
        public Vector3 Direction;
        public double HitParameter;
        public Vector3 HitNormal;
        //public double ZNear;

        // TODO: Declare HitModel to null
        public Model HitModel = null;

        #endregion


        #region Init

        public Ray()
        {           
        }

        // Init ray
        public Ray(Vector3 origin, Vector3 direction, Double zFar)
        {
            Set(origin, direction, zFar);
        }

        // Set ray properties for already created ray
        public void Set(Vector3 origin, Vector3 direction, Double zFar = Double.MaxValue)
        {
            // TODO: Init ray class members Origin, Direction. Set HitParameter to the zFar
            Origin = origin;
            Direction = direction.Normalized;
            //ZNear = zNear;
            HitParameter = zFar;
        }

        // Return hit point of current ray
        public Vector3 GetHitPoint()
        {
            // TODO: calculate hit point position on the ray, use HitParameter value;
            return Origin + Direction * HitParameter;
        }

        #endregion
    }
}
