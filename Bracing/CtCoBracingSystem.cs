using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;


namespace DetailingObjectModel.Bracing
{
    public class CtCoBracingSystem : DaControl
    {
        public DaBracingSystem daBracingSystem { get; set; }
        public ListBox List_DaBracing { get; set; }
        public ListBox List_DaBracingCouple { get; set; }
        public ListBox List_DaConnBracingsLeft { get; set; }
        public ListBox List_DaConnBracingsRight { get; set; }
        public ListBox List_DaConnCouplesLeft { get; set; }
        public ListBox List_DaConnCouplesRight { get; set; }

        public CtCoBracingSystem(DaBracingSystem dabracingsystem) : base()
        {
            daBracingSystem = dabracingsystem;
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            int dLeft = 180;
            int lCur = l;

            #region Bracing list

            Label Label_DaBracing = ControlRunTime.CreateLabel("Label_DaBracing", "Bracings", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaBracing);

            List_DaBracing = ControlRunTime.CreateListBox("List_DaBracing", "", l, t + 20, 150, 200);
            //Label_DaBracingCouple.DoubleClick += List_DaBracing_DblClick;

            parent.Controls.Add(List_DaBracing);

            lCur += dLeft;

            #endregion Bracing list

            #region BracingCouple list

            Label Label_DaBracingCouple = ControlRunTime.CreateLabel("Label_DaBracingCouple", "Bracing couples", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaBracingCouple);

            List_DaBracingCouple = ControlRunTime.CreateListBox("List_DaBracingCouple", "", lCur, t + 20, 150, 200);
            //List_DaBracingCouple.DoubleClick += List_DaBracingCouple_DblClick;

            parent.Controls.Add(List_DaBracingCouple);

            #endregion BracingCouple list

            lCur += dLeft;

            #region ConnBracingsLeft list

            Label Label_DaConnBracingsLeft = ControlRunTime.CreateLabel("Label_DaConnBracingsLeft", "Connections bracings left", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaConnBracingsLeft);

            List_DaConnBracingsLeft = ControlRunTime.CreateListBox("List_DaConnBracingsLeft", "", lCur, t + 20, 150, 200);
            List_DaConnBracingsLeft.DoubleClick += List_DaConnBracingsLeft_DblClick;

            parent.Controls.Add(List_DaConnBracingsLeft);

            #endregion ConnBracingsLeft list

            lCur += dLeft;

            #region ConnBracingsRight list

            Label Label_DaConnBracingsRight = ControlRunTime.CreateLabel("Label_DaConnBracingsRight", "Connections bracings right", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaConnBracingsRight);

            List_DaConnBracingsRight = ControlRunTime.CreateListBox("List_DaConnBracingsRight", "", lCur, t + 20, 150, 200);
            List_DaConnBracingsRight.DoubleClick += List_DaConnBracingsRight_DblClick;

            parent.Controls.Add(List_DaConnBracingsRight);

            #endregion ConnBracingsRight list

            lCur += dLeft;

            #region ConnCouplesLeft list

            Label Label_DaConnCouplesLeft = ControlRunTime.CreateLabel("Label_DaConnCouplesLeft", "Connections couples left", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaConnCouplesLeft);

            List_DaConnCouplesLeft = ControlRunTime.CreateListBox("List_DaConnCouplesLeft", "", lCur, t + 20, 150, 200);
            List_DaConnCouplesLeft.DoubleClick += List_DaConnCouplesLeft_DblClick;

            parent.Controls.Add(List_DaConnCouplesLeft);

            #endregion ConnCouplesLeft list

            lCur += dLeft;

            #region ConnCouplesRight list

            Label Label_DaConnCouplesRight = ControlRunTime.CreateLabel("Label_DaConnCouplesRight", "Connections couples right", lCur, t, 135, 19);
            parent.Controls.Add(Label_DaConnCouplesRight);

            List_DaConnCouplesRight = ControlRunTime.CreateListBox("List_DaConnCouplesRight", "", lCur, t + 20, 150, 200);
            List_DaConnCouplesRight.DoubleClick += List_DaConnCouplesRight_DblClick;

            parent.Controls.Add(List_DaConnCouplesRight);

            #endregion ConnCouplesRight list

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

        protected void List_DaBracingCouple_DblClick(object sender, EventArgs e)
        {
            int si = List_DaBracing.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Bracings[si].SetDataFromDialog();
                RefreshLists();
            }
        }

