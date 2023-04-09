using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Mathematics;
using PG2.Rendering;

namespace PG2.Lighting
{
    public class Light
    {
        #region Properties

        // TODO: Define light Origin
        public Vector3 Origin;

        // TODO: Define light Intensity
        public double Intensity;

        // TODO: Declare light diffuse color
        public Vector3 lightDiffuseColor = new Vector3(1, 1, 1); //ako na cviku

        #endregion


        #region Lighting

        public virtual void SetLightRayAt(Vector3 point, Ray ray)
        {
        }

        #endregion
    }
}
