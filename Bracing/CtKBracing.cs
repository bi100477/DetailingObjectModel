using DetailingObjectModel.Controls;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class CtKBracing : DaControl
    {
        private DaKBracing daKBracing { get; set; }
        private StringText ST_Tag { get; set; }
        public DoubleText DT_Bottom { get; set; }
        private DoubleText DT_Top { get; set; }
        private DoubleText DT_Mid { get; set; }
        private CtProfileInputList ctProfileDetailList { get; set; }
        public TabControl tabcKBracing { get; set; }
        private TabPage tabPageMain { get; set; }

        public CtKBracing(DaKBracing dakbracing) : base()
        {
            daKBracing = dakbracing;
        }

        public override bool Check()
        {
            failedControl = null;

            if (ST_Tag.Check() == false)
            {
                tabcKBracing.SelectedTab = tabPageMain;
                failedControl = ST_Tag.Control;
                return false;
            }
            else if (DT_Bottom.Check() == false)
            {
                tabcKBracing.SelectedTab = tabPageMain;
                failedControl = DT_Bottom.Control;
                return false;
            }
            else if (DT_Top.Check() == false)
            {
                tabcKBracing.SelectedTab = tabPageMain;
                failedControl = DT_Top.Control;
                return false;
            }
            else if (DT_Mid.Check() == false)
            {
                tabcKBracing.SelectedTab = tabPageMain;
                failedControl = DT_Mid.Control;
                return false;
            }
            else if (ctProfileDetailList.Check() == false)
            {                
                failedControl = ctProfileDetailList.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            tabcKBracing = new TabControl();
            tabcKBracing.Dock = DockStyle.Fill;
            parent.Controls.Add(tabcKBracing);

            #region add tab page for main data

            tabPageMain = new TabPage();

            tabPageMain.Name = "TabPage_Main";
            tabPageMain.Text = "Data";
            tabPageMain.UseVisualStyleBackColor = true;

            tabcKBracing.Controls.Add(tabPageMain);

            #endregion add tab page for main

            TextBox tb;
            Label lb;

            int tInc = 30;
            int tVal = t;

            lb = ControlRunTime.CreateLabel("Label_Tag", "Tag", l, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Tag", "", l + 50, tVal, 185, 23);
            ST_Tag = new StringText(tb, false);
            tabPageMain.Controls.Add(lb);
            tabPageMain.Controls.Add(tb);

            #region create text boxs for bottom and top

            bool createTextBottom = daKBracing.CreateTextBottom();
            bool createTextTop = daKBracing.CreateTextTop();

            if (createTextBottom == false && createTextTop == false)
            {
                throw new Exception("createTextBottom == false && createTextTop == false");

            }
            else if (createTextBottom == true && createTextTop == true)
            {
                tVal += tInc;

                lb = ControlRunTime.CreateLabel("Label_Bottom", "Bottom", l, tVal + 4, 75, 23);
                tb = ControlRunTime.CreateTextBox("Text_Bottom", "", l + 50, tVal, 185, 23);
                DT_Bottom = new DoubleText(tb, false, false, true);
                tabPageMain.Controls.Add(lb);
                tabPageMain.Controls.Add(tb);

                tVal += tInc;

                lb = ControlRunTime.CreateLabel("Label_Top", "Top", l, tVal + 4, 75, 23);
                tb = ControlRunTime.CreateTextBox("Text_Top", "", l + 50, tVal, 185, 23);
                DT_Top = new DoubleText(tb, false, false, false);
                tabPageMain.Controls.Add(lb);
                tabPageMain.Controls.Add(tb);
            }
            else if (createTextBottom == true)
            {
                tVal += tInc;

                lb = ControlRunTime.CreateLabel("Label_Bottom", "Bottom", l, tVal + 4, 75, 23);
                tb = ControlRunTime.CreateTextBox("Text_Bottom", "", l + 50, tVal, 185, 23);
                DT_Bottom = new DoubleText(tb, false, false, true);
                tabPageMain.Controls.Add(lb);
                tabPageMain.Controls.Add(tb);

                //dummy
                tb = ControlRunTime.CreateTextBox("Text_Dummy", "", 0, 0, 20, 23);
                DT_Top = new DoubleText(tb, false, false, false);                
            }
            else if (createTextTop == true)
            {
                tVal += tInc;

                lb = ControlRunTime.CreateLabel("Label_Top", "Top", l, tVal + 4, 75, 23);
                tb = ControlRunTime.CreateTextBox("Text_Top", "", l + 50, tVal, 185, 23);
                DT_Top = new DoubleText(tb, false, false, false);
                tabPageMain.Controls.Add(lb);
                tabPageMain.Controls.Add(tb);

                //dummy
                tb = ControlRunTime.CreateTextBox("Text_Dummy", "", 0, 0, 20, 23);
                DT_Bottom = new DoubleText(tb, false, false, false);
            }

            #endregion create text boxs for bottom and top

            tVal += tInc;

            lb = ControlRunTime.CreateLabel("Label_Mid", "Mid", l, tVal + 4, 75, 23);
            tb = ControlRunTime.CreateTextBox("Text_Mid", "", l + 50, tVal, 185, 23);
            DT_Mid = new DoubleText(tb, false, false, false);
            tabPageMain.Controls.Add(lb);
            tabPageMain.Controls.Add(tb);

            tVal += 50;

            ctProfileDetailList = new CtProfileInputList(daKBracing.GetProfiles());
            ctProfileDetailList.Create(tabPageMain, l, tVal);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.KBracing;
        }

        public override void Get()
        {
            daKBracing.Tag = ST_Tag.Get();
            daKBracing.Bottom = DT_Bottom.Get();
            daKBracing.Top = DT_Top.Get();
            daKBracing.Mid = DT_Mid.Get();

            ctProfileDetailList.Get();
        }

        public override void Set()
        {
            ST_Tag.Set(daKBracing.Tag);
            DT_Bottom.Set(daKBracing.Bottom);
            DT_Top.Set(daKBracing.Top);
            DT_Mid.Set(daKBracing.Mid);

            ctProfileDetailList.Set();
        }
    }
}
