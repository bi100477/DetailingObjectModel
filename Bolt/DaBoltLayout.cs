using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public delegate void FuncOnNumBoltChanged();

    public class DaBoltLayout : DaInput
    {
        public string boltName { get; set; }
        public int numBolt { get; set; }
        public FuncOnNumBoltChanged OnNumBoltChanged { get; set; }      

        #region I/O

        private const string IOCaption = "<DaBoltLayout>";

        private const string IOTerminate = "</DaBoltLayout>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaBoltLayout() : base()
        {
            boltName = "M20";
            numBolt = 1;

            OnNumBoltChanged += OnNumBoltChangedDefault;
        }

        public override DaInType daInType()
        {
            return DaInType.BoltLayout;
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
            sw.Write("numBolt = " + numBolt);
            sw.Write("\n");

            sw.Write("boltName = " + boltName);
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

            line = sr.ReadLine().Replace("numBolt = ", "");
            numBolt = Convert.ToInt32(line);

            line = sr.ReadLine().Replace("boltName = ", "");
            boltName = line;

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

        private void OnNumBoltChangedDefault()
        {

        }
    }
}
