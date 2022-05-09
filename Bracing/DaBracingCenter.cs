using DetailingObjectModel.MainLeg;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Bracing
{
    public class DaBracingCenter : DaInput
    {
        #region Params
        public bool allAreSame { get; set; }
        public bool sameFrontBack { get; set; }
        public bool sameRightLeft { get; set; }
        public List<DaBracingSystem> Containers { get; set; }
        public DaBracingSystem Front { get; set; }
        public DaBracingSystem Back { get; set; }
        public DaBracingSystem Right { get; set; }
        public DaBracingSystem Left { get; set; }
        public DaProfileInput mainProfile { get; set; }
        public DaMainLegContainer mainLegs { get; set; }

        #region I/O

        private const string IOCaption = "<DaBracingCenter>";

        private const string IOTerminate = "</DaBracingCenter>";

        private const int IOVersion = 1;

        #endregion I/O

        #endregion Params

        #region Constructor
        public DaBracingCenter(string tag) : base()
        {
            Tag = tag;

            mainLegs = new DaMainLegContainer(tag);

            Containers = new List<DaBracingSystem>();

            Containers.Add(new DaBracingSystem("Front", mainLegs));
            Containers.Add(new DaBracingSystem("Back", mainLegs));
            Containers.Add(new DaBracingSystem("Right", mainLegs));
            Containers.Add(new DaBracingSystem("Left", mainLegs));

            Front = Containers[0];
            Back = Containers[1];
            Right = Containers[2];
            Left = Containers[3];

            allAreSame = true;
            sameFrontBack = true;
            sameRightLeft = true;

            Refresh();
        }

        #endregion Constructor

        #region Interface

        public override DaInType daInType()
        {
            return DaInType.BracingCenter;
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
            sw.Write("allAreSame = " + allAreSame);
            sw.Write("\n");

            sw.Write("sameFrontBack = " + sameFrontBack);
            sw.Write("\n");

            sw.Write("sameRightLeft = " + sameRightLeft);
            sw.Write("\n");

            mainLegs.Write(sw);

            foreach (var container in Containers)
            {
                container.Write(sw);
            }

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

            Refresh();
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

            line = sr.ReadLine().Replace("allAreSame = ", "");
            allAreSame = Convert.ToBoolean(line);

            line = sr.ReadLine().Replace("sameFrontBack = ", "");
            sameFrontBack = Convert.ToBoolean(line);

            line = sr.ReadLine().Replace("sameRightLeft = ", "");
            sameRightLeft = Convert.ToBoolean(line);

            mainLegs.Read(sr);

            foreach (var container in Containers)
            {
                container.Read(sr);
            }

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
            DiDaBracingCenter form = new DiDaBracingCenter(this);

            if (form.ShowDialog() == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        #endregion Interface

        #region Members

        public void Refresh()
        {
            if (allAreSame)
            {
                SetAll(true);
            }
            else
            {
                SetAll(false);

                if (sameFrontBack)
                {
                    SetBack(true);
                }

                if (sameRightLeft)
                {
                    SetLeft(true);
                }
            }
        }

        private void SetAll(bool allSame)
        {
            if (allSame)
            {
                Front.Caption = "Front-Back-Right-Left";

                Back = Containers[0];
                Right = Containers[0];
                Left = Containers[0];
            }
            else
            {
                Front.Caption = "Front";

                //restore
                Back = Containers[1];
                Right = Containers[2];
                Left = Containers[3];
            }
        }

        private void SetBack(bool v)
        {
            if (sameFrontBack)
            {
                Front.Caption = "Front-Back";

                Back = Containers[0];
            }
            else
            {
                Front.Caption = "Front";

                Back = Containers[1];
            }

        }

        private void SetLeft(bool v)
        {
            if (sameRightLeft)
            {
                Right.Caption = "Right-Left";

                Left = Containers[2];
            }
            else
            {
                Right.Caption = "Right";

                //restore
                Left = Containers[3];
            }
        }

        public void CreateBracingCouples()
        {
            foreach (var container in Containers)
            {
                container.CreateBracingCouples();
            }
        }

        public void CreateConnections()
        {
            foreach (var container in Containers)
            {
                container.CreateConnections();
            }
        }

        #endregion Members
    }
}
