using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class DiDaBracingSystem : BaseForm
    {
        public DaBracingSystem Data { get; set; }
        private CtDaBracingSystem ctBracingSystem { get; set; }

        public DiDaBracingSystem(DaBracingSystem data) : base()
        {
            Data = data;

            Size = new System.Drawing.Size(430, 380);
            Text = "Bracing system data " + Data.Caption;

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctBracingSystem = new CtDaBracingSystem(Data);
            ctBracingSystem.Create(this, 40, 40);
        }
    }
}
