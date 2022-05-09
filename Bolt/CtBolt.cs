using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesUI;
using DetailingObjectModel.Controls;
using System.Windows.Forms;
using DetailingObjectModel.Profile;

namespace DetailingObjectModel.Bolt
{
    public class CtBolt : DaControl
    {
        public DaBolt daBolt { get; set; }
        public CtBoltLayout ctBoltLayout { get; set; }
        public CtBoltType ctBoltType { get; set; }
        public CtBoltOffset ctBoltOffset { get; set; }
        public CtBoltDetailType ctBoltDetailType { get; set; }
        public List<CtBoltDetail> ctBoltDetails { get; set; }
        public TabPage tabPageLayout { get; set; }
        public TabPage tabPageBolt { get; set; }
        public TabControl tabControlBolt { get; set; }
        public TabPage tabPageBoltType { get; set; }
        public TabPage tabPageBoltDetail { get; set; }
        public List<TabPage> tabPageBoltDetails { get; set; }

        public CtBolt(DaBolt da_bolt) : base()
        {
            daBolt = da_bolt;

            ctBoltLayout = new CtBoltLayout(daBolt.boltLayout);
            ctBoltType = new CtBoltType(daBolt.boltType);
            ctBoltOffset = new CtBoltOffset(daBolt.boltOffset);
            ctBoltDetailType = new CtBoltDetailType();

            ctBoltDetails = new List<CtBoltDetail>();

            foreach (var item in daBolt.boltDetails)
            {
                ctBoltDetails.Add(CtBoltDetail.CreateCtBoltDetailClass(item));
            }
        }

        public override DaCtType daCtType()
        {
            return DaCtType.Bolt;
        }

        public override void Create(Control parent, int l, int t)
        {
            #region bolt

            #region bolt tab page

            tabPageBolt = new TabPage();

            tabPageBolt.Name = "TabPage_Bolt";
            tabPageBolt.Text = " Bolt";
            tabPageBolt.UseVisualStyleBackColor = true;

            parent.Controls.Add(tabPageBolt);

            tabPageBolt.Enter += new EventHandler(TabPageBolt_Enter);
            tabPageBolt.Leave += new EventHandler(TabPageBolt_Leave);

            #endregion profile tab page

            #region bolt tab control

            tabControlBolt = new TabControl();
            tabControlBolt.Dock = DockStyle.Fill;
            tabPageBolt.Controls.Add(tabControlBolt);

            #endregion bolt tab control

            #region tab page bolt type

            tabPageBoltType = new TabPage();

            tabPageBoltType.Name = "TabPage_BoltType";
            tabPageBoltType.Text = "Bolt Type";
            tabPageBoltType.UseVisualStyleBackColor = true;

            tabControlBolt.Controls.Add(tabPageBoltType);

            #endregion tab page bolt type

            #region tab page bolt detail

            tabPageBoltDetail = new TabPage();

            tabPageBoltDetail.Name = "TabPage_BoltDetail";
            tabPageBoltDetail.Text = " BoltDetail";
            tabPageBoltDetail.UseVisualStyleBackColor = true;

            tabControlBolt.Controls.Add(tabPageBoltDetail);

            #endregion tab page bolt detail

            #region add tab pages for profile details

            tabPageBoltDetails = new List<TabPage>();

            foreach (var item in daBolt.boltDetails)
            {
                tabPageBoltDetails.Add(new TabPage());

                tabPageBoltDetails.Last().Name = "TabPage_BoltDetail" + Enum.GetName(item.boltDetailType());
                tabPageBoltDetails.Last().Text = "Bolt Detail " + item.boltDetailType();
                tabPageBoltDetails.Last().UseVisualStyleBackColor = true;

                //tabControlBolt.Controls.Add(tabPageBoltDetails.Last());
            }

            #endregion add tab pages for profile details

            #endregion bolt

            #region create controls for bolt            

            ctBoltLayout.Create(tabPageLayout, 40, 200);
            ctBoltType.Create(tabPageBoltType, 40, 40);
            ctBoltOffset.Create(tabPageBoltDetail, 180, 50);
            ctBoltDetailType.Create(tabPageBoltDetail, 40, 40);
            ctBoltDetailType.funcBoltDetailChanged += OnBoltDetailChanged;

            for (int i = 0; i < ctBoltDetails.Count; i++)
            {
                ctBoltDetails[i].Create(tabPageBoltDetails[i], 40, 40);
            }            

            #endregion create controls for bolt
        }

