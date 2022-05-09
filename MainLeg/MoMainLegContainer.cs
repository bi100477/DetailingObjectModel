using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.MainLeg
{
    public class MoMainLegContainer : MoObject
    {
        public List<MoMainLeg> mainLegs { get; set; }
        public DaMainLegContainer daMainLegContainer { get; set; }

        public MoMainLegContainer(DaInput dainput) : base(dainput)
        {
            mainLegs = new List<MoMainLeg>();

            daMainLegContainer = (DaMainLegContainer)daObject;

            if (daMainLegContainer == null)
            {
                throw new Exception("daMainLegContainer == null");
            }
        }

        public override MoObType moObType()
        {
            return MoObType.MainLegContainer;
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void DrawSelectables()
        {
            foreach (var mainLeg in mainLegs)
            {
                mainLeg.DrawSelectables();
            }
        }

        internal MoProfile ProfileAtLevel(double level)
        {
            MoMainLeg mainLeg = mainLegs.Find(x => x.daMainLeg.Bottom < level && level < x.daMainLeg.Top);

            if (mainLeg != null)
            {
                return mainLeg.moProfile;
            }

            return null;
        }

    }
}
