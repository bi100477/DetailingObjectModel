using DetailingObjectModel.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class DiKBracing : BaseForm
    {
        private DaKBracing daKBracing { get; set; }        
        public CtKBracing ctKBracing { get; set; }
        private static int selectedIndex { get; set; }

        public DiKBracing(DaKBracing dakbraicng) : base()
        {
            daKBracing = dakbraicng;

            Size = new System.Drawing.Size(700, 500);
            Text = "Bracing data for " + daKBracing.Caption();

            FormClosing += DiKBracing_Closing;

            GenerateControls();
        }

        public override bool CheckDialogData(out Control failedControl)
        {
            failedControl = null;

            if (ctKBracing.Check() == false)
            {
                failedControl = ctKBracing.failedControl;
                return false;
            }

            return true;
        }

        public override void GenerateControls()
        {
            base.GenerateControls();            

            ctKBracing = new CtKBracing(daKBracing);
            ctKBracing.Create(this, 40, 40);

            ctKBracing.tabcKBracing.SelectedIndex = selectedIndex;
        }

        public override void GetDialogData()
        {
            ctKBracing.Get();
        }

        public override void SetDialogData()
        {
            ctKBracing.Set();
        }

        private void DiKBracing_Closing(object sender, FormClosingEventArgs e)
        {
            selectedIndex = ctKBracing.tabcKBracing.SelectedIndex;
        }

    }
}