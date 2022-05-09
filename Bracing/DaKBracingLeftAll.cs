﻿using DetailingObjectModel.Connection;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bracing
{
    public class DaKBracingLeftAll : DaKBracing
    {
        #region Create DaBracing class

        public static string ClassIdentifier = "KBracingLeftAll";
        const int IClassIdentifier = 101;

        public static DaBracing CreateDaBracingKBracingLeftAll(DaBracingType daBracingType, int intIdentifier)
        {
            DaBracing daBracing = null;

            if (daBracingType == DaBracingType.KBracing)
            {
                if (intIdentifier == IClassIdentifier)
                {
                    daBracing = new DaKBracingLeftAll();
                }
            }

            return daBracing;
        }

        public static DaBracing CreateDaBracingFromIdentifierKBracingLeftAll(string classIdentifier)
        {
            DaBracing daBracing = null;

            if (classIdentifier == ClassIdentifier)
            {
                daBracing = new DaKBracingLeftAll();

            }

            return daBracing;
        }

        #endregion Create DaBracing class

        #region I/O

        private const string IOCaption = "<DaKBracingLeftAll>";

        private const string IOTerminate = "</DaKBracingLeftAll>";

        private const int IOVersion = 1;

        #endregion I/O

        protected DaKBracingLeftAll() : base()
        {
        }

        public override DaKBracingType kBracingType()
        {
            return DaKBracingType.LeftAll;
        }

        public override bool CreateTextBottom()
        {
            return true;
        }

        public override bool CreateTextTop()
        {
            return true;
        }

        public override string Caption()
        {
            return "KBracingLeftAll";
        }

        public override int IntIdentifier()
        {
            return IClassIdentifier;
        }

        public override List<DaProfileInput> GetProfiles()
        {
            List<DaProfileInput> profiles = new List<DaProfileInput>();

            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);
            profiles.Add(prHorBottom);
            profiles.Add(prHorTop);

            return profiles;
        }

        public override bool HasHorizontalBottom()
        {
            return true;
        }

        public override bool HasHorizontalTop()
        {
            return true;
        }

        public override bool HasDiagonalLeftBottom()
        {
            return false;
        }

        public override bool HasDiagonalLeftTop()
        {
            return false;
        }

        public override bool HasDiagonalRightBottom()
        {
            return true;
        }

        public override bool HasDiagonalRightTop()
        {
            return true;
        }

        public override DaProfileInput GetHorizontalBottom()
        {
            return prHorBottom;
        }

        public override DaProfileInput GetHorizontalTop()
        {
            return prHorTop;
        }

        public override DaProfileInput GetDiagonalLeftBottom()
        {
            return null;
        }

        public override DaProfileInput GetDiagonalLeftTop()
        {
            return null;
        }

        public override DaProfileInput GetDiagonalRightBottom()
        {
            return prDiaBottom;
        }

        public override DaProfileInput GetDiagonalRightTop()
        {
            return prDiaTop;
        }

        public override void CreateConnectionLeft()
        {
            List<DaProfileInput> profiles = new List<DaProfileInput>();
            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);

            connLeft = DaConnection.CreateDaConnectionClass(DaConnectionType.M2D, 0, profiles);
        }

        public override void CreateConnectionRight()
        {
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
