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
        #region Properties

        // Declare light Linear attenuation factor coefficient to 0.02
        public double lightLinearattenuation = 0.02;
        // Declare light Quadratic attenuation factor coefficient to 0.00
        public double lightQuadraticattenuation = 0.00;

        #endregion


        #region Lighting

        public override Double GetAttenuationFactor(Vector3 point)
        {
            double r = (Origin - point).Length;

            // TODO: Calculate light attenuation factor for point, use .Length method for the length of a vector
            double attentuationFactor = 1 / (double)(1 + lightLinearattenuation * r + lightQuadraticattenuation * Math.Pow(r, 2));
            return attentuationFactor; 
        }

        public override void SetLightRayAt(Vector3 point, Ray ray)
        {
            // TODO: Set normalized light vector from point to Origin of the light, use ray.Set() method
            ray.Set(point, (Origin - point).Normalized);
        }

        #endregion
    }
}
