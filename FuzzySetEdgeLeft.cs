using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;


namespace Angora
{
    public class FuzzySetEdgeLeft : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public FuzzySetEdgeLeft()
          : base("FuzzySet Edge Left", "EdgeLeft",
              "FuzzySet Edge Left",
              "Angora", "Fuzzy Sets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Label", "L", "Label of the fuzzy set", GH_ParamAccess.item);
            pManager.AddNumberParameter("A", "A", "First point", GH_ParamAccess.item);
            pManager.AddNumberParameter("B", "B", "Second point", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FuzzySet", "FS", "EdgeLeft fuzzy set", GH_ParamAccess.item); 
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string Label = null;
            double A = double.NaN;
            double B = double.NaN;

            if (!DA.GetData(0, ref Label)) { return; }
            if (!DA.GetData(1, ref A)) { return; }
            if (!DA.GetData(2, ref B)) { return; }

            if (!Rhino.RhinoMath.IsValidDouble(A)) { return; }
            if (!Rhino.RhinoMath.IsValidDouble(B)) { return; }

            FuzzySet fSet = new FuzzySet(Label, new TrapezoidalFunction(
              (float)A, (float)B, TrapezoidalFunction.EdgeType.Left));

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
                return Resources.EdgeLeft;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e2b57d9e-549d-4f91-8ca3-1ec2536b9b80}"); }
        }
    }
}