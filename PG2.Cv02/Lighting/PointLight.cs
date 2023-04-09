using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;

namespace PG2.Lighting
{
    public class PointLight : Light
    {
        #region Lighting

        public override void SetLightRayAt(Vector3 point, Ray ray)
        {
            // TODO: Set normalized light vector from point to Origin of the light, use ray.Set() method
            ray.Set(point, (Origin - point).Normalized);
        }

        #endregion
    }
}
