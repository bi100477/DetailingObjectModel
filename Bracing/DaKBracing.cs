using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DetailingObjectModel.Bracing
{
    public abstract class DaKBracing : DaBracing
    {
        public double Bottom { get; set; }
        public double Top { get; set; }
        public double Mid { get; set; }
        public DaProfileInput prDiaBottom { get; set; }
        public DaProfileInput prDiaTop { get; set; }
        public DaProfileInput prHorBottom { get; set; }
        public DaProfileInput prHorTop { get; set; }
        

        #region I/O

        private const string IOCaption = "<DaKBracing>";

        private const string IOTerminate = "</DaKBracing>";

        private const int IOVersion = 1;

        #endregion I/O

        protected DaKBracing() : base()
        {
            Bottom = 500.0;
            Top = 2000.0;
            Mid = 1500.0;

            prDiaBottom = new DaProfileInput("DiaBottom");
            prDiaTop = new DaProfileInput("DiaTop");

            prHorBottom = new DaProfileInput("HorBottom");
            prHorTop = new DaProfileInput("HorTop");
        }

        public abstract DaKBracingType kBracingType();

        public abstract bool CreateTextBottom();

        public abstract bool CreateTextTop();

        public override DaBracingType daBracingType()
        {
            return DaBracingType.KBracing;
        }

        public override bool SetDataFromDialog()
        {
            DiKBracing form = new DiKBracing(this);

            return (form.ShowDialog() == DialogResult.OK);
        }

        public override double BottomLevel()
        {
            return Bottom;
        }

        public override double TopLevel()
        {
            return Top;
        }

        public override double MidLevel()
        {
            return Mid;
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

            base.Write(sw);
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
            sw.Write("Bottom = " + Bottom);
            sw.Write("\n");

            sw.Write("Top = " + Top);
            sw.Write("\n");

            sw.Write("Mid = " + Mid);
            sw.Write("\n");

            prDiaBottom.Write(sw);
            prDiaTop.Write(sw);

            prHorBottom.Write(sw);
            prHorTop.Write(sw);

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

            base.Read(sr);
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

            line = sr.ReadLine().Replace("Bottom = ", "");
            Bottom = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("Top = ", "");
            Top = Convert.ToDouble(line);

            line = sr.ReadLine().Replace("Mid = ", "");
            Mid = Convert.ToDouble(line);

            prDiaBottom.Read(sr);
            prDiaTop.Read(sr);

            prHorBottom.Read(sr);
            prHorTop.Read(sr);

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