using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Profile
{
    public delegate void FuncOnNumProfileChanged();

    public class DaProfileLayout : DaInput
    {
        public EProfileCount profileCount { get; set; }
        public string profileName { get; set; }

        #region I/O

        private const string IOCaption = "<DaProfileLayout>";

        private const string IOTerminate = "</DaProfileLayout>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaProfileLayout() : base()
        {
            profileCount = EProfileCount.One;
            profileName = "L80x8";
        }

        public override DaInType daInType()
        {
            return DaInType.ProfileLayout;
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
            sw.Write("profileCount = " + profileCount);
            sw.Write("\n");

            sw.Write("profileName = " + profileName);
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

            line = sr.ReadLine().Replace("profileCount = ", "");
            profileCount = Enum.Parse<EProfileCount>(line);

            line = sr.ReadLine().Replace("profileName = ", "");
            profileName = line;

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

        private void OnNumProfileChangedDefault()
        {

        }

    }
}
