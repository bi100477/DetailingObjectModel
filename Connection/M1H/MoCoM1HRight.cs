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
    public class MoCoM1HRight : MoCoM1H
    {
        public const int classIdentifier = 1;

        #region Create MoCoM1H class

        public static MoCoM1H CreateMoCoM1HClassRight(DaConnection daConnection, M1HType m1hType, MoProfile profileInput)
        {
            if (m1hType == M1HType.Right)
            {
                if (profileInput == null)
                {
                    throw new Exception("profileInput == null");
                }

                if (profileInput.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("profileInput.inProfile.daProfile.connectionEnd == null");
                }

                return new MoCoM1HRight(daConnection, profileInput);
            }

            return null;
        }

        public static MoCoM1H CreateMoCoM1HClassFromIdentifierRight(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 1)
                {
                    throw new Exception("profileInput.Count != 1");
                }

                if (profileInput[0].inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("profileInput[0].inProfile.daProfile.connectionEnd == null");
                }

                return new MoCoM1HRight(daConnection, profileInput[0]);
            }

            return null;
        }

        #endregion Create MoCoM1H class

        public MoCoM1HRight(DaConnection daConnection, MoProfile prhor) : base(daConnection, prhor)
        {
        }

        public override M1HType m1hType()
        {
            return M1HType.Right;
        }

        public override string Caption()
        {
            return "M1H-Right";
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
            if (prHor.inProfile.daProfile.connectionEnd == null)
            {
                throw new Exception("prHor.inProfile.daProfile.connectionEnd == null");
            }

            return prHor.inProfile.daProfile.connectionEnd;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prHor.cpE, MoObject.RadSphere, MoObject.SC_CoM1H));
        }
    }
}
