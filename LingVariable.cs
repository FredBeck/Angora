using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;

namespace Angora
{
    public class LingVariable : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public LingVariable()
          : base("LinguisticVariable", "LingVar",
              "Linguistic Variable",
              "Angora", "Fuzzy Sets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "N", "Name of the Linguistic variable", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Interval", "I", "Interval", GH_ParamAccess.item);
            pManager.AddGenericParameter("FuzzySets", "FS", "Fuzzy sets", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("LinguisticVariable", "LV", "Linguistic Variable", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string Name = null;
            Interval Interval = new Interval();
            List < FuzzySet > fSets = new List<FuzzySet>();


            if (!DA.GetData(0, ref Name)) { return; }
            if (!DA.GetData(1, ref Interval)) { return; }
            if (!DA.GetDataList(2, fSets)) { return; }

            LinguisticVariable lingVar = new LinguisticVariable(Name, (float)Interval.Min, (float)Interval.Max);

            foreach (object x in fSets)
            {
                FuzzySet func = (FuzzySet)x;
                lingVar.AddLabel(func);
            }

            DA.SetData(0, lingVar);
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
                return Resources.LingVar;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{68eff152-f86f-4be7-91a5-84f4a76f5304}"); }
        }
    }
}