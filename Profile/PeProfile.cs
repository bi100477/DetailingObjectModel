using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;

namespace DetailingObjectModel.Profile
{
    public class PeProfile : PropertyEditor
    {
        public MoProfile moProfile { get; set; }

        public PeProfile(MoProfile moprofile) : base()
        {
            moProfile = moprofile;
        }

        public override void ShowPropertyEditor()
        {
            moProfile.inProfile.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moProfile.Entities)
            {
                entities.Add(item);
            }

            return entities;
        }
    }
}
