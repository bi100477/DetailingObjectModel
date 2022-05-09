using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class CtProfileInputList : DaControl
    {
        public List<DaProfileInput> Profiles { get; set; }
        public ListBox List_Profiles { get; set; }
        private DaProfileInput daProfileInput { get; set; }
        private CtProfileInput ctProfileInput { get; set; }
        private int indOld { get; set; }

        public CtProfileInputList(List<DaProfileInput> profiles) : base()
        {
            Profiles = profiles;

            indOld = -1;
        }

        public override bool Check()
        {                      
            if (ctProfileInput.Check() == false)
            {
                failedControl = ctProfileInput.failedControl;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Label lb = ControlRunTime.CreateLabel("Label_Profiles", "Profiles", l, t, 135, 19);
            parent.Controls.Add(lb);

            List_Profiles = ControlRunTime.CreateListBox("List_Profiles", "", l, t + 20, 120, 160);
            List_Profiles.Click += new EventHandler(List_Profiles_Click);
            parent.Controls.Add(List_Profiles);

            RefreshList();

            daProfileInput = new DaProfileInput("Not set");
            ctProfileInput = new CtProfileInput(daProfileInput);
            ctProfileInput.Create(parent, 40, 40);

            ctProfileInput.ctProfile.tabPageProfile.Text = "Profile (" + daProfileInput.Tag + ")";
            ctProfileInput.ctBolt.tabPageBolt.Text = "Bolt (" + daProfileInput.Tag + ")";

            ctProfileInput.Set();
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileInputList;
        }

        public override void Get()
        {
            ctProfileInput.Get();
        }

        public override void Set()
        {
            ctProfileInput.Set();
        }

        public override void Refresh()
        {
            RefreshList();
        }

        private void RefreshList()
        {
            List_Profiles.BeginUpdate();

            List_Profiles.Items.Clear();

            foreach (var profile in Profiles)
            {
                List_Profiles.Items.Add(profile.Tag);
            }

            List_Profiles.EndUpdate();
        }

        private void List_Profiles_Click(object sender, EventArgs e)
        {
            int ii = List_Profiles.SelectedIndex;

            if (ii > -1)
            {
                if (ii == indOld)
                {
                    return;
                }

                if (indOld != -1)
                {
                    if (ctProfileInput.Check() == true)
                    {
                        ctProfileInput.Get();
                    }
                    else
                    {
                        Control failedControl = ctProfileInput.failedControl;

                        MessageBox.Show("invalid input", "Warning");
                        failedControl.Visible = true;
                        failedControl.Focus();

                        List_Profiles.SelectedIndex = indOld;

                        return;
                    }
                }

                ctProfileInput.daProfileInput = Profiles[ii];
                ctProfileInput.Refresh();
                ctProfileInput.ctProfile.tabPageProfile.Text = "Profile (" + Profiles[ii].Tag + ")";
                ctProfileInput.ctBolt.tabPageBolt.Text = "Bolt (" + Profiles[ii].Tag + ")";
                ctProfileInput.Set();

                indOld = ii;
            }
        }
    }
}
