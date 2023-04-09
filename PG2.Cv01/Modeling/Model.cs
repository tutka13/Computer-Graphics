using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Rendering;
using PG2.Mathematics;

namespace PG2.Modeling
{
    public class Model
    {
        public const Double Eps = 1e-5;

        #region Properties

        public Vector3 Color = new Vector3(0, 0, 0);

        #endregion


        #region Ray Tracing

        // Collide ray with object and return intersection and intersected object
        public virtual void Collide(Ray ray)
        {
        }

        #endregion
    }
}
