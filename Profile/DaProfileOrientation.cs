using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Profile
{
    public class DaProfileOrientation : DaInput
    {
        public EProfileOrientation profileOrientation { get; set; }
        public double inPlaneOffset { get; set; }
        public double outPlaneOffset { get; set; }

        #region I/O

        private const string IOCaption = "<Da_ProfileOrientation>";

        private const string IOTerminate = "</Da_ProfileOrientation>";

        private const int IOVersion = 1;

        #endregion I/O


        public DaProfileOrientation(string tag) : base()
        {
            Tag = tag;

            profileOrientation = EProfileOrientation.Front;
            inPlaneOffset = 0.0;
            outPlaneOffset = 0.0;            
        }

        public override DaInType daInType()
        {
            throw new NotImplementedException();
        }

        public override bool SetDataFromDialog()
        {
            throw new NotImplementedException();
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
            base.Write(sw);

            switch (ver)
            {
                case 1: WriteVer01(sw); break;
            }
        }

        private void WriteVer01(StreamWriter sw)
        {
            sw.Write("profileOrientation = " + profileOrientation);
            sw.Write("\n");

            sw.Write("inPlaneOffset = " + inPlaneOffset);
            sw.Write("\n");

            sw.Write("outPlaneOffset = " + outPlaneOffset);
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
            base.Read(sr);

            switch (ver)
            {
                case 1: ReadVer01(sr); break;
            }
        }

        private void ReadVer01(StreamReader sr)
        {
            string line;

            line = sr.ReadLine().Replace("profileOrientation = ", "");
            profileOrientation = Enum.Parse<EProfileOrientation>(line);

            line = sr.ReadLine().Replace("inPlaneOffset = ", "");
            inPlaneOffset = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("outPlaneOffset = ", "");
            outPlaneOffset = Convert.ToDouble(line);

            //skip termination string
            if (sr.ReadLine() != IOTerminate)
            {
                throw new Exception("sr.ReadLine() != IOTerminate");
            }

        }

        #endregion read

        #endregion I/O
    }
}
