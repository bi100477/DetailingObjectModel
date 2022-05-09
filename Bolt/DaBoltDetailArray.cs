using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public class DaBoltDetailArray : DaBoltDetail
    {
        #region Create DaBoltDetail class

        public static DaBoltDetail CreateDaBoltDetailSingle(EBoltDetailType boltDetailType, DaBoltLayout boltlayout)
        {
            DaBoltDetail daBoltDetail = null;

            if (boltDetailType == EBoltDetailType.Array)
            {
                daBoltDetail = new DaBoltDetailArray(boltlayout);
            }

            return daBoltDetail;
        }

        #endregion Create DaBoltDetail class

        #region I/O

        private const string IOCaption = "<DaBoltBetailSingle>";

        private const string IOTerminate = "</DaBoltBetailSingle>";

        private const int IOVersion = 1;

        #endregion I/O

        public List<DaBoltPoint> boltPoints { get; set; }

        public DaBoltDetailArray(DaBoltLayout boltlayout) : base(boltlayout)
        {
            boltPoints = new List<DaBoltPoint>();

            for (int i = 0; i < boltLayout.numBolt; i++)
            {
                boltPoints.Add(new DaBoltPoint((0.0, 0.0)));
            }

            boltLayout.OnNumBoltChanged += OnNumBoltChanged;
        }

        public override EBoltDetailType boltDetailType()
        {
            return EBoltDetailType.Array;
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
            sw.Write("numBoltPoint = " + boltPoints.Count);
            sw.Write("\n");

            if (boltPoints.Count > 0)
            {
                foreach (var boltPoint in boltPoints)
                {
                    boltPoint.Write(sw);
                }
            }

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

            line = sr.ReadLine().Replace("numBoltPoint = ", "");
            int numBoltPoint = Convert.ToInt32(line);

            if (numBoltPoint > 0)
            {
                for (int i = 0; i < numBoltPoint; i++)
                {
                    DaBoltPoint boltPoint = new DaBoltPoint();
                    boltPoint.Read(sr);

                    boltPoints.Add(boltPoint);
                }
            }

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

        public void OnNumBoltChanged()
        {
            int numBolt = boltLayout.numBolt;
            int numBoltOld = boltPoints.Count;

            if (numBolt == numBoltOld)
            {
                return;
            }
            else if (numBolt > numBoltOld)
            {
                for (int i = numBoltOld; i < numBolt; i++)
                {
                    boltPoints.Add(new DaBoltPoint((0.0, 0.0)));
                }

                if (boltPoints.Count != numBolt)
                {
                    throw new Exception("boltPoints.Count != numBolt");
                }
            }
            else
            {
                for (int i = numBoltOld-1; i >= numBolt; i--)
                {
                    boltPoints.RemoveAt(i);
                }

                if (boltPoints.Count != numBolt)
                {
                    throw new Exception("boltPoints.Count != numBolt");
                }
            }
        }
    }

}
