using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection.M1H
{
    public class MoCoM1HLeft : MoCoM1H
    {
        public const int classIdentifier = 0;

        #region Create MoCoM1H class

        public static MoCoM1H CreateMoCoM1HClassLeft(DaConnection daConnection, M1HType m1hType, MoProfile profileInput)
        {
            if (m1hType == M1HType.Left)
            {
                if (profileInput == null)
                {
                    throw new Exception("profileInput == null");
                }

                if (profileInput.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("profileInput.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1HLeft(daConnection, profileInput);
            }

            return null;
        }

        public static MoCoM1H CreateMoCoM1HClassFromIdentifierLeft(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 1)
                {
                    throw new Exception("profileInput.Count != 1");
                }

                if (profileInput[0].inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("profileInput[0].inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1HLeft(daConnection, profileInput[0]);
            }

            return null;
        }

        #endregion Create MoCoM1H class

        public MoCoM1HLeft(DaConnection daConnection, MoProfile prhor) : base(daConnection, prhor)
        {
        }

        public override M1HType m1hType()
        {
            return M1HType.Left;
        }

        public override string Caption()
        {
            return "M1H-Left";
        }

        public override bool HasHorizontal()
        {
            return true;
        }

        public override MoProfile GetHorizontal()
        {
            return prHor;
        }

        public override DaProfileEndConnection GetEndConnectionHorizontal()
        {
            if (prHor.inProfile.daProfile.connectionStart == null)
            {
                throw new Exception("prHor.daProfile.connectionStart == null");
            }

            return prHor.inProfile.daProfile.connectionStart;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prHor.cpS, MoObject.RadSphere, MoObject.SC_CoM1H));
        }
    }
}
