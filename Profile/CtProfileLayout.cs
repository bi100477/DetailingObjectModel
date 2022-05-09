using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;
using DetailingObjectModel.Controls;

namespace DetailingObjectModel.Profile
{
    public class CtProfileLayout : DaControl
    {
        #region Params
        public DaProfileLayout daProfileLayout { get; set; }
        public SpecialCombo SC_profileName { get; set; }
        public CtProfileCountType ctProfileCountType { get; set; }
        public ProfileNameCombo PNC_profileName { get; set; }

        #endregion Params

        #region Constructor
        public CtProfileLayout(DaProfileLayout daboltlayout) : base()
        {
            daProfileLayout = daboltlayout;
        }

        #endregion Constructor

        #region Interface
        public override DaCtType daCtType()
        {
            return DaCtType.BoltType;
        }

        public override void Create(Control parent, int l, int t)
        {
            Label lb = ControlRunTime.CreateLabel("Label_profileName", "profileName", l, t + 4, 75, 23);
            ComboBox cb = ControlRunTime.CreateComboBox("Combo_profileName", "", l + 100, t, 75, 23);
            PNC_profileName = new ProfileNameCombo(cb);
            SC_profileName = new SpecialCombo(cb);
            parent.Controls.Add(cb);
            parent.Controls.Add(lb);

            ctProfileCountType = new CtProfileCountType();
            ctProfileCountType.Create(parent, 40, t + 40);
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_profileName.Check() == false)
            {
                failedControl = SC_profileName.Control;
                return false;
            }
            else if (ctProfileCountType.Check() == false)
            {
                canLeaveTabPage = false;
                failedControl = ctProfileCountType.failedControl;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            daProfileLayout.profileCount = ctProfileCountType.SC_profileCountType.Get();
            daProfileLayout.profileName = SC_profileName.Get();
        }

        public override void Set()
        {
            ctProfileCountType.SC_profileCountType.Set(daProfileLayout.profileCount);
            SC_profileName.Set(daProfileLayout.profileName);
        }

        #endregion Interface
    }
}
