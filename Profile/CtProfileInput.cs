using DetailingObjectModel.Bolt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Profile
{   

    public class CtProfileInput : DaControl
    {
        public DaProfileInput daProfileInput { get; set; }
        public CtProfile ctProfile { get; set; }
        public CtBolt ctBolt { get; set; }
        public TabPage tabPageLayout { get; set; }

        public CtProfileInput(DaProfileInput daprofileinput) : base()
        {
            daProfileInput = daprofileinput;

            canLeaveTabPage = true;
        }

        public override bool Check()
        {
            failedControl = null;

            if (ctProfile.ctProfileLayout.Check() == false)
            {
                failedControl = ctProfile.ctProfileLayout.failedControl;
                tabControl.SelectedTab = tabPageLayout;
                return false;
            }
            else if (ctBolt.ctBoltLayout.Check() == false)
            {
                failedControl = ctBolt.ctBoltLayout.failedControl;
                tabControl.SelectedTab = tabPageLayout;
                return false;
            }
            else if (ctProfile.Check() == false)
            {
                tabControl.SelectedTab = ctProfile.failedTabPage;                
                failedControl = ctProfile.failedControl;
                return false;
            }
            else if (ctBolt.Check() == false)
            {
                tabControl.SelectedTab = ctProfile.failedTabPage;
                failedControl = ctBolt.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            SetTabControl(parent);

            #region layout tab page

            tabPageLayout = new TabPage();

            tabPageLayout.Name = "TabPage_Layout";
            tabPageLayout.Text = "Layout";
            tabPageLayout.UseVisualStyleBackColor = true;

            tabPageLayout.Enter += new EventHandler(TabPageLayout_Enter);
            tabPageLayout.Leave += new EventHandler(TabPageLayout_Leave);

            tabControl.Controls.Add(tabPageLayout);

            #endregion layout tab page

            ctProfile = new CtProfile(daProfileInput.daProfile);
            ctProfile.tabPageLayout = tabPageLayout;
            ctProfile.Create(tabControl, 40, 40);

            ctBolt = new CtBolt(daProfileInput.daBolt);
            ctBolt.tabPageLayout = tabPageLayout;
            ctBolt.Create(tabControl, 40, 40);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileInput;
        }

        public override void Get()
        {
            ctProfile.Get();
            ctBolt.Get();
        }

        public override void Set()
        {
            ctProfile.Set();
            ctBolt.Set();
        }

        public override void Refresh()
        {
            //in case daProfileInput has been changed outside
            ctProfile.daProfile = daProfileInput.daProfile;
            ctProfile.Refresh();

            ctBolt.daBolt = daProfileInput.daBolt;
            ctBolt.Refresh();
        }

        private void TabPageLayout_Enter(object sender, EventArgs e)
        {
            ctProfile.ctProfileLayout.Set();
            ctBolt.ctBoltLayout.Set();
        }

        private void TabPageLayout_Leave(object sender, EventArgs e)
        {
            failedControl = null;

            if (ctProfile.ctProfileLayout.Check() == true)
            {
                ctProfile.ctProfileLayout.Get();
            }
            else
            {
                canLeaveTabPage = false;
                failedControl = ctProfile.ctProfileLayout.failedControl;
                ShowFailedControl();

                return;
            }
            
            if(ctBolt.ctBoltLayout.Check() == true)
            {
                ctBolt.ctBoltLayout.Get();
            }
            else
            {
                canLeaveTabPage = false;
                failedControl = ctBolt.ctBoltLayout.failedControl;
                ShowFailedControl();

                return;
            }
        }
    }
}
