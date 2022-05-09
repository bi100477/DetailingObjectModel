using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;


namespace DetailingObjectModel.Bracing
{
    public class DiDaBracingCenter : BaseForm
    {
        #region Params
        public DaBracingCenter daBracingCenter { get; set; }
        public CtDaBracingCenter ctDaBracingCenter { get; set; }

        #endregion Params

        public DiDaBracingCenter(DaBracingCenter dabracingcenter) : base()
        {
            daBracingCenter = dabracingcenter;

            Size = new System.Drawing.Size(380, 290);
            Text = "Bracing Center Data";

            GenerateControls();
        }

        public override void GenerateControls()
        {
            base.GenerateControls();

            ctDaBracingCenter = new CtDaBracingCenter(daBracingCenter);
            ctDaBracingCenter.Create(this, 40, 40);
        }

        public override void SetDialogData()
        {
            ctDaBracingCenter.Set();
        }
    }
}
