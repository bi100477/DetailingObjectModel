using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.MainLeg
{
    public class DiMainLeg : BaseForm
    {
        public DaMainLeg Data { get; set; }
        private CtMainLeg ctMainLeg { get; set; }

        public DiMainLeg(DaMainLeg data) : base()
        {
            Data = data;

            Size = new System.Drawing.Size(500, 400);
            Text = "Main leg - " + Data.Tag;

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctMainLeg = new CtMainLeg(Data);
            ctMainLeg.Create(this, 40, 40);
        }

        public override bool CheckDialogData(out Control failedControl)
        {
            failedControl = null;

            if (ctMainLeg.Check() == false)
            {
                failedControl = ctMainLeg.failedControl;
                return false;
            }

            return true;
        }

        public override void GetDialogData()
        {
            ctMainLeg.Get();
        }

        public override void SetDialogData()
        {
            ctMainLeg.Set();
        }

    }
}
