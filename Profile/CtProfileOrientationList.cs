using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileOrientationList : DaControl
    {
        public List<DaProfileOrientation> Orientations { get; set; }
        public ListBox List_Orientations { get; set; }
        private DaProfileOrientation daProfileOrientation { get; set; }
        public CtProfileOrientation ctProfileOrientation { get; set; }
        private int indOld { get; set; }

        public CtProfileOrientationList(List<DaProfileOrientation> orientations) : base()
        {
            Orientations = orientations;

            indOld = -1;
        }

        public override bool Check()
        {
            if (ctProfileOrientation.Check() == false)
            {
                failedControl = ctProfileOrientation.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Label lb = ControlRunTime.CreateLabel("Label_Orientations", "Orientations", l, t, 135, 19);
            parent.Controls.Add(lb);

            List_Orientations = ControlRunTime.CreateListBox("List_Orientations", "", l, t + 20, 120, 160);
            List_Orientations.Click += new EventHandler(List_Orientations_Click);
            parent.Controls.Add(List_Orientations);

            RefreshList();

            daProfileOrientation = new DaProfileOrientation("Not set");
            ctProfileOrientation = new CtProfileOrientation(daProfileOrientation);
            ctProfileOrientation.Create(parent, l, t);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileOrientationList;
        }

        public override void Get()
        {
            ctProfileOrientation.Get();
        }

        public override void Set()
        {
            ctProfileOrientation.Set();
        }

        public override void Refresh()
        {
            RefreshList();

            indOld = -1;
            ctProfileOrientation.daProfileOrientation = new DaProfileOrientation("Not set");
        }

        private void RefreshList()
        {
            List_Orientations.BeginUpdate();

            List_Orientations.Items.Clear();

            foreach (var orientation in Orientations)
            {
                List_Orientations.Items.Add(orientation.Tag);
            }

            List_Orientations.EndUpdate();
        }

        private void List_Orientations_Click(object sender, EventArgs e)
        {
            int ii = List_Orientations.SelectedIndex;

            if (ii > -1)
            {
                if (ii == indOld)
                {
                    return;
                }

                if (indOld != -1)
                {
                    if (ctProfileOrientation.Check() == true)
                    {
                        ctProfileOrientation.Get();
                    }
                    else
                    {
                        Control failedControl = ctProfileOrientation.failedControl;

                        MessageBox.Show("invalid input", "Warning");
                        failedControl.Visible = true;
                        failedControl.Focus();

                        List_Orientations.SelectedIndex = indOld;

                        return;
                    }
                }

                ctProfileOrientation.daProfileOrientation = Orientations[ii];
                ctProfileOrientation.Refresh();
                ctProfileOrientation.Set();

                indOld = ii;
            }
        }
    }
}
