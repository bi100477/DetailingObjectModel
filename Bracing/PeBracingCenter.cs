using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;

namespace DetailingObjectModel.Bracing
{
    public class PeBracingCenter : PropertyEditor
    {
        public MoBracingCenter moBracingCenter { get; set; }

        public PeBracingCenter(MoBracingCenter mobracingcenter) : base()
        {
            moBracingCenter = mobracingcenter;
        }

        public override void ShowPropertyEditor()
        {
            moBracingCenter.daBracingCenter.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moBracingCenter.Entities)
            {
                entities.Add(item);
            }

            return entities;
        }
    }
}
