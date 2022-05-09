using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.MainLeg
{
    public class DaMainLeg : DaInput
    {
        public double Bottom { get; set; }
        public double Top { get; set; }
        public DaProfileInput profileCouple { get; set; }

        #region I/O

        private const string IOCaption = "<DaMainLeg>";

        private const string IOTerminate = "</DaMainLeg>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaMainLeg()
        {
            Tag = "A main profile";
            Bottom = 0.0;
            Top = 2500.0;
            profileCouple = new DaProfileInput("Main profile");
        }


        public override DaInType daInType()
        {
            return DaInType.MainLeg;
        }

        public override bool SetDataFromDialog()
        {
            DiMainLeg form = new DiMainLeg(this);

            return (form.ShowDialog() == DialogResult.OK);
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
            sw.Write("Bottom = " + Bottom);
            sw.Write("\n");

            sw.Write("Top = " + Top);
            sw.Write("\n");

            profileCouple.Write(sw);

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
            string line = sr.ReadLine().Replace("Bottom = ", "");
            Bottom = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("Top = ", "");
            Top = Convert.ToDouble(line);

            profileCouple.Read(sr);

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
