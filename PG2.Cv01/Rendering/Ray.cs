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

        // TODO: Define ray properties Origin, Direction, HitParameter
        public Vector3 Origin;
        public Vector3 Direction;
        public double HitParameter;
        public double ZNear;

        // TODO: Declare HitModel to null
        public Model HitModel = null;

        #endregion

        #region Init

        public Ray()
        {           
        }

        public Ray(Vector3 origin, Vector3 direction, Double zNear, Double zFar)
        {
            // TODO: Init ray properties Origin, Direction. Set HitParameter to the zFar
            Origin = origin;
            Direction = direction.Normalized;
            ZNear = zNear;
            HitParameter = zFar;
        }

        #endregion
    }
}
