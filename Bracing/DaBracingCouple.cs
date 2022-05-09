using DetailingObjectModel.Connection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Bracing
{
    public class DaBracingCouple : DaInput
    {
        #region Params

        public double Level { get; set; }
        public DaBracing brBelow { get; set; }
        public DaBracing brAbove { get; set; }
        public DaConnection connLeft { get; set; }
        public DaConnection connRight { get; set; }

        #endregion Params

        public DaBracingCouple(DaBracing brbelow, DaBracing brabove) : base()
        {
            Tag = "";
            Level = 0.0;
            brBelow = brbelow;
            brAbove = brabove;

            connLeft = null;
            connRight = null;
        }

        public override DaInType daInType()
        {
            return DaInType.BracingCouple;
        }

        public override bool SetDataFromDialog()
        {
            throw new NotImplementedException();
        }

        #region I/O connections

        #region write
        public void WriteConnections(StreamWriter sw)
        {
            if (connLeft != null)
            {
                connLeft.Write(sw);
            }

            if (connRight != null)
            {
                connRight.Write(sw);
            }
        }

        #endregion write

        #region read
        public void ReadConnections(StreamReader sr)
        {
            connLeft = DaConnection.CreateDaConnectionClassLeft(this);

            if (connLeft != null)
            {
                connLeft.Read(sr);
            }

            connRight = DaConnection.CreateDaConnectionClassRight(this);

            if (connRight != null)
            {
                connRight.Read(sr);
            }
        }

        #endregion read

        #endregion I/O connections
    }
}
