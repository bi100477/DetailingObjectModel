using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bolt
{
    public delegate DaBoltDetail CreateDaBoltDetailClassFunc(EBoltDetailType boltDetailType, DaBoltLayout boltlayout);

    public abstract class DaBoltDetail : DaInput
    {
        #region Create classes

        #region Create DaBoltDetail class
        public static List<CreateDaBoltDetailClassFunc> createDaBoltDetailClassFuncs { get; set; }

        public static DaBoltDetail CreateDaBoltDetailClass(EBoltDetailType boltDetailType, DaBoltLayout boltlayout)
        {
            DaBoltDetail daBoltDetail = null;

            for (int i = 0; i < createDaBoltDetailClassFuncs.Count; i++)
            {
                daBoltDetail = createDaBoltDetailClassFuncs[i](boltDetailType, boltlayout);

                if (daBoltDetail != null)
                {
                    break;
                }
            }

            return daBoltDetail;
        }

        #endregion Create DaBoltDetail class

        static DaBoltDetail()
        {
            createDaBoltDetailClassFuncs = new List<CreateDaBoltDetailClassFunc>();

            createDaBoltDetailClassFuncs.Add(DaBoltDetailVector.CreateDaBoltDetailStandard);
            createDaBoltDetailClassFuncs.Add(DaBoltDetailArray.CreateDaBoltDetailSingle);
        }

        #endregion Create classes

        #region I/O

        private const string IOCaption = "<DaBoltBetail>";

        private const string IOTerminate = "</DaBoltDetail>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaBoltLayout boltLayout { get; set; }
       
        public DaBoltDetail(DaBoltLayout boltlayout) : base()
        {
            boltLayout = boltlayout;
        }

        public abstract EBoltDetailType boltDetailType();

        public override DaInType daInType()
        {
            return DaInType.BoltDetail;
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
