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
    public class CtDaBracingSystem : DaControl
    {
        public DaBracingSystem daBracingSystem { get; set; }
        public ListBox List_DaBracing { get; set; }
        public ListBox List_DaBracingType { get; set; }

        public CtDaBracingSystem(DaBracingSystem dabracingsystem) : base()
        {
            daBracingSystem = dabracingsystem;
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Label Label_DaBracing = ControlRunTime.CreateLabel("Label_DaBracing", "Bracings", l, t, 135, 19);
            Label Label_BracingType = ControlRunTime.CreateLabel("Label_BracingType", "Available bracing types", l + 180, t, 135, 19);

            parent.Controls.Add(Label_DaBracing);
            parent.Controls.Add(Label_BracingType);

            List_DaBracing = ControlRunTime.CreateListBox("List_DaBracing", "", l, t + 20, 150, 200);
            List_DaBracing.DoubleClick += List_DaBracing_DblClick;

            List_DaBracingType = ControlRunTime.CreateListBox("List_DaBracingType", "", l + 180, t + 20, 150, 200);
            List_DaBracingType.DoubleClick += List_DaBracingType_DblClick;

            parent.Controls.Add(List_DaBracing);
            parent.Controls.Add(List_DaBracingType);

            RefreshLists();
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

        protected void List_DaBracing_DblClick(object sender, EventArgs e)
        {
            int si = List_DaBracing.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Bracings[si].SetDataFromDialog();
                RefreshLists();
            }
        }

        protected void List_DaBracingType_DblClick(object sender, EventArgs e)
        {
            var si = List_DaBracingType.SelectedItem;

            if (si != null)
            {
                DaBracing daBracing = DaBracing.CreateDaBracingClassFromIdentifier(si.ToString());

                if (daBracing.SetDataFromDialog() == true)
                {
                    daBracingSystem.Bracings.Add(daBracing);
                    RefreshLists();
                }
            }
        }

        private void RefreshLists()
        {
            List_DaBracing.BeginUpdate();

            List_DaBracing.Items.Clear();

            foreach (var item in daBracingSystem.Bracings)
            {
                List_DaBracing.Items.Add(item.Tag);
            }

            List_DaBracing.EndUpdate();


            List_DaBracingType.BeginUpdate();

            List_DaBracingType.Items.Clear();

            foreach (var identifier in DaBracing.daBracingClassIdenitifiers)
            {
                List_DaBracingType.Items.Add(identifier);
            }

            List_DaBracingType.EndUpdate();
        }
    }
}