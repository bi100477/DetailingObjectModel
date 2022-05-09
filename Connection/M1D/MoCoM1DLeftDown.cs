using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesInterface.drawing;
using TypesInterface.geometry;

namespace DetailingObjectModel.Connection.M1D
{
    public class MoCoM1DLeftDown : MoCoM1D
    {
        public const int classIdentifier = 0;

        #region Create MoCoM1D class

        public static MoCoM1D CreateMoCoM1DClassLeftDown(DaConnection daConnection, M1DType m1dType, MoProfile moProfile)
        {
            if (m1dType == M1DType.LeftDown)
            {
                if (moProfile == null)
                {
                    throw new Exception("moProfile == null");
                }

                if (moProfile.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("moProfile.inProfile.daProfile.connectionEnd == null");
                }

                return new MoCoM1DLeftDown(daConnection, moProfile);
            }

            return null;
        }

        public static MoCoM1D CreateMoCoM1DClassFromIdentifierLeftDown(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
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

                return new MoCoM1DLeftDown(daConnection, profileInput[0]);
            }

            return null;
        }

        #endregion Create MoCoM1D class

        public MoCoM1DLeftDown(DaConnection daConnection, MoProfile prdia) : base(daConnection, prdia)
        {
        }

        public override M1DType m1dType()
        {
            return M1DType.LeftDown;
        }

        public override string Caption()
        {
            return "M1D-LeftDown";
        }

        public override bool HasDiagonalDown()
        {
            return true;
        }

        public override MoProfile GetDiagonalDown()
        {
            return prDia;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalDown()
        {
            if (prDia.inProfile.daProfile.connectionEnd == null)
            {
                throw new Exception("prDia.inProfile.daProfile.connectionEnd == null");
            }

            return prDia.inProfile.daProfile.connectionEnd;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prDia.cpE, MoObject.RadSphere, MoObject.SC_CoM1D));
        }
    }
}
