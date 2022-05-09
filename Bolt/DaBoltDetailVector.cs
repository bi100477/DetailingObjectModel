using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public class DaBoltDetailVector : DaBoltDetail
    {
        #region Create DaBoltDetail class

        public static DaBoltDetail CreateDaBoltDetailStandard(EBoltDetailType boltDetailType, DaBoltLayout boltlayout)
        {
            DaBoltDetail daBoltDetail = null;

            if (boltDetailType == EBoltDetailType.Vector)
            {                    
                daBoltDetail = new DaBoltDetailVector(boltlayout);
            }

            return daBoltDetail;
        }

        #endregion Create DaBoltDetail class

        #region I/O

        private const string IOCaption = "<DaBoltBetailStandard>";

        private const string IOTerminate = "</DaBoltBetailStandard>";

        private const int IOVersion = 1;

        #endregion I/O

        public double dS { get; set; }
        public double dE { get; set; }

        public DaBoltDetailVector(DaBoltLayout boltlayout) : base(boltlayout)
        {
            dS = 50.0;
            dE = 40.0;
        }

        public override EBoltDetailType boltDetailType()
        {
            return EBoltDetailType.Vector;
        }

        #region I/O

        #region write
        public override void Write(StreamWriter sw)
        {
            base.Write(sw);

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
            sw.Write("dS = " + dS);
            sw.Write("\n");

            sw.Write("dE = " + dE);
            sw.Write("\n");

            sw.Write(IOTerminate + "\n");
        }

        #endregion write

        #region read
        public override void Read(StreamReader sr)
        {
            base.Read(sr);

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

            line = sr.ReadLine().Replace("dS = ", "");
            dS = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("dE = ", "");
            dE = Convert.ToDouble(line);

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