        public override bool Check()
        {
            failedControl = null;

            //if (ctBoltLayout.Check() == false)
            //{
            //    failedControl = ctBoltLayout.failedControl;
            //    failedTabPage = tabPageLayout;
            //    return false;
            //}
            if (ctBoltType.Check() == false)
            {
                failedControl = ctBoltType.failedControl;
                tabControlBolt.SelectedTab = tabPageBoltType;
                failedTabPage = tabPageBolt;
                return false;
            }
            else if (ctBoltOffset.Check() == false)
            {
                failedControl = ctBoltOffset.failedControl;
                tabControlBolt.SelectedTab = tabPageBoltDetail;
                failedTabPage = tabPageBolt;
                return false;
            }
            else if (ctBoltDetailType.Check() == false)
            {
                failedControl = ctBoltDetailType.failedControl;
                tabControlBolt.SelectedTab = tabPageBoltDetail;
                failedTabPage = tabPageBolt;
                return false;
            }
            else
            {
                EBoltDetailType boltDetailType = ctBoltDetailType.SC_boltDetailType.Get();

                for (int i = 0; i < ctBoltDetails.Count; i++)
                {
                    CtBoltDetail ct = ctBoltDetails[i];

                    if (ct.daBoltDetail.boltDetailType() == boltDetailType)
                    {
                        if (ct.Check() == false)
                        {
                            failedControl = ct.failedControl;
                            tabControlBolt.SelectedTab = tabPageBoltDetails[i];
                            failedTabPage = tabPageBolt;
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override void Get()
        {
            ctBoltLayout.Get();
            ctBoltType.Get();
            ctBoltOffset.Get();
            daBolt.boltDetailType = ctBoltDetailType.SC_boltDetailType.Get();

            for (int i = 0; i < ctBoltDetails.Count; i++)
            {
                CtBoltDetail ct = ctBoltDetails[i];

                if (ct.daBoltDetail.boltDetailType() == daBolt.boltDetailType)
                {
                    ct.Get();
                }
            }
        }

        public override void Set()
        {
            ctBoltLayout.Set();
            ctBoltType.Set();
            ctBoltOffset.Set();
            ctBoltDetailType.SC_boltDetailType.Set(daBolt.boltDetailType);

            foreach (var ct in ctBoltDetails)
            {
                ct.Set();
            }
        }

        public override void Refresh()
        {
            //in case daBolt has been changed outside.
            //purpose is to use same controls to set/get
            //different DaBolt

            ctBoltLayout.daBoltLayout = daBolt.boltLayout;
            ctBoltType.daBoltType = daBolt.boltType;
            ctBoltOffset.boltOffset = daBolt.boltOffset;
            //ctBoltDetailType. = new Ct_BoltDetailType();

            for (int i = 0; i < ctBoltDetails.Count; i++)
            {
                ctBoltDetails[i].daBoltDetail = daBolt.boltDetails[i];
                ctBoltDetails[i].Refresh();
            }
        }

        private void TabPageBolt_Enter(object sender, EventArgs e)
        {
            Set();

            OnBoltDetailChanged(ctBoltDetailType.SC_boltDetailType.Get());

            tabControlBolt.SelectedTab = tabPageBoltType;
        }

        private void TabPageBolt_Leave(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
            }
            else
            {
                CtProfileInput.canLeaveTabPage = false;
                ShowFailedControl();
            }
        }

        private void OnBoltDetailChanged(EBoltDetailType boltDetailType)
        {
            tabControlBolt.Visible = false;
            tabPageBolt.Enter -= TabPageBolt_Enter;

            foreach (TabPage tabPage in tabPageBoltDetails)
            {
                if (tabControlBolt.Contains(tabPage))
                {
                    tabControlBolt.Controls.Remove(tabPage);
                }                
            }

            for (int i = 0; i < ctBoltDetails.Count; i++)
            {
                if (ctBoltDetails[i].daBoltDetail.boltDetailType() == boltDetailType)
                {
                    tabControlBolt.Controls.Add(tabPageBoltDetails[i]);
                }
            }

            tabControlBolt.SelectedTab = tabPageBoltDetail;
            tabPageBolt.Enter += TabPageBolt_Enter;
            tabControlBolt.Visible = true;
        }

    }
}
