using DetailingObjectModel.Bracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.MainLeg
{
    public class CtDaMainLegContainer : DaControl
    {
        public DaMainLegContainer mainLegContainer { get; set; }
        public ListBox List_MainLeg { get; set; }
        public TabPage tabPageMainLegs { get; set; }
        public CtMainLeg ctMainLeg { get; set; }
        public DaMainLeg daMainLeg { get; set; }
        public Button Button_Add { get; set; }


        public CtDaMainLegContainer(DaMainLegContainer mainlegcontainer) : base()
        {
            mainLegContainer = mainlegcontainer;
            canLeaveTabPage = true;

            daMainLeg = new DaMainLeg();
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            SetTabControl(parent);

            #region main legs tab page

            tabPageMainLegs = new TabPage();

            tabPageMainLegs.Name = "TabPage_MainLegs";
            tabPageMainLegs.Text = "Main Legs";
            tabPageMainLegs.UseVisualStyleBackColor = true;

            tabPageMainLegs.Enter += new EventHandler(TabPageMainLegs_Enter);
            tabPageMainLegs.Leave += new EventHandler(TabPageMainLegs_Leave);

            tabControl.Controls.Add(tabPageMainLegs);

            #endregion main legs tab page

            #region add control to main legs tab

            Label Label_MainLeg = ControlRunTime.CreateLabel("Label_MainLeg", "Main legs", l, t, 135, 19);
            tabPageMainLegs.Controls.Add(Label_MainLeg);

            List_MainLeg = ControlRunTime.CreateListBox("List_MainLeg", "", l, t + 20, 150, 200);
            List_MainLeg.Click += List_MainLeg_Click;

            tabPageMainLegs.Controls.Add(List_MainLeg);

            Button_Add = ControlRunTime.CreateButton("Button_Add", "Add", l + 170, t + 20, 75, 23);
            tabPageMainLegs.Controls.Add(Button_Add);
            Button_Add.Click += new EventHandler(Button_Add_Click);

            RefreshList();

            #endregion add control to main legs tab

            ctMainLeg = new CtMainLeg(daMainLeg);
            ctMainLeg.Create(tabControl, 40, 40);
            ctMainLeg.Set();
        }

        public override DaCtType daCtType()
        {
            return DaCtType.DaBracingSystem;
        }

        public override void Get()
        {
        }

        public override void Set()
        {
        }

        protected void List_MainLeg_Click(object sender, EventArgs e)
        {
            int si = List_MainLeg.SelectedIndex;

            if (si != -1)
            {                
                DaMainLeg ml = mainLegContainer.mainLegs[si];

                ctMainLeg.daMainLeg = ml;
                ctMainLeg.tabPageMainLeg.Text = ml.Tag;

                ctMainLeg.Refresh();
            }
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            mainLegContainer.mainLegs.Add(daMainLeg);
            daMainLeg = new DaMainLeg();

            ctMainLeg.daMainLeg = daMainLeg;
            ctMainLeg.Refresh();

            RefreshList();
        }

        private void RefreshList()
        {
            List_MainLeg.BeginUpdate();

            List_MainLeg.Items.Clear();

            foreach (var item in mainLegContainer.mainLegs)
            {
                List_MainLeg.Items.Add(item.Tag);
            }

            List_MainLeg.EndUpdate();
        }

        private void TabPageMainLegs_Enter(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void TabPageMainLegs_Leave(object sender, EventArgs e)
        {
        }
    }
}
