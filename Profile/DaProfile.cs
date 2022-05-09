using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Profile
{
    public class DaProfile : DaInput
    {
        public DaProfileLayout profileLayout { get; set; }
        public DaProfileType profileType { get; set; }
        public DaProfileOrientation orientationFirst { get; set; }
        public DaProfileOrientation orientationSecond { get; set; }
        public DaProfileEndOffsets profileEndOffsets { get; set; }
        public DaProfileBoltLocations profileBoltLocations { get; set; }
        public DaProfileEndConnection connectionStart { get; set; }
        public DaProfileEndConnection connectionEnd { get; set; }

        #region I/O

        private const string IOCaption = "<DaProfile>";

        private const string IOTerminate = "</DaProfile>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaProfile(string tag) : base()
        {
            Tag = tag;

            profileLayout = new DaProfileLayout();
            profileType = new DaProfileType();
            orientationFirst = new DaProfileOrientation("First");
            orientationSecond = new DaProfileOrientation("Second");
            profileEndOffsets = new DaProfileEndOffsets();
            profileBoltLocations = new DaProfileBoltLocations();
        }

        public override DaInType daInType()
        {
            return DaInType.Profile;
        }

        internal List<DaProfileOrientation> GetProfileOrientations()
        {
            List<DaProfileOrientation> orientations = new List<DaProfileOrientation>();

            switch (profileLayout.profileCount)
            {
                case EProfileCount.One:
                    {
                        orientations.Add(orientationFirst);
                    }
                    break;
                case EProfileCount.Two:
                    {
                        orientations.Add(orientationFirst);
                        orientations.Add(orientationSecond);
                    }
                    break;
                default:
                    break;
            }

            return orientations;
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
            profileLayout.Write(sw);
            profileType.Write(sw);
            orientationFirst.Write(sw);
            orientationSecond.Write(sw);
            profileEndOffsets.Write(sw);
            profileBoltLocations.Write(sw);

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
            profileLayout.Read(sr);
            profileType.Read(sr);
            orientationFirst.Read(sr);
            orientationSecond.Read(sr);
            profileEndOffsets.Read(sr);
            profileBoltLocations.Read(sr);

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
