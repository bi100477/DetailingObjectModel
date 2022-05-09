using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class DiCoBracingSystem : BaseForm
    {
        public DaBracingSystem Data { get; set; }
        private CtCoBracingSystem ctBracingSystem { get; set; }

        public DiCoBracingSystem(DaBracingSystem data) : base()
        {
            Data = data;

            Size = new System.Drawing.Size(1200, 380);
            Text = "Bracing system connections " + Data.Caption;

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctBracingSystem = new CtCoBracingSystem(Data);
            ctBracingSystem.Create(this, 40, 40);
        }
    }
}
