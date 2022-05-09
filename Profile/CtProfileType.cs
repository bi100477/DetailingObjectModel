using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileType : DaControl
    {
        #region Params
        public DaProfileType daProfileType { get; set; }
        public StringText ST_className { get; set; }

        #endregion Params

        #region Constructor
        public CtProfileType(DaProfileType dabolttype) : base()
        {
            daProfileType = dabolttype;
        }

        #endregion Constructor

        #region Interface
        public override DaCtType daCtType()
        {
            return DaCtType.ProfileType;
        }

        public override void Create(Control parent, int l, int t)
        {
            TextBox tb;
            Label lb;

            int tVal = t;
            int lVal = l;

            lb = ControlRunTime.CreateLabel("Label_className", "className", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_className", "", lVal + 100, tVal, 185, 23);
            ST_className = new StringText(tb, false);
            parent.Controls.Add(lb);
            parent.Controls.Add(tb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (ST_className.Check() == false)
            {
                failedControl = ST_className.Control;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            daProfileType.className = ST_className.Get();
        }

        public override void Set()
        {
            ST_className.Set(daProfileType.className);
        }

        #endregion Interface
    }
}
