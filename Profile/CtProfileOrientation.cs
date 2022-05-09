using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileOrientation : DaControl
    {
        public DaProfileOrientation daProfileOrientation { get; set; }
        private CtProfileOrientationType ctProfileOrientationType { get; set; }
        private DoubleText DT_offInPlane { get; set; }
        private DoubleText DT_offOutPlane { get; set; }

        public CtProfileOrientation(DaProfileOrientation daprofileorientation) : base()
        {
            daProfileOrientation = daprofileorientation;
        }

        public override bool Check()
        {
            failedControl = null;

            if (DT_offInPlane.Check() == false)
            {
                failedControl = DT_offInPlane.Control;
                return false;
            }
            else if (DT_offOutPlane.Check() == false)
            {
                failedControl = DT_offOutPlane.Control;
                return false;
            }
            else if (ctProfileOrientationType.Check() == false)
            {
                failedControl = ctProfileOrientationType.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            int tInc = 30;
            int tVal = t + 190;
            int lVal = l + 160;

            TextBox tb;
            Label lb;

            lb = ControlRunTime.CreateLabel("Label_offInPlane", "offInPlane", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_offInPlane", "", lVal + 100, tVal, 75, 23);
            DT_offInPlane = new DoubleText(tb, false, true, true);
            parent.Controls.Add(tb);
            parent.Controls.Add(lb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_offOutPlane", "offOutPlane", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_offOutPlane", "", lVal + 100, tVal, 75, 23);
            DT_offOutPlane = new DoubleText(tb, false, true, true);
            parent.Controls.Add(tb);
            parent.Controls.Add(lb);

            ctProfileOrientationType = new CtProfileOrientationType();
            ctProfileOrientationType.Create(parent, l + 160, t + 10);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileOrientation;
        }

        public override void Get()
        {
            daProfileOrientation.profileOrientation = ctProfileOrientationType.SC_profileOrientationType.Get();

            daProfileOrientation.inPlaneOffset = DT_offInPlane.Get();
            daProfileOrientation.outPlaneOffset = DT_offOutPlane.Get();
        }

        public override void Set()
        {
            ctProfileOrientationType.SC_profileOrientationType.Set(daProfileOrientation.profileOrientation);

            DT_offInPlane.Set(daProfileOrientation.inPlaneOffset);
            DT_offOutPlane.Set(daProfileOrientation.outPlaneOffset);
        }
    }
}
