using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M1D
{
    public class DaCoM1DRightUp : DaCoM1D
    {
        public const int classIdentifier = 3;

        #region Create DaCoM1D class

        public static DaCoM1D CreateDaCoM1DClassRightUp(M1DType m1dType, DaProfileInput profileInput)
        {
            if (m1dType == M1DType.RightUp)
            {
                if (profileInput == null)
                {
                    throw new Exception("profileInput == null");
                }

                if (profileInput.daProfile.connectionStart != null)
                {
                    MessageBox.Show("profileInput.daProfile.connectionStart != null");
                }

                profileInput.daProfile.connectionStart = new DaProfileEndConnection("Start");

                return new DaCoM1DRightUp(profileInput);
            }

            return null;
        }

        public static DaCoM1D CreateDaCoM1DClassFromIdentifierRightUp(int classidentifier, List<DaProfileInput> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 1)
                {
                    throw new Exception("profileInput.Count != 1");
                }

                if (profileInput[0].daProfile.connectionStart != null)
                {
                    MessageBox.Show("profileInput[0].daProfile.connectionStart != null");
                }

                profileInput[0].daProfile.connectionStart = new DaProfileEndConnection("Start");

                return new DaCoM1DRightUp(profileInput[0]);
            }

            return null;
        }

        #endregion Create DaCoM1D class

        public DaCoM1DRightUp(DaProfileInput prdia) : base(prdia)
        {
        }

        public override M1DType m1dType()
        {
            return M1DType.RightUp;
        }

        public override string Caption()
        {
            return "M1D-RightUp";
        }

        public override int IntIdentifier()
        {
            return classIdentifier;
        }

        public override bool HasDiagonalUp()
        {
            return true;
        }

        public override DaProfileInput GetDiagonalUp()
        {
            return prDia;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            if (prDia.daProfile.connectionStart == null)
            {
                throw new Exception("prDia.daProfile.connectionStart == null");
            }

            return prDia.daProfile.connectionStart;
        }
    }
}
