using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Rendering;
using PG2.Mathematics;
using PG2.Shading;

namespace PG2.Modeling
{
    public class Model
    {
        public const double Eps = 1e-5;

        #region Properties

        public Shader Shader;

        #endregion

        #region Ray Tracing

        // Collide ray with object and return intersection and intersected object
        public virtual void Collide(Ray ray)
        {
        }

        #endregion
    }
}
