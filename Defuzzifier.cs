using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;


namespace Angora
{
    public class Defuzzifier : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Defuzzifier()
          : base("Defuzzifier", "Defuzzifier",
              "Defuzzifier",
              "Angora", "Main")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("InferenceSystem", "InfSys", "Fuzzy inference system", GH_ParamAccess.item);
            pManager.AddTextParameter("Variable", "Var", "Name of the variable to defuzzify", GH_ParamAccess.item);
            pManager.AddNumberParameter("FailsafeValue", "FailSafeVal", "Failsafe value", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Value", "Val", "Defuzzified value", GH_ParamAccess.item);
            pManager.AddBooleanParameter("UsedFailsafe", "FS", "True if Failsafe was used", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            List<double> NumInputs = new List<double>();
            List<LinguisticVariable> InputVars = new List<LinguisticVariable>();
            List<LinguisticVariable> OutputVars = new List<LinguisticVariable>();
            List<string> Rules = new List<string>();



            AForge.Fuzzy.InferenceSystem IS = null;
            string VarName = null;
            double FailsafeVal = double.NaN;

            if (!DA.GetData(0, ref IS)) { return; }
            if (!DA.GetData(1, ref VarName)) { return; }
            if (!DA.GetData(2, ref FailsafeVal)) { return; }

            try
            {
                double outVal = IS.Evaluate(VarName);
                DA.SetData(0, outVal);
                DA.SetData(1, false);
            }
            catch (Exception)
            {
                DA.SetData(0, FailsafeVal);
                DA.SetData(1, true);
            }
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
                return Resources.Defuzzifier;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{83ea21d6-b842-488a-87ab-320779d541d8}"); }
        }
    }
}