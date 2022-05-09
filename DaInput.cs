using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel
{
    public abstract class DaInput : BaObject
    {
        #region Params
        public string Tag { get; set; }
        public bool Create { get; set; }

        #region I/O

        private const string IOCaption = "<DaObject>";

        private const string IOTerminate = "</DaObject>";

        private const int IOVersion = 1;

        #endregion I/O

        #endregion Params

        #region Constructor

        protected DaInput() : base()
        {
            Create = true;
        }

        #endregion Constructor

        #region Interface

        public override BaObType baObType()
        {
            return BaObType.Data;
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
            sw.Write("Tag = " + Tag);
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
            string line = sr.ReadLine().Replace("Tag = ", "");
            Tag = line;

            //skip termination string
            if (sr.ReadLine() != IOTerminate)
            {
                throw new Exception("sr.ReadLine() != IOTerminate");
            }
        }

        #endregion read

        #endregion I/O

        public abstract DaInType daInType();

        public abstract bool SetDataFromDialog();

        public virtual bool SetConnectionsFromDialog()
        {
            return false;
        }

        #endregion Interface

    }
}
