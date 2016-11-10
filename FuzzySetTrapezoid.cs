using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;

namespace Angora
{
    public class FuzzySetTrapezoid : GH_Component
    {

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public FuzzySetTrapezoid()
          : base("FuzzySet Trapezoid", "Trapezoid",
              "FuzzySet Trapezoid",
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
            pManager.AddNumberParameter("C", "C", "Third point", GH_ParamAccess.item);
            pManager.AddNumberParameter("D", "D", "Fourth point", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FuzzySet", "FS", "Trapezoid fuzzy set", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string Label = null;
            double A = double.NaN;
            double B = double.NaN;
            double C = double.NaN;
            double D = double.NaN;

            if (!DA.GetData(0, ref Label)) { return; }
            if (!DA.GetData(1, ref A)) { return; }
            if (!DA.GetData(2, ref B)) { return; }
            if (!DA.GetData(3, ref C)) { return; }
            if (!DA.GetData(4, ref D)) { return; }

            if (!Rhino.RhinoMath.IsValidDouble(A)) { return; }
            if (!Rhino.RhinoMath.IsValidDouble(B)) { return; }
            if (!Rhino.RhinoMath.IsValidDouble(C)) { return; }
            if (!Rhino.RhinoMath.IsValidDouble(D)) { return; }

            FuzzySet fSet = new FuzzySet(Label, new TrapezoidalFunction(
              (float)A, (float)B, (float)C, (float)D));

            DA.SetData(0, fSet);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Resources.Trapezoid;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{8b834b7a-e284-4bce-963d-7af18a3c4787}"); }
        }
    }
}
