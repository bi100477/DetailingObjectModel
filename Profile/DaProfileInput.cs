using DetailingObjectModel.Bolt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Profile
{
    public class DaProfileInput : DaInput
    {
        public DaProfile daProfile { get; set; }
        public DaBolt daBolt { get; set; }

        #region I/O

        private const string IOCaption = "<Da_ProfileInput>";

        private const string IOTerminate = "</Da_ProfileInput>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaProfileInput(string tag) : base()
        {
            Tag = tag;

            daProfile = new DaProfile(tag);
            daBolt = new DaBolt(tag);
        }

        public override DaInType daInType()
        {
            return DaInType.ProfileDetail;
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
            daProfile.Write(sw);
            daBolt.Write(sw);

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
            daProfile.Read(sr);
            daBolt.Read(sr);

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
            DiProfileInput form = new DiProfileInput(this);

            return (form.ShowDialog() == DialogResult.OK);
        }
    }
}
