using DetailingObjectModel.Connection;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bracing
{
    public delegate DaBracing CreateDaBracingClassFunc(DaBracingType daBracingType, int intIdentifier);
    public delegate DaBracing CreateDaBracingClassFromIdentifierFunc(string classIdentifier);

    public abstract class DaBracing : DaInput
    {
        #region Create classes

        #region Create DaBracing class

        public static List<string> daBracingClassIdenitifiers { get; set; }
        public static List<CreateDaBracingClassFunc> createDaBracingFunctions { get; set; }
        public static List<CreateDaBracingClassFromIdentifierFunc> createDaBracingFromIdentifierFunctions { get; set; }

        public static DaBracing CreateDaBracingClass(DaBracingType daBracingType, int intIdentifier)
        {
            DaBracing daBracingClass = null;

            for (int i = 0; i < createDaBracingFunctions.Count; i++)
            {
                daBracingClass = createDaBracingFunctions[i](daBracingType, intIdentifier);

                if (daBracingClass != null)
                {
                    break;
                }
            }

            return daBracingClass;
        }

        public static DaBracing CreateDaBracingClassFromIdentifier(string classIdentifier)
        {
            DaBracing daBracingClass = null;

            for (int i = 0; i < createDaBracingFromIdentifierFunctions.Count; i++)
            {
                daBracingClass = createDaBracingFromIdentifierFunctions[i](classIdentifier);

                if (daBracingClass != null)
                {
                    break;
                }
            }

            return daBracingClass;
        }
        #endregion Create DaBracing class

        static DaBracing()
        {
            daBracingClassIdenitifiers = new List<string>();
            daBracingClassIdenitifiers.Add(DaKBracingLeft.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingLeftTop.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingLeftBottom.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingLeftAll.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingRight.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingRightTop.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingRightBottom.ClassIdentifier);
            daBracingClassIdenitifiers.Add(DaKBracingRightAll.ClassIdentifier);

            createDaBracingFunctions = new List<CreateDaBracingClassFunc>();

            createDaBracingFunctions.Add(DaKBracingLeft.CreateDaBracingKBracingLeft);
            createDaBracingFunctions.Add(DaKBracingLeftTop.CreateDaBracingKBracingLeftTop);
            createDaBracingFunctions.Add(DaKBracingLeftBottom.CreateDaBracingKBracingLeftBottom);
            createDaBracingFunctions.Add(DaKBracingLeftAll.CreateDaBracingKBracingLeftAll);
            createDaBracingFunctions.Add(DaKBracingRight.CreateDaBracingKBracingRight);
            createDaBracingFunctions.Add(DaKBracingRightTop.CreateDaBracingKBracingRightTop);
            createDaBracingFunctions.Add(DaKBracingRightBottom.CreateDaBracingKBracingRightBottom);
            createDaBracingFunctions.Add(DaKBracingRightAll.CreateDaBracingKBracingRightAll);

            createDaBracingFromIdentifierFunctions = new List<CreateDaBracingClassFromIdentifierFunc>();

            createDaBracingFromIdentifierFunctions.Add(DaKBracingLeft.CreateDaBracingFromIdentifierKBracingLeft);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingLeftTop.CreateDaBracingFromIdentifierKBracingLeftTop);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingLeftBottom.CreateDaBracingFromIdentifierKBracingLeftBottom);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingLeftAll.CreateDaBracingFromIdentifierKBracingLeftAll);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingRight.CreateDaBracingFromIdentifierKBracingRight);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingRightTop.CreateDaBracingFromIdentifierKBracingRightTop);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingRightBottom.CreateDaBracingFromIdentifierKBracingRightBottom);
            createDaBracingFromIdentifierFunctions.Add(DaKBracingRightAll.CreateDaBracingFromIdentifierKBracingRightAll);
        }

        #endregion Create classes

        #region I/O

        private const string IOCaption = "<DaBracing>";

        private const string IOTerminate = "</DaBracing>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaConnection connLeft { get; set; }
        public DaConnection connRight { get; set; }

        protected DaBracing() : base()
        {
            connLeft = null;
            connRight = null;
        }

        #region Interface DaBracing
        public abstract DaBracingType daBracingType();

        public abstract string Caption();

        public abstract int IntIdentifier();

        public abstract List<DaProfileInput> GetProfiles();

        public abstract bool HasHorizontalBottom();

        public abstract bool HasHorizontalTop();

        public abstract bool HasDiagonalLeftBottom();

        public abstract bool HasDiagonalLeftTop();

        public abstract bool HasDiagonalRightBottom();

        public abstract bool HasDiagonalRightTop();

        public abstract DaProfileInput GetHorizontalBottom();

        public abstract DaProfileInput GetHorizontalTop();

        public abstract DaProfileInput GetDiagonalLeftBottom();

        public abstract DaProfileInput GetDiagonalLeftTop();

        public abstract DaProfileInput GetDiagonalRightBottom();

        public abstract DaProfileInput GetDiagonalRightTop();

        public abstract double BottomLevel();

        public abstract double TopLevel();

        public abstract double MidLevel();

        public abstract void CreateConnectionLeft();

        public abstract void CreateConnectionRight();

        #endregion Interface DaBracing

        public override DaInType daInType()
        {
            return DaInType.Bracing;
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

        #region I/O connections

        #region write
        public void WriteConnections(StreamWriter sw)
        {
            if (connLeft != null)
            {
                connLeft.Write(sw);
            }

            if (connRight != null)
            {
                connRight.Write(sw);
            }
        }

        #endregion write

        #region read
        public void ReadConnections(StreamReader sr)
        {
            CreateConnectionLeft();

            if (connLeft != null)
            {
                connLeft.Read(sr);
            }

            CreateConnectionRight();

            if (connRight != null)
            {
                connRight.Read(sr);
            }
        }

        #endregion read

        #endregion I/O connections

    }
}
