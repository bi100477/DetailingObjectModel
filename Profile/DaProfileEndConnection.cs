using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Profile
{
    public class DaProfileEndConnection : DaInput
    {
        public DaProfileEndConnection(string tag) : base()
        {
            Tag = tag;
        }

        public override DaInType daInType()
        {
            return DaInType.ProfileEndConnection;
        }

        public override bool SetDataFromDialog()
        {
            throw new NotImplementedException();
        }
    }
}