        protected void List_DaConnBracingsLeft_DblClick(object sender, EventArgs e)
        {
            int si = List_DaConnBracingsLeft.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Bracings[si].connLeft.SetDataFromDialog();
            }
        }

        protected void List_DaConnBracingsRight_DblClick(object sender, EventArgs e)
        {
            int si = List_DaConnBracingsRight.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Bracings[si].connRight.SetDataFromDialog();
            }
        }

        protected void List_DaConnCouplesLeft_DblClick(object sender, EventArgs e)
        {
            int si = List_DaConnCouplesLeft.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Couples[si].connLeft.SetDataFromDialog();
            }
        }

        protected void List_DaConnCouplesRight_DblClick(object sender, EventArgs e)
        {
            int si = List_DaConnCouplesRight.SelectedIndex;

            if (si != -1)
            {
                daBracingSystem.Couples[si].connRight.SetDataFromDialog();
            }
        }

        private void RefreshLists()
        {
            #region Bracings

            List_DaBracing.BeginUpdate();

            List_DaBracing.Items.Clear();

            foreach (var item in daBracingSystem.Bracings)
            {
                List_DaBracing.Items.Add(item.Tag);
            }

            List_DaBracing.EndUpdate();

            #endregion Bracings

            #region BracingCouples

            List_DaBracingCouple.BeginUpdate();

            List_DaBracingCouple.Items.Clear();

            foreach (var item in daBracingSystem.Couples)
            {
                List_DaBracingCouple.Items.Add(item.Tag);
            }

            List_DaBracingCouple.EndUpdate();

            #endregion BracingCouples

            #region ConnBracingsLeft

            List_DaConnBracingsLeft.BeginUpdate();

            List_DaConnBracingsLeft.Items.Clear();

            foreach (var item in daBracingSystem.Bracings)
            {
                if (item.connLeft != null)
                {
                    List_DaConnBracingsLeft.Items.Add(item.connLeft.Caption());
                }
            }

            List_DaConnBracingsLeft.EndUpdate();

            #endregion ConnBracingsLeft

            #region ConnBracingsRight

            List_DaConnBracingsRight.BeginUpdate();

            List_DaConnBracingsRight.Items.Clear();

            foreach (var item in daBracingSystem.Bracings)
            {
                if (item.connRight != null)
                {
                    List_DaConnBracingsRight.Items.Add(item.connRight.Caption());
                }                
            }

            List_DaConnBracingsRight.EndUpdate();

            #endregion ConnBracingsRight

            #region ConnCouplesLeft

            List_DaConnCouplesLeft.BeginUpdate();

            List_DaConnCouplesLeft.Items.Clear();

            foreach (var item in daBracingSystem.Couples)
            {
                if (item.connLeft != null)
                {
                    List_DaConnCouplesLeft.Items.Add(item.connLeft.Caption());
                }                
            }

            List_DaConnCouplesLeft.EndUpdate();

            #endregion ConnCouplesLeft

            #region ConnCouplesRight

            List_DaConnCouplesRight.BeginUpdate();

            List_DaConnCouplesRight.Items.Clear();

            foreach (var item in daBracingSystem.Couples)
            {
                if (item.connRight != null)
                {
                    List_DaConnCouplesRight.Items.Add(item.connRight.Caption());
                }                
            }

            List_DaConnCouplesRight.EndUpdate();

            #endregion ConnCouplesRight
        }
    }
}
