using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileBoltLocations : DaControl
    {
        public DaProfileBoltLocations boltLocations { get; set; }
        public DoubleText DT_boltLocY { get; set; }
        public DoubleText DT_boltLocZ { get; set; }

        public CtProfileBoltLocations(DaProfileBoltLocations profileboltlocations) : base()
        {
            boltLocations = profileboltlocations;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileBoltLocations;
        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_boltLocY", "boltLocY", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_boltLocY", "", lVal + 100, tVal, 75, 23);
            DT_boltLocY = new DoubleText(tb, false, true, true);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_boltLocZ", "boltLocZ", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_boltLocZ", "", lVal + 100, tVal, 75, 23);
            DT_boltLocZ = new DoubleText(tb, false, true, true);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (DT_boltLocY.Check() == false)
            {
                failedControl = DT_boltLocY.Control;
                failedTabPage = (TabPage)DT_boltLocY.Control.Parent;
                return false;
            }
            else if (DT_boltLocZ.Check() == false)
            {
                failedControl = DT_boltLocZ.Control;
                failedTabPage = (TabPage)DT_boltLocZ.Control.Parent;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            boltLocations.boltLocY = DT_boltLocY.Get();
            boltLocations.boltLocZ = DT_boltLocZ.Get();
        }

        public override void Set()
        {
            DT_boltLocY.Set(boltLocations.boltLocY);
            DT_boltLocZ.Set(boltLocations.boltLocZ);
        }
    }
}
