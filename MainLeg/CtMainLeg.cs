using DetailingObjectModel.Bolt;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.MainLeg
{
    public class CtMainLeg : DaControl
    {
        public DaMainLeg daMainLeg { get; set; }
        public StringText ST_Tag { get; set; }
        public DoubleText DT_Bottom { get; set; }
        public DoubleText DT_Top { get; set; }
        public TabPage tabPageMainLeg { get; set; }
        public CtProfileInput ctProfileInput { get; set; }

        public CtMainLeg(DaMainLeg damainprofile) : base()
        {
            daMainLeg = damainprofile;

            canLeaveTabPage = true;
        }

        public override bool Check()
        {
            failedControl = null;

            if (ctProfileInput.Check() == false)
            {
                failedControl = ctProfileInput.failedControl;
                return false;
            }
            else if (ST_Tag.Check() == false)
            {
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = ST_Tag.Control;
                return false;
            }
            else if (DT_Bottom.Check() == false)
            {
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = DT_Bottom.Control;
                return false;
            }
            else if (DT_Top.Check() == false)
            {
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = DT_Top.Control;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            SetTabControl(parent);

            #region main leg tab page

            tabPageMainLeg = new TabPage();

            tabPageMainLeg.Name = "TabPage_MainLeg";
            tabPageMainLeg.Text = "Main Leg";
            tabPageMainLeg.UseVisualStyleBackColor = true;

            tabPageMainLeg.Enter += new EventHandler(TabPageMainLeg_Enter);
            tabPageMainLeg.Leave += new EventHandler(TabPageMainLeg_Leave);

            tabControl.Controls.Add(tabPageMainLeg);

            #endregion main leg tab page

            #region add controls to main leg tabpage

            int tInc = 30;
            int tVal = t;
            int lVal = l;

            TextBox tb;
            Label lb;

            lb = ControlRunTime.CreateLabel("Label_Tag", "Tag", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Tag", "", lVal + 100, tVal, 185, 23);
            ST_Tag = new StringText(tb, false);
            tabPageMainLeg.Controls.Add(tb);
            tabPageMainLeg.Controls.Add(lb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_Bottom", "Bottom", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Bottom", "", lVal + 100, tVal, 185, 23);
            DT_Bottom = new DoubleText(tb, false, false, true);
            tabPageMainLeg.Controls.Add(tb);
            tabPageMainLeg.Controls.Add(lb);

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_Top", "Top", lVal, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Top", "", lVal + 100, tVal, 185, 23);
            DT_Top = new DoubleText(tb, false, false, true);
            tabPageMainLeg.Controls.Add(tb);
            tabPageMainLeg.Controls.Add(lb);

            #endregion add controls to main leg tabpage

            ctProfileInput = new CtProfileInput(daMainLeg.profileCouple);
            ctProfileInput.Create(tabControl, 40, 40);

            ctProfileInput.ctProfile.tabPageProfile.Text = "Profile (" + daMainLeg.profileCouple.Tag + ")";
            ctProfileInput.ctBolt.tabPageBolt.Text = "Bolt (" + daMainLeg.profileCouple.Tag + ")";
        }

        public override DaCtType daCtType()
        {
            return DaCtType.MainLeg;
        }

        public override void Get()
        {
            daMainLeg.Tag = ST_Tag.Get();
            daMainLeg.Bottom = DT_Bottom.Get();
            daMainLeg.Top = DT_Top.Get();

            ctProfileInput.Get();
        }

        public override void Set()
        {
            ST_Tag.Set(daMainLeg.Tag);
            DT_Bottom.Set(daMainLeg.Bottom);
            DT_Top.Set(daMainLeg.Top);

            ctProfileInput.Set();
        }

        public override void Refresh()
        {
            //in case daProfileInput has been changed outside
            ctProfileInput.daProfileInput = daMainLeg.profileCouple;
            ctProfileInput.Refresh();
        }

        private void TabPageMainLeg_Enter(object sender, EventArgs e)
        {
            ST_Tag.Set(daMainLeg.Tag);
            DT_Bottom.Set(daMainLeg.Bottom);
            DT_Top.Set(daMainLeg.Top);
        }

        private void TabPageMainLeg_Leave(object sender, EventArgs e)
        {
            failedControl = null;

            if (ST_Tag.Check() == false)
            {
                canLeaveTabPage = false;
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = ST_Tag.Control;
                ShowFailedControl();
                return;
            }
            else if (DT_Bottom.Check() == false)
            {
                canLeaveTabPage = false;
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = DT_Bottom.Control;
                ShowFailedControl();
                return;
            }
            else if (DT_Top.Check() == false)
            {
                canLeaveTabPage = false;
                tabControl.SelectedTab = tabPageMainLeg;
                failedControl = DT_Top.Control;
                ShowFailedControl();
                return;
            }

            daMainLeg.Tag = ST_Tag.Get();
            daMainLeg.Bottom = DT_Bottom.Get();
            daMainLeg.Top = DT_Top.Get();
        }
    }
}
