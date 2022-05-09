using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public class DaBoltType : DaInput
    {
        public string boltType { get; set; }
        public string boltGrade { get; set; }
        public string boltAssembly { get; set; }

        #region I/O

        private const string IOCaption = "<DaBoltType>";

        private const string IOTerminate = "</DaBoltType>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaBoltType() : base()
        {
            boltType = "14399-4";
            boltGrade = "10.9";
            boltAssembly = "Mu2S";
        }

        public override DaInType daInType()
        {
            return DaInType.BoltType;
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
            sw.Write("boltType = " + boltType);
            sw.Write("\n");

            sw.Write("boltGrade = " + boltGrade);
            sw.Write("\n");

            sw.Write("boltAssembly = " + boltAssembly);
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

            line = sr.ReadLine().Replace("boltType = ", "");
            boltType = line;

            line = sr.ReadLine().Replace("boltGrade = ", "");
            boltGrade = line;

            line = sr.ReadLine().Replace("boltAssembly = ", "");
            boltAssembly = line;

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
