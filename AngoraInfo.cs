using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Angora
{
    public class AngoraInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Angora";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Resources.Angora_24x24;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Fuzzy logic grasshopper components";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("ea005f10-0eb4-4527-8935-7bae9f7e3f18");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Frederic Becquelin";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "capitaine.fred@gmail.com";
            }
        }
    }
}
