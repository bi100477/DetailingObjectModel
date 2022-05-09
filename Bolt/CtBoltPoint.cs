using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltPoint : DaControl
    {
        public List<DaBoltPoint> boltPoints { get; set; }
        public DoublePairText DPT_boltPoint { get; set; }
        public ListBox List_boltPoint { get; set; }

        public CtBoltPoint(List<DaBoltPoint> boltpoints) : base()
        {
            boltPoints = boltpoints;
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            GroupBox gb = ControlRunTime.CreateGroupBox("Group_boltPoints", "Bolt points", l, t, 235, 150);
            parent.Controls.Add(gb);

            Button btn;

            btn = ControlRunTime.CreateButton("Button_Add_BoltPoint", "Add", 95, 30, 60, 23);
            btn.Click += new EventHandler(Button_Add_BoltPoint_Click);
            btn.Enabled = false;
            gb.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Remove_BoltPoint", "Remove", 95, 53, 60, 23);
            btn.Click += new EventHandler(Button_Remove_BoltPoint_Click);
            btn.Enabled = false;
            gb.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Edit_BoltPoint", "Edit", 155, 30, 60, 23);
            btn.Click += new EventHandler(Button_Edit_BoltPoint_Click);
            gb.Controls.Add(btn);

            btn = ControlRunTime.CreateButton("Button_Clear_BoltPoint", "Clear", 155, 53, 60, 23);
            btn.Click += new EventHandler(Button_Clear_BoltPoint_Click);
            btn.Enabled = false;
            gb.Controls.Add(btn);

            List_boltPoint = ControlRunTime.CreateListBox("List_boltPoint", "", 20, 30, 65, 95);
            List_boltPoint.Click += new EventHandler(List_BoltPoint_Click);
            gb.Controls.Add(List_boltPoint);            

            TextBox tb = ControlRunTime.CreateTextBox("Text_boltPoint", "", 95, 90, 120, 23);
            tb.KeyUp += new KeyEventHandler(Text_boltPoint_KeyUp);
            gb.Controls.Add(tb);

            DPT_boltPoint = new DoublePairText(tb, true, true, true);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.BoltPoint;
        }

        public override void Get()
        {
        }

        public override void Set()
        {
            RefreshList();
        }

        private void RefreshList()
        {
            List_boltPoint.BeginUpdate();

            List_boltPoint.Items.Clear();

            foreach (var point in boltPoints)
            {
                List_boltPoint.Items.Add(point.ToString());
            }

            List_boltPoint.EndUpdate();
        }

        private void Button_Add_BoltPoint_Click(object sender, EventArgs e)
        {
            if (DPT_boltPoint.Check() == true)
            {
                boltPoints.Add(new DaBoltPoint(DPT_boltPoint.Get()));
                RefreshList();
            }
        }

        private void Button_Remove_BoltPoint_Click(object sender, EventArgs e)
        {
            int ii = List_boltPoint.SelectedIndex;

            if (ii > -1)
            {
                boltPoints.RemoveAt(ii);
                RefreshList();
            }
        }

        private void Button_Edit_BoltPoint_Click(object sender, EventArgs e)
        {
            int ii = List_boltPoint.SelectedIndex;

            if (ii > -1)
            {
                (double, double) point = DPT_boltPoint.Get();

                boltPoints[ii] = new DaBoltPoint(point);
                RefreshList();
            }
        }

        private void Button_Clear_BoltPoint_Click(object sender, EventArgs e)
        {
            boltPoints.Clear();
            RefreshList();
        }

        private void List_BoltPoint_Click(object sender, EventArgs e)
        {
            int ii = List_boltPoint.SelectedIndex;

            if (ii > -1)
            {
                DPT_boltPoint.Control.Text = boltPoints[ii].ToString();
            }
        }

        private void Text_boltPoint_KeyUp(object sender, KeyEventArgs e)
        {          
            if (e.KeyValue == 27) // Escape
            {
            }
            else if (e.KeyValue == 13) // Enter
            {
                //Button_Add_BoltPoint_Click(sender, e);
                Button_Edit_BoltPoint_Click(sender, e);
            }
        }

    }
}
