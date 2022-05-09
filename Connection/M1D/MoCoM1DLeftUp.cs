using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetailingObjectModel.Profile;
using System.Windows.Forms;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection.M1D
{
    public class MoCoM1DLeftUp : MoCoM1D
    {
        public const int classIdentifier = 1;

        #region Create MoCoM1D class

        public static MoCoM1D CreateMoCoM1DClassLeftUp(DaConnection daConnection, M1DType m1dType, MoProfile moProfile)
        {
            if (m1dType == M1DType.LeftUp)
            {
                if (moProfile == null)
                {
                    throw new Exception("moProfile == null");
                }

                if (moProfile.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("moProfile.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1DLeftUp(daConnection, moProfile);
            }

            return null;
        }

        public static MoCoM1D CreateMoCoM1DClassFromIdentifierLeftUp(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
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

                return new MoCoM1DLeftUp(daConnection, profileInput[0]);
            }

            return null;
        }

        #endregion Create MoCoM1D class

        public MoCoM1DLeftUp(DaConnection daConnection, MoProfile prdia) : base(daConnection, prdia)
        {
        }

        public override M1DType m1dType()
        {
            return M1DType.LeftUp;
        }

        public override string Caption()
        {
            return "M1D-LeftUp";
        }

        public override bool HasDiagonalUp()
        {
            return true;
        }

        public override MoProfile GetDiagonalUp()
        {
            return prDia;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            if (prDia.inProfile.daProfile.connectionStart == null)
            {
                throw new Exception("prDia.inProfile.daProfile.connectionStart == null");
            }

            return prDia.inProfile.daProfile.connectionStart;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prDia.cpS, MoObject.RadSphere, MoObject.SC_CoM1D));
        }
    }
}
