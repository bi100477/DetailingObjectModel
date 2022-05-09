using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Profile
{
    public class DaProfilePlaneOffsets : DaInput
    {
        public double offInPlaneFront { get; set; }
        public double offOutPlaneFront { get; set; }
        public double offInPlaneBack { get; set; }
        public double offOutPlaneBack { get; set; }

        #region I/O

        private const string IOCaption = "<DaProfileEndOffsets>";

        private const string IOTerminate = "</DaProfileEndOffsets>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaProfilePlaneOffsets() : base()
        {
            offInPlaneFront = 0.0;
            offOutPlaneFront = 0.0;
            offInPlaneBack = 0.0;
            offOutPlaneBack = 0.0;
        }

        public override DaInType daInType()
        {
            return DaInType.ProfileType;
        }

        #region I/O

        #region write
        public override void Write(StreamWriter sw)
        {
            sw.Write(IOCaption);
            sw.Write("\n");

            sw.Write(IOVersion); //version
            sw.Write("\n");

            WriteVer(sw, IOVersion);
        }

        private void WriteVer(StreamWriter sw, int ver)
        {
            switch (ver)
            {
                case 1: WriteVer01(sw); break;
            }
        }

        private void WriteVer01(StreamWriter sw)
        {
            sw.Write("offInPlaneFront = " + offInPlaneFront);
            sw.Write("\n");

            sw.Write("offOutPlaneFront = " + offOutPlaneFront);
            sw.Write("\n");

            sw.Write("offInPlaneBack = " + offInPlaneBack);
            sw.Write("\n");

            sw.Write("offOutPlaneBack = " + offOutPlaneBack);
            sw.Write("\n");

            sw.Write(IOTerminate + "\n");
        }

        #endregion write

        #region read
        public override void Read(StreamReader sr)
        {
            if (sr.ReadLine() != IOCaption)
            {
                throw new Exception("sr.ReadLine() != IOCaption");
            }

            var line = sr.ReadLine();
            int ver = Convert.ToInt32(line);

            ReadVer(sr, ver);
        }

        private void ReadVer(StreamReader sr, int ver)
        {
            switch (ver)
            {
                case 1: ReadVer01(sr); break;
            }
        }

        private void ReadVer01(StreamReader sr)
        {
            string line;

            line = sr.ReadLine().Replace("offInPlaneFront = ", "");
            offInPlaneFront = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("offOutPlaneFront = ", "");
            offOutPlaneFront = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("offInPlaneBack = ", "");
            offInPlaneBack = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("offOutPlaneBack = ", "");
            offOutPlaneBack = Convert.ToDouble(line);

            //skip termination string
            if (sr.ReadLine() != IOTerminate)
            {
                throw new Exception("sr.ReadLine() != IOTerminate");
            }
        }

        #endregion read

        #endregion I/O

        public override bool SetDataFromDialog()
        {
            throw new NotImplementedException();
        }
    }
}
