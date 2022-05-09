using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class DiProfileInput : BaseForm
    {
        private DaProfileInput daProfileInput { get; set; }
        private CtProfileInput ctProfileInput { get; set; }
        private static int selectedIndex { get; set; }

        public DiProfileInput(DaProfileInput daprofileinput) : base()
        {
            daProfileInput = daprofileinput;

            Size = new System.Drawing.Size(500, 500);
            Text = "Profile input for " + daProfileInput.Tag;

            FormClosing += DiProfileInput_Closing;

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctProfileInput = new CtProfileInput(daProfileInput);
            ctProfileInput.Create(this, 40, 40);

            DaControl.SetSelectedIndex(selectedIndex);
        }

        public override bool CheckDialogData(out Control failedControl)
        {
            failedControl = null;

            if (ctProfileInput.Check() == false)
            {
                failedControl = ctProfileInput.failedControl;
                return false;
            }

            return true;
        }

        public override void GetDialogData()
        {
            ctProfileInput.Get();
        }

        public override void SetDialogData()
        {
            ctProfileInput.Set();
        }

        private void DiProfileInput_Closing(object sender, FormClosingEventArgs e)
        {
            selectedIndex = DaControl.GetSelectedIndex();
        }
    }
}
