using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using AForge;
using AForge.Fuzzy;

namespace Angora
{
    public class InferenceSystem : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public InferenceSystem()
          : base("InferenceSystem", "InfSys",
              "Fuzzy inferrence system",
              "Angora", "Main")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Numeric_Inputs", "NumInputs", "Numeric inputs", GH_ParamAccess.list);
            pManager.AddGenericParameter("Input_linguistic_variables", "InputVars", "Input Lingustic Variables", GH_ParamAccess.list);
            pManager.AddGenericParameter("Output_linguistic_variables", "OutputVars", "Output Lingustic Variables", GH_ParamAccess.list);
            pManager.AddTextParameter("Rules", "Rules", "Set of fuzzy rules", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Inference_system", "InfSys", "Fuzzy inference system", GH_ParamAccess.item);
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

            if (!DA.GetDataList(0, NumInputs)) { return; }
            if (!DA.GetDataList(1, InputVars)) { return; }
            if (!DA.GetDataList(2, OutputVars)) { return; }
            if (!DA.GetDataList(3, Rules)) { return; }

            // creating the database
            Database fuzzyDB = new Database();

            foreach (object x in InputVars)
            {
                LinguisticVariable LV = (LinguisticVariable) x;
                fuzzyDB.AddVariable(LV);
            }
            foreach (object y in OutputVars)
            {
                LinguisticVariable LV = (LinguisticVariable) y;
                fuzzyDB.AddVariable(LV);
            }


            // creating the inference system
            AForge.Fuzzy.InferenceSystem InfrSys = new AForge.Fuzzy.InferenceSystem(fuzzyDB, new AForge.Fuzzy.CentroidDefuzzifier(1000));

            int C = Rules.Count;
            for (int i = 0; i <= C - 1; i++)
            {
                string ruleName = "Rule" + i;
                InfrSys.NewRule(ruleName, Rules[i]);
            }

            // Setting inputs
            int D = InputVars.Count;
            for (int j = 0; j <= D - 1; j++)
            {
                LinguisticVariable LV = (LinguisticVariable) InputVars[j];
                double val = NumInputs[j];
                InfrSys.SetInput(LV.Name, Convert.ToSingle(val));
            }

            DA.SetData(0, InfrSys);
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
                return Resources.Angora_24x24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{0ed2a63b-6aca-4480-84db-3edf86d1548b}"); }
        }
    }
}