using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG2.Rendering;

namespace PG2.Modeling
{
    public class World
    {
        #region Properties

        // All primitives in the scene
        public List<Model> Models = new List<Model>();

        #endregion


        #region Ray Tracing

        public void Collide(Ray ray)
        {
            foreach (Model model in Models)
            {
                model.Collide(ray);
            }
        }

        #endregion
    }
}
