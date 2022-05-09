using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetailingObjectModel.Profile;
using System.Windows.Forms;
using TypesUI;


namespace DetailingObjectModel.Connection
{
    public class CtConnection : DaControl
    {
        private DaConnection daConnection { get; set; }
        private CtProfileInputList ctProfileInputList { get; set; }
        public TabControl tcConnection { get; set; }
        private TabPage tabPageMain { get; set; }

        public CtConnection(DaConnection daconnection) : base()
        {
            daConnection = daconnection;
        }

        public override bool Check()
        {
            failedControl = null;

            if (ctProfileInputList.Check() == false)
            {
                failedControl = ctProfileInputList.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            tcConnection = new TabControl();
            tcConnection.Dock = DockStyle.Fill;
            parent.Controls.Add(tcConnection);

            #region add tab page for main data

            tabPageMain = new TabPage();

            tabPageMain.Name = "TabPage_Main";
            tabPageMain.Text = "Data";
            tabPageMain.UseVisualStyleBackColor = true;

            tcConnection.Controls.Add(tabPageMain);

            #endregion add tab page for main

            ctProfileInputList = new CtProfileInputList(daConnection.GetProfiles());
            ctProfileInputList.Create(tabPageMain, 40, 40);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.Connection;
        }

        public override void Get()
        {
            ctProfileInputList.Get();
        }

        public override void Set()
        {
            ctProfileInputList.Set();
        }
    }
}