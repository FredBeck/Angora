using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;


namespace Angora
{
    public class FuzzySetCrisp : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public FuzzySetCrisp()
          : base("FuzzySet Crisp", "Crisp",
              "FuzzySet Crisp",
              "Angora", "Fuzzy Sets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Label", "L", "Label of the fuzzy set", GH_ParamAccess.item);
            pManager.AddNumberParameter("A", "A", "Value", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FuzzySet", "FS", "Crisp fuzzy set", GH_ParamAccess.item); 
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string Label = null;
            double A = double.NaN;

            if (!DA.GetData(0, ref Label)) { return; }
            if (!DA.GetData(1, ref A)) { return; }

            if (!Rhino.RhinoMath.IsValidDouble(A)) { return; }

            FuzzySet fSet = new FuzzySet(Label, new SingletonFunction(
      (float)A));   

            DA.SetData(0, fSet);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resources.Crisp;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{085845b4-cfcf-4a38-91bf-a7ee26f7b034}"); }
        }
    }
}