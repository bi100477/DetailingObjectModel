using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileEndOffsets : DaControl
    {
        public DaProfileEndOffsets endOffsets { get; set; }
        public DoubleText DT_offStart { get; set; }
        public DoubleText DT_offEnd { get; set; }

        public CtProfileEndOffsets(DaProfileEndOffsets profileendoffsets) : base()
        {
            endOffsets = profileendoffsets;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileEndOffsets;
        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_offStart", "offStart", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_offStart", "", lVal + 100, tVal, 75, 23);
            DT_offStart = new DoubleText(tb, false, true, true);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_offEnd", "offEnd", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_offEnd", "", lVal + 100, tVal, 75, 23);
            DT_offEnd = new DoubleText(tb, false, true, true);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (DT_offStart.Check() == false)
            {
                failedControl = DT_offStart.Control;
                failedTabPage = (TabPage)DT_offStart.Control.Parent;
                return false;
            }
            else if (DT_offEnd.Check() == false)
            {
                failedControl = DT_offEnd.Control;
                failedTabPage = (TabPage)DT_offEnd.Control.Parent;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            endOffsets.offStart = DT_offStart.Get();
            endOffsets.offEnd = DT_offEnd.Get();
        }

        public override void Set()
        {
            DT_offStart.Set(endOffsets.offStart);
            DT_offEnd.Set(endOffsets.offEnd);
        }
    }
}
