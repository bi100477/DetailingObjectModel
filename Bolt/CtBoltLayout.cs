using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;
using DetailingObjectModel.Controls;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltLayout : DaControl
    {
        #region Params
        public DaBoltLayout daBoltLayout { get; set; }
        public SpecialCombo SC_boltName { get; set; }
        public BoltDiameterCombo BDC_boltName { get; set; }
        public IntegerText IT_numBolt { get; set; }

        #endregion Params

        #region Constructor
        public CtBoltLayout(DaBoltLayout daboltlayout) : base()
        {
            daBoltLayout = daboltlayout;
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
            ComboBox cb;

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_numBolt", "numBolt", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_numBolt", "", lVal + 100, tVal, 75, 23);
            IT_numBolt = new IntegerText(tb, false, false, false);
            parent.Controls.Add(tb);
            parent.Controls.Add(lb);


            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_boltName", "boltName", lVal, tVal + 4, 75, 23);
            cb = ControlRunTime.CreateComboBox("Combo_boltName", "", lVal + 100, tVal, 75, 23);
            BDC_boltName = new BoltDiameterCombo(cb);
            SC_boltName = new SpecialCombo(cb);
            parent.Controls.Add(cb);
            parent.Controls.Add(lb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_boltName.Check() == false)
            {
                failedControl = SC_boltName.Control;
                return false;
            }
            else if (IT_numBolt.Check() == false)
            {
                failedControl = IT_numBolt.Control;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            daBoltLayout.boltName = SC_boltName.Get();

            int numBoltOld = daBoltLayout.numBolt;

            daBoltLayout.numBolt = IT_numBolt.Get();

            if (numBoltOld != daBoltLayout.numBolt)
            {
                daBoltLayout.OnNumBoltChanged();
            }
        }

        public override void Set()
        {
            SC_boltName.Set(daBoltLayout.boltName);
            IT_numBolt.Set(daBoltLayout.numBolt);
        }

        #endregion Interface
    }
}
