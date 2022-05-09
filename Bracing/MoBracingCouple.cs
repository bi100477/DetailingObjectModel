using DetailingObjectModel.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bracing
{
    public class MoBracingCouple : MoObject
    {
        #region Params

        public DaBracingCouple daBracingCouple { get; set; }
        public MoBracing brBelow { get; set; }
        public MoBracing brAbove { get; set; }
        public MoConnection connLeft { get; set; }
        public MoConnection connRight { get; set; }

        #endregion Params

        public MoBracingCouple(DaInput dabracingcouple, MoBracing brbelow, MoBracing brabove) : base(dabracingcouple)
        {
            daBracingCouple = (DaBracingCouple)dabracingcouple;

            if (daBracingCouple == null)
            {
                throw new Exception("daBracingCouple == null");
            }

            brBelow = brbelow;
            brAbove = brabove;

            connLeft = null;
            connRight = null;
        }

        public override MoObType moObType()
        {
            return MoObType.BracingCouple;
        }

        public override void Create()
        {
            if (connLeft != null)
            {
                connLeft.Create();

                Entities.AddRange(connLeft.Entities);
                Points.AddRange(connLeft.Points);
                Lines.AddRange(connLeft.Lines);
            }

            if (connRight != null)
            {
                connRight.Create();

                Entities.AddRange(connRight.Entities);
                Points.AddRange(connRight.Points);
                Lines.AddRange(connRight.Lines);
            }
        }

        public override void DrawSelectables()
        {
            if (connLeft != null)
            {
                connLeft.DrawSelectables();
            }

            if (connRight != null)
            {
                connRight.DrawSelectables();
            }
        }
    }
}
