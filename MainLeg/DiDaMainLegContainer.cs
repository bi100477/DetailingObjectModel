using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesUI;

namespace DetailingObjectModel.MainLeg
{
    public class DiDaMainLegContainer : BaseForm
    {
        public DaMainLegContainer Data { get; set; }
        private CtDaMainLegContainer ctMainLegContainer { get; set; }

        public DiDaMainLegContainer(DaMainLegContainer data) : base()
        {
            Data = data;

            Size = new System.Drawing.Size(700, 400);
            Text = "Main leg container - " + Data.Tag;

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctMainLegContainer = new CtDaMainLegContainer(Data);
            ctMainLegContainer.Create(this, 40, 40);
        }
    }
}
