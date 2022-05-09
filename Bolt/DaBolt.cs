using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public class DaBolt : DaInput
    {
        public DaBoltLayout boltLayout { get; set; }
        public DaBoltType boltType { get; set; }
        public DaBoltOffset boltOffset { get; set; }
        public EBoltDetailType boltDetailType { get; set; }
        public List<DaBoltDetail> boltDetails { get; set; }

        #region I/O

        private const string IOCaption = "<DaBolt>";

        private const string IOTerminate = "</DaBolt>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaBolt(string tag) : base()
        {
            Tag = tag;

            boltLayout = new DaBoltLayout();
            boltType = new DaBoltType();
            boltOffset = new DaBoltOffset();
            boltDetailType = EBoltDetailType.Vector;

            boltDetails = new List<DaBoltDetail>();

            foreach (EBoltDetailType item in Enum.GetValues(typeof(EBoltDetailType))) 
            {
                DaBoltDetail daBoltDetail = DaBoltDetail.CreateDaBoltDetailClass(item, boltLayout);

                if (daBoltDetail != null)
                {
                    boltDetails.Add(daBoltDetail);
                }                
            }
        }

        public override DaInType daInType()
        {
            return DaInType.Bolt;
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
            boltLayout.Write(sw);
            boltType.Write(sw);
            boltOffset.Write(sw);

            sw.Write("boltDetailType = " + boltDetailType);
            sw.Write("\n");

            boltDetails[(int)boltDetailType].Write(sw);

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
            boltLayout.Read(sr);
            boltType.Read(sr);
            boltOffset.Read(sr);

            string line;

            line = sr.ReadLine().Replace("boltDetailType = ", "");
            boltDetailType = Enum.Parse<EBoltDetailType>(line);

            boltDetails[(int)boltDetailType].Read(sr);

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
