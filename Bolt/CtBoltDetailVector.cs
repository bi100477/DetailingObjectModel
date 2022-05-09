using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltDetailVector : CtBoltDetail
    {
        #region Create CtBoltDetail class

        public static CtBoltDetail CreateCtBoltDetailVector(DaBoltDetail daBoltDetail)
        {
            CtBoltDetail ctBoltDetail = null;

            if (daBoltDetail.boltDetailType() == EBoltDetailType.Vector)
            {
                ctBoltDetail = new CtBoltDetailVector(daBoltDetail);
            }

            return ctBoltDetail;
        }

        #endregion Create DaBoltDetail class

        public DoubleText DT_dS { get; set; }
        public DoubleText DT_dE { get; set; }

        public CtBoltDetailVector(DaBoltDetail daboltdetail) : base(daboltdetail)
        {

        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_dS", "dS", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_dS", "", lVal + 100, tVal, 75, 23);
            DT_dS = new DoubleText(tb, false, true, true);
            parent.Controls.Add(tb);
            parent.Controls.Add(lb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_dE", "dE", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_dE", "", lVal + 100, tVal, 75, 23);
            DT_dE = new DoubleText(tb, false, false, false);
            parent.Controls.Add(tb);
            parent.Controls.Add(lb);
        }

        public override bool Check()
        {
            if (DT_dS.Check() == false)
            {
                failedControl = DT_dS.Control;
                return false;
            }
            else if (DT_dE.Check() == false)
            {
                failedControl = DT_dE.Control;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            DaBoltDetailVector daBoltDetailVector = (DaBoltDetailVector)daBoltDetail;

            if (daBoltDetailVector == null)
            {
                throw new Exception("daBoltDetailVector == null");
            }

            daBoltDetailVector.dS = DT_dS.Get();
            daBoltDetailVector.dE = DT_dE.Get();
        }

        public override void Set()
        {
            DaBoltDetailVector daBoltDetailVector = (DaBoltDetailVector)daBoltDetail;

            if (daBoltDetailVector == null)
            {
                throw new Exception("daBoltDetailVector == null");
            }

            DT_dS.Set(daBoltDetailVector.dS);
            DT_dE.Set(daBoltDetailVector.dE);
        }

    }
}
