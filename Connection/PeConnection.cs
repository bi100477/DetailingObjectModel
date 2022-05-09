using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection
{
    public class PeConnection : PropertyEditor
    {
        public MoConnection moConnection { get; set; }

        public PeConnection(MoConnection mobracing) : base()
        {
            moConnection = mobracing;
        }

        public override void ShowPropertyEditor()
        {
            moConnection.daConnection.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moConnection.Entities)
            {
                entities.Add(item);
            }

            foreach (var item in moConnection.GetProfiles())
            {
                entities.AddRange(item.Entities);
            }

            return entities;
        }
    }
}
