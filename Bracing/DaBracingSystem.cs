using DetailingObjectModel.Connection;
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
    public class DaBracingSystem : DaInput
    {
        public string Caption { get; set; }
        public List<DaBracing> Bracings { get; set; }
        public List<DaBracingCouple> Couples { get; set; }
        public DaMainLegContainer mainLegs { get; set; }

        #region I/O

        private const string IOCaption = "<DaBracingSystem>";

        private const string IOTerminate = "</DaBracingSystem>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaBracingSystem(string caption, DaMainLegContainer mainlegs) : base()
        {
            Caption = caption;
            Bracings = new List<DaBracing>();
            Couples = new List<DaBracingCouple>();

            mainLegs = mainlegs;
        }

        public override DaInType daInType()
        {
            return DaInType.BracingSystem;
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
            sw.Write("numBracing = " + Bracings.Count);
            sw.Write("\n");

            if (Bracings.Count > 0)
            {
                foreach (var item in Bracings)
                {
                    sw.Write("bracingType = " + item.daBracingType());
                    sw.Write("\n");

                    sw.Write("intIdentifier = " + item.IntIdentifier());
                    sw.Write("\n");

                    item.Write(sw);
                }
            }

            sw.Write("numBracingCouple = " + Couples.Count);
            sw.Write("\n");

            WriteConnections(sw);

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

            line = sr.ReadLine().Replace("numBracing = ", "");
            int numBracing = Convert.ToInt32(line);

            if (numBracing > 0)
            {
                for (int i = 0; i < numBracing; i++)
                {
                    line = sr.ReadLine().Replace("bracingType = ", "");
                    DaBracingType type = Enum.Parse<DaBracingType>(line);

                    line = sr.ReadLine().Replace("intIdentifier = ", "");
                    int identifier = Convert.ToInt32(line);

                    Bracings.Add(DaBracing.CreateDaBracingClass(type, identifier));
                    Bracings.Last().Read(sr);
                }
            }

            line = sr.ReadLine().Replace("numBracingCouple = ", "");
            int numBracingCouple = Convert.ToInt32(line);

            if (numBracingCouple > 0)
            {
                CreateBracingCouples();
            }

            ReadConnections(sr);

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
            DiDaBracingSystem diBracingSystem = new DiDaBracingSystem(this);

            if (diBracingSystem.ShowDialog() == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        public override bool SetConnectionsFromDialog()
        {
            DiCoBracingSystem diBracingSystem = new DiCoBracingSystem(this);

            if (diBracingSystem.ShowDialog() == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        internal void CreateBracingCouples()
        {
            if (Bracings.Count == 0)
            {
                return;
            }

            Couples.Clear();

            for (int i = 0; i < Bracings.Count + 1; i++)
            {
                Couples.Add(new DaBracingCouple(null, null));
            }

            for (int i = 0; i < Bracings.Count; i++)
            {
                Couples[i + 1].brBelow = Bracings[i];
                Couples[i + 1].Tag = "BC-" + Bracings[i].TopLevel();
                Couples[i + 1].Level = Bracings[i].TopLevel();
            }

            for (int i = 0; i < Bracings.Count; i++)
            {
                Couples[i].brAbove = Bracings[i];
                Couples[i].Tag = "BC-" + Bracings[i].BottomLevel();
                Couples[i].Level = Bracings[i].BottomLevel();
            }
        }

        internal void CreateConnections()
        {
            CreateConnectionsOnCouples();
            CreateConnectionsOnBracings();
        }

        private void CreateConnectionsOnCouples()
        {
            foreach (var couple in Couples)
            {
                DaConnection conLeft = DaConnection.CreateDaConnectionClassLeft(couple);
                DaConnection conRight = DaConnection.CreateDaConnectionClassRight(couple);

                (DaProfileInput, DaProfileInput) mainProfiles = DaConnection.SetMainProfiles(couple, mainLegs);

                if (conLeft != null)
                {
                    conLeft.mainProfileBelow = mainProfiles.Item1;
                    conLeft.mainProfileAbove = mainProfiles.Item2;

                    couple.connLeft = conLeft;
                }

                if (conRight != null)
                {
                    conRight.mainProfileBelow = mainProfiles.Item1;
                    conRight.mainProfileAbove = mainProfiles.Item2;

                    couple.connRight = conRight;
                }
            }
        }

        private void CreateConnectionsOnBracings()
        {
            foreach (var bracing in Bracings)
            {
                bracing.CreateConnectionLeft();
                bracing.CreateConnectionRight();

                (DaProfileInput, DaProfileInput) mainProfiles = DaConnection.SetMainProfiles(bracing, mainLegs);

                if (bracing.connLeft != null)
                {
                    bracing.connLeft.mainProfileBelow = mainProfiles.Item1;
                    bracing.connLeft.mainProfileAbove = mainProfiles.Item2;
                }

                if (bracing.connRight != null)
                {
                    bracing.connRight.mainProfileBelow = mainProfiles.Item1;
                    bracing.connRight.mainProfileAbove = mainProfiles.Item2;
                }
            }
        }

        #region I/O connections

        #region write
        private void WriteConnections(StreamWriter sw)
        {
            WriteConnectionsOnCouples(sw);
            WriteConnectionsOnBracings(sw);
        }

        private void WriteConnectionsOnCouples(StreamWriter sw)
        {
            foreach (var couple in Couples)
            {
                couple.WriteConnections(sw);
            }
        }

        private void WriteConnectionsOnBracings(StreamWriter sw)
        {
            foreach (DaBracing bracing in Bracings)
            {
                bracing.WriteConnections(sw);
            }
        }

        #endregion write

        #region read

        internal void ReadConnections(StreamReader sr)
        {
            ReadConnectionsOnCouples(sr);
            ReadConnectionsOnBracings(sr);
        }

        private void ReadConnectionsOnCouples(StreamReader sr)
        {
            foreach (var couple in Couples)
            {
                couple.ReadConnections(sr);

                (DaProfileInput, DaProfileInput) mainProfiles = DaConnection.SetMainProfiles(couple, mainLegs);

                if (couple.connLeft != null)
                {
                    couple.connLeft.mainProfileBelow = mainProfiles.Item1;
                    couple.connLeft.mainProfileAbove = mainProfiles.Item2;
                }

                if (couple.connRight != null)
                {
                    couple.connRight.mainProfileBelow = mainProfiles.Item1;
                    couple.connRight.mainProfileAbove = mainProfiles.Item2;
                }
            }
        }

        private void ReadConnectionsOnBracings(StreamReader sr)
        {
            foreach (var bracing in Bracings)
            {
                bracing.ReadConnections(sr);

                (DaProfileInput, DaProfileInput) mainProfiles = DaConnection.SetMainProfiles(bracing, mainLegs);

                if (bracing.connLeft != null)
                {
                    bracing.connLeft.mainProfileBelow = mainProfiles.Item1;
                    bracing.connLeft.mainProfileAbove = mainProfiles.Item2;
                }

                if (bracing.connRight != null)
                {
                    bracing.connRight.mainProfileBelow = mainProfiles.Item1;
                    bracing.connRight.mainProfileAbove = mainProfiles.Item2;
                }
            }
        }

        #endregion read

        #endregion I/O connections
    }
}
