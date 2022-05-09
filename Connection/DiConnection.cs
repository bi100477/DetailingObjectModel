using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Connection
{
    public class DiConnection : BaseForm
    {
        private DaConnection daConnection { get; set; }
        public CtConnection ctConnection { get; set; }
        private static int selectedIndex { get; set; }

        public DiConnection(DaConnection daconnection) : base()
        {
            daConnection = daconnection;

            Size = new System.Drawing.Size(500, 500);
            Text = "Connection data for " + daConnection.Caption();

            FormClosing += DiConnection_Closing;

            GenerateControls();
        }

        public override bool CheckDialogData(out Control failedControl)
        {
            failedControl = null;

            if (ctConnection.Check() == false)
            {
                failedControl = ctConnection.failedControl;
                return false;
            }

            return true;
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctConnection = new CtConnection(daConnection);
            ctConnection.Create(this, 40, 40);

            ctConnection.tcConnection.SelectedIndex = selectedIndex;
        }

        public override void GetDialogData()
        {
            ctConnection.Get();
        }

        public override void SetDialogData()
        {
            ctConnection.Set();
        }

        private void DiConnection_Closing(object sender, FormClosingEventArgs e)
        {
            selectedIndex = ctConnection.tcConnection.SelectedIndex;
        }

    }
}
