using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesUI;
using DetailingObjectModel.Controls;
using System.Windows.Forms;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltType : DaControl
    {
        #region Params
        public DaBoltType daBoltType { get; set; }
        public StringText ST_boltType { get; set; }
        public StringText ST_boltGrade { get; set; }
        public StringText ST_boltAssembly { get; set; }

        #endregion Params

        #region Constructor
        public CtBoltType(DaBoltType dabolttype) : base()
        {
            daBoltType = dabolttype;
        }

        #endregion Constructor

        #region Interface
        public override DaCtType daCtType()
        {
            return DaCtType.BoltType;
        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_boltType", "boltType", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_boltType", "", lVal + 100, tVal, 75, 23);
            ST_boltType = new StringText(tb, false);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_boltGrade", "boltGrade", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_boltGrade", "", lVal + 100, tVal, 75, 23);
            ST_boltGrade = new StringText(tb, false);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_boltAssembly", "boltAssembly", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_boltAssembly", "", lVal + 100, tVal, 75, 23);
            ST_boltAssembly = new StringText(tb, false);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (ST_boltType.Check() == false)
            {
                failedControl = ST_boltType.Control;
                return false;
            }
            else if (ST_boltGrade.Check() == false)
            {
                failedControl = ST_boltGrade.Control;
                return false;
            }
            else if (ST_boltAssembly.Check() == false)
            {
                failedControl = ST_boltAssembly.Control;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            daBoltType.boltType = ST_boltType.Get();
            daBoltType.boltGrade = ST_boltGrade.Get();
            daBoltType.boltAssembly = ST_boltAssembly.Get();
        }

        public override void Set()
        {
            ST_boltType.Set(daBoltType.boltType);
            ST_boltGrade.Set(daBoltType.boltGrade);
            ST_boltAssembly.Set(daBoltType.boltAssembly);
        }

        #endregion Interface
    }

}
