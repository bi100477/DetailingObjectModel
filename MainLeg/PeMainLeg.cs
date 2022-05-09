using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;

namespace DetailingObjectModel.MainLeg
{
    public class PeMainLeg : PropertyEditor
    {
        public MoMainLeg moMainLeg { get; set; }

        public PeMainLeg(MoMainLeg momainleg) : base()
        {
            moMainLeg = momainleg;
        }

        public override void ShowPropertyEditor()
        {
            moMainLeg.daMainLeg.SetDataFromDialog();
        }

        public override List<VisualEntity> GetEntities()
        {
            List<VisualEntity> entities = new List<VisualEntity>();

            foreach (var item in moMainLeg.Entities)
            {
                entities.Add(item);
            }

            return entities;
        }
    }
}
