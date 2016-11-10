using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;


namespace Angora
{
    public class FuzzySetPiecewise : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public FuzzySetPiecewise()
          : base("FuzzySet Piecewise", "Piecewise",
              "FuzzySet Piecewise",
              "Angora", "Fuzzy Sets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Label", "L", "Label of the fuzzy set", GH_ParamAccess.item);
            pManager.AddPointParameter("P", "Points", "Points", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FuzzySet", "FS", "Piecewise fuzzy set", GH_ParamAccess.item); 
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string Label = null;
            List<Point3d> Points = new List<Point3d>();
                   
            if (!DA.GetData(0, ref Label)) { return; }
            if (!DA.GetDataList(1, Points)) { return; }

                
            if (Points.Count == 0) { return; }


            /// Construct AForge Points
            AForge.Point[] Pts = new AForge.Point[Points.Count];
            double[] xx = new double[Points.Count];
            double[] yy = new double[Points.Count]; ;
            /// Truth must be in [0,1]
            for (int i = 0; i < Points.Count; i++)
            {
                if (!Points[i].IsValid) { return; }    
                xx[i] = Points[i].X;
                double y = Points[i].Y;
                if (y < 0) { y = 0; }   
                if (y > 1) { y = 1; }
                yy[i] = y;
            }
            
            for (int i = 0; i < Points.Count; i++)
            {
                Pts[i] = new AForge.Point((float)xx[i], (float) (yy[i]));
            }
            /// Construct FuzzySet
            FuzzySet fSet = new FuzzySet(Label, new PiecewiseLinearFunction(Pts));

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
                return Resources.Piecewise;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{030bff94-77b7-4763-8e58-6c702313ca8e}"); }
        }
    }
}