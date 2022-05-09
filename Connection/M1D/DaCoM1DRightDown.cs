using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M1D
{
    public class DaCoM1DRightDown : DaCoM1D
    {
        public const int classIdentifier = 2;

        #region Create DaCoM1D class

        public static DaCoM1D CreateDaCoM1DClassRightDown(M1DType m1dType, DaProfileInput profileInput)
        {
            if (m1dType == M1DType.RightDown)
            {
                if (profileInput == null)
                {
                    throw new Exception("profileInput == null");
                }

                if (profileInput.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("profileInput.daProfile.connectionEnd != null");
                }

                profileInput.daProfile.connectionEnd = new DaProfileEndConnection("End");

                return new DaCoM1DRightDown(profileInput);
            }

            return null;
        }

        public static DaCoM1D CreateDaCoM1DClassFromIdentifierRightDown(int classidentifier, List<DaProfileInput> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 1)
                {
                    throw new Exception("profileInput.Count != 1");
                }

                if (profileInput[0].daProfile.connectionEnd != null)
                {
                    MessageBox.Show("profileInput[0].daProfile.connectionEnd != null");
                }

                profileInput[0].daProfile.connectionEnd = new DaProfileEndConnection("End");

                return new DaCoM1DRightDown(profileInput[0]);
            }

            return null;
        }

        #endregion Create DaCoM1D class

        public DaCoM1DRightDown(DaProfileInput prdia) : base(prdia)
        {
        }

        public override M1DType m1dType()
        {
            return M1DType.RightDown;
        }

        public override string Caption()
        {
            return "M1D-RightDown";
        }

        public override int IntIdentifier()
        {
            return classIdentifier;
        }

        public override bool HasDiagonalDown()
        {
            return true;
        }

        public override DaProfileInput GetDiagonalDown()
        {
            return prDia;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalDown()
        {
            if (prDia.daProfile.connectionEnd == null)
            {
                throw new Exception("prDia.daProfile.connectionEnd == null");
            }

            return prDia.daProfile.connectionEnd;
        }
    }
}
