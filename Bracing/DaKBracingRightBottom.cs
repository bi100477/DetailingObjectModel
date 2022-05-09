﻿using DetailingObjectModel.Connection;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;

namespace DetailingObjectModel.Bracing
{
    public class DaKBracingRightBottom : DaKBracing
    {
        #region Create DaBracing class

        public static string ClassIdentifier = "KBracingRightBottom";
        const int IClassIdentifier = 106;

        public static DaBracing CreateDaBracingKBracingRightBottom(DaBracingType daBracingType, int intIdentifier)
        {
            DaBracing daBracing = null;

            if (daBracingType == DaBracingType.KBracing)
            {
                if (intIdentifier == IClassIdentifier)
                {
                    daBracing = new DaKBracingRightBottom();
                }
            }

            return daBracing;
        }

        public static DaBracing CreateDaBracingFromIdentifierKBracingRightBottom(string classIdentifier)
        {
            DaBracing daBracing = null;

            if (classIdentifier == ClassIdentifier)
            {
                daBracing = new DaKBracingRightBottom();

            }

            return daBracing;
        }

        #endregion Create DaBracing class

        #region I/O

        private const string IOCaption = "<DaKBracingRightBottom>";

        private const string IOTerminate = "</DaKBracingRightBottom>";

        private const int IOVersion = 1;

        #endregion I/O

        protected DaKBracingRightBottom() : base()
        {
        }

        public override DaKBracingType kBracingType()
        {
            return DaKBracingType.RightBottom;
        }

        public override bool CreateTextBottom()
        {
            return true;
        }

        public override bool CreateTextTop()
        {
            return true;
        }

        public override int IntIdentifier()
        {
            return IClassIdentifier;
        }

        public override string Caption()
        {
            return "KBracingRightBottom";
        }

        public override List<DaProfileInput> GetProfiles()
        {
            List<DaProfileInput> profiles = new List<DaProfileInput>();

            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);
            profiles.Add(prHorBottom);

            return profiles;
        }

        public override bool HasHorizontalBottom()
        {
            return true;
        }

        public override bool HasHorizontalTop()
        {
            return false;
        }

        public override bool HasDiagonalLeftBottom()
        {
            return true;
        }

        public override bool HasDiagonalLeftTop()
        {
            return true;
        }

        public override bool HasDiagonalRightBottom()
        {
            return false;
        }

        public override bool HasDiagonalRightTop()
        {
            return false;
        }

        public override DaProfileInput GetHorizontalBottom()
        {
            return prHorBottom;
        }

        public override DaProfileInput GetHorizontalTop()
        {
            return null;
        }

        public override DaProfileInput GetDiagonalLeftBottom()
        {
            return prDiaBottom;
        }

        public override DaProfileInput GetDiagonalLeftTop()
        {
            return prDiaTop;
        }

        public override DaProfileInput GetDiagonalRightBottom()
        {
            return null;
        }

        public override DaProfileInput GetDiagonalRightTop()
        {
            return null;
        }

        public override void CreateConnectionLeft()
        {
        }

        public override void CreateConnectionRight()
        {
            List<DaProfileInput> profiles = new List<DaProfileInput>();
            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);

            connRight = DaConnection.CreateDaConnectionClass(DaConnectionType.M2D, 1, profiles);
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
