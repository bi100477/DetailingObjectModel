using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;


namespace DetailingObjectModel.Bracing
{
    public class PeBracing : PropertyEditor
    {
        public MoBracing moBracing { get; set; }

        public PeBracing(MoBracing mobracing) : base()
        {
            moBracing = mobracing;
        }

        public override void ShowPropertyEditor()
        {
            moBracing.daBracing.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moBracing.Entities)
            {
                entities.Add(item);
            }

            return entities;
        }
    }
}
