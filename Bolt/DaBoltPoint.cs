using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public class DaBoltPoint : DaInput
    {
        public double X { get; set; }
        public double Y { get; set; }

        #region I/O

        private const string IOCaption = "<DaBoltPoint>";

        private const string IOTerminate = "</DaBoltPoint>";

        private const int IOVersion = 1;

        #endregion I/O
        public DaBoltPoint() : base()
        {
            X = 0.0;
            Y = 0.0;
        }

        public DaBoltPoint((double, double) doublePair) : base()
        {
            X = doublePair.Item1;
            Y = doublePair.Item2;
        }

        public override DaInType daInType()
        {
            return DaInType.BoltPoint;
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
            sw.Write("X = " + X);
            sw.Write("\n");

            sw.Write("Y = " + Y);
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

            line = sr.ReadLine().Replace("X = ", "");
            X = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("Y = ", "");
            Y = Convert.ToDouble(line);

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

        public override string ToString()
        {
            return X.ToString() + "; " + Y.ToString();
        }    
    }
}
