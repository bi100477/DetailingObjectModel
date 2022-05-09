using DetailingObjectModel.Connection;
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
    public class DaMainLegContainer : DaInput
    {
        public List<DaMainLeg> mainLegs { get; set; }
        public List<DaConnection> Connections { get; set; }

        #region I/O

        private const string IOCaption = "<DaMainLegContainer>";

        private const string IOTerminate = "</DaMainLegContainer>";

        private const int IOVersion = 1;

        #endregion I/O

        public DaMainLegContainer(string tag) : base()
        {
            Tag = tag;
            mainLegs = new List<DaMainLeg>();
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
            base.Write(sw);

            switch (ver)
            {
                case 1: WriteVer01(sw); break;
            }
        }

        private void WriteVer01(StreamWriter sw)
        {
            sw.Write("numMainLeg = " + mainLegs.Count);
            sw.Write("\n");

            if (mainLegs.Count > 0)
            {
                foreach (var mainLeg in mainLegs)
                {
                    mainLeg.Write(sw);
                }
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
            string line;

            line = sr.ReadLine().Replace("numMainLeg = ", "");
            int numMainLeg = Convert.ToInt32(line);

            if (numMainLeg > 0)
            {
                for (int i = 0; i < numMainLeg; i++)
                {
                    mainLegs.Add(new DaMainLeg());

                    mainLegs.Last().Read(sr);
                }
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
            DiDaMainLegContainer form = new DiDaMainLegContainer(this);

            return (form.ShowDialog() == DialogResult.OK);
        }

        public override bool SetConnectionsFromDialog()
        {
            //DiCoBracingSystem diBracingSystem = new DiCoBracingSystem(this);

            //if (diBracingSystem.ShowDialog() == DialogResult.OK)
            //{
            //    return true;
            //}

            return false;
        }

        internal void CreateConnections()
        {
            //connectionsLeft.Clear();
            //connectionsRight.Clear();

            //CreateConnectionsOnCouples();
            //CreateConnectionsOnBracings();
        }

        internal DaProfileInput ProfileAtLevel(double level)
        {
            DaMainLeg mainLeg = mainLegs.Find(x => x.Bottom < level && level < x.Top);

            if (mainLeg != null)
            {
                return mainLeg.profileCouple;
            }

            return null;
        }
    }
}
