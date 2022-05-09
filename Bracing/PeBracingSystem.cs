using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;

namespace DetailingObjectModel.Bracing
{
    public class PeBracingSystem : PropertyEditor
    {
        public MoBracingSystem moBracingSystem { get; set; }

        public PeBracingSystem(MoBracingSystem mobracingsystem) : base()
        {
            moBracingSystem = mobracingsystem;
        }

        public override void ShowPropertyEditor()
        {
            moBracingSystem.daBracingSystem.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moBracingSystem.Entities)
            {
                entities.Add(item);
            }

            return entities;
        }
    }
}
