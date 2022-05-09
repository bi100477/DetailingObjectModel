using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Controls
{
    public class CtDoubleList : DaControl
    {
        public List<double> doubleVals { get; set; }
        public DoubleText DT_doubleVal { get; set; }
        public ListBox List_doubleVal { get; set; }
        public GroupBox Group_doubleVals { get; set; }

        public CtDoubleList(List<double> doublevals) : base()
        {
            doubleVals = doublevals;
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Group_doubleVals = ControlRunTime.CreateGroupBox("Group_doubleVals", "Double list", l, t, 235, 150);
            parent.Controls.Add(Group_doubleVals);

            Button btn;

            btn = ControlRunTime.CreateButton("Button_Add_Double", "Add", 95, 30, 60, 23);
            btn.Click += new EventHandler(Button_Add_Double_Click);
            Group_doubleVals.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Remove_Double", "Remove", 95, 53, 60, 23);
            btn.Click += new EventHandler(Button_Remove_Double_Click);
            Group_doubleVals.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Edit_Double", "Edit", 155, 30, 60, 23);
            btn.Click += new EventHandler(Button_Edit_Double_Click);
            Group_doubleVals.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Clear_Double", "Clear", 155, 53, 60, 23);
            btn.Click += new EventHandler(Button_Clear_Double_Click);
            Group_doubleVals.Controls.Add(btn);

            List_doubleVal = ControlRunTime.CreateListBox("List_doubleVal", "", 20, 30, 65, 95);
            List_doubleVal.Click += new EventHandler(List_doubleVal_Click);
            Group_doubleVals.Controls.Add(List_doubleVal);

            RefreshList();

            TextBox tb = ControlRunTime.CreateTextBox("Text_doubleVal", "", 95, 90, 120, 23);
            tb.KeyUp += new KeyEventHandler(Text_doubleVal_KeyUp);
            Group_doubleVals.Controls.Add(tb);

            DT_doubleVal = new DoubleText(tb, true, false, true);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.DoubleList;
        }

        public override void Get()
        {
        }

        public override void Set()
        {
        }

        private void RefreshList()
        {
            List_doubleVal.BeginUpdate();

            List_doubleVal.Items.Clear();

            foreach (var point in doubleVals)
            {
                List_doubleVal.Items.Add(point.ToString());
            }

            List_doubleVal.EndUpdate();
        }

        private void Button_Add_Double_Click(object sender, EventArgs e)
        {
            if (DT_doubleVal.Check() == true)
            {
                doubleVals.Add(DT_doubleVal.Get());
                RefreshList();
            }
        }

        private void Button_Remove_Double_Click(object sender, EventArgs e)
        {
            int ii = List_doubleVal.SelectedIndex;

            if (ii > -1)
            {
                doubleVals.RemoveAt(ii);
                RefreshList();
            }
        }

        private void Button_Edit_Double_Click(object sender, EventArgs e)
        {
            int ii = List_doubleVal.SelectedIndex;

            if (ii > -1)
            {
                doubleVals[ii] = DT_doubleVal.Get();
                RefreshList();
            }
        }

        private void Button_Clear_Double_Click(object sender, EventArgs e)
        {
            doubleVals.Clear();
            RefreshList();
        }

        private void List_doubleVal_Click(object sender, EventArgs e)
        {
            int ii = List_doubleVal.SelectedIndex;

            if (ii > -1)
            {
                DT_doubleVal.Control.Text = doubleVals[ii].ToString();
            }
        }

        private void Text_doubleVal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) // Escape
            {
            }
            else if (e.KeyValue == 13) // Enter
            {
                Button_Add_Double_Click(sender, e);
            }
        }

    }
}
