using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltOffset : DaControl
    {
        public DaBoltOffset boltOffset { get; set; }
        public DoubleText DT_Offset { get; set; }

        public CtBoltOffset(DaBoltOffset boltoffset) : base()
        {
            boltOffset = boltoffset;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.BoltOffset;
        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_Offset", "Offset", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Offset", "", lVal + 100, tVal, 75, 23);
            DT_Offset = new DoubleText(tb, false, true, true);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (DT_Offset.Check() == false)
            {
                failedControl = DT_Offset.Control;
                failedTabPage = (TabPage)DT_Offset.Control.Parent;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            boltOffset.Offset = DT_Offset.Get();
        }

        public override void Set()
        {
            DT_Offset.Set(boltOffset.Offset);
        }
    }
}
