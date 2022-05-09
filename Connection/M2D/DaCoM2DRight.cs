using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M2D
{
    public class DaCoM2DRight : DaCoM2D
    {
        public const int classIdentifier = 1;

        #region Create DaCoM2D class

        public static DaCoM2D CreateDaCoM2DClassRight(M2DType m2dType, DaProfileInput prDown, DaProfileInput prUp)
        {
            if (m2dType == M2DType.Right)
            {
                if (prDown == null || prUp == null)
                {
                    throw new Exception("prDown == null || prUp == null");
                }

                if (prDown.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prDown.daProfile.connectionEnd != null");
                }

                prDown.daProfile.connectionEnd = new DaProfileEndConnection("End");

                if (prUp.daProfile.connectionStart != null)
                {
                    MessageBox.Show("prUp.daProfile.connectionStart != null");
                }

                prUp.daProfile.connectionStart = new DaProfileEndConnection("Start");

                return new DaCoM2DRight(prDown, prUp);
            }

            return null;
        }

        public static DaCoM2D CreateDaCoM2DClassFromIdentifierRight(int classidentifier, List<DaProfileInput> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 2)
                {
                    throw new Exception("profileInput.Count != 2");
                }

                DaProfileInput prDown = profileInput[0];
                DaProfileInput prUp = profileInput[1];

                if (prDown == null || prUp == null)
                {
                    throw new Exception("prDown == null || prUp == null");
                }

                if (prDown.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prDown.daProfile.connectionEnd != null");
                }

                prDown.daProfile.connectionEnd = new DaProfileEndConnection("End");

                if (prUp.daProfile.connectionStart != null)
                {
                    MessageBox.Show("prUp.daProfile.connectionStart != null");
                }

                prUp.daProfile.connectionStart = new DaProfileEndConnection("Start");

                return new DaCoM2DRight(prDown, prUp);
            }

            return null;
        }

        #endregion Create DaCoM2D class

        public DaCoM2DRight(DaProfileInput prdown, DaProfileInput prup) : base(prdown, prup)
        {
        }

        public override M2DType m2dType()
        {
            return M2DType.Right;
        }

        public override string Caption()
        {
            return "M2D-Right";
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
            return prDown;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalDown()
        {
            if (prDown.daProfile.connectionEnd == null)
            {
                throw new Exception("prDown.daProfile.connectionEnd == null");
            }

            return prDown.daProfile.connectionEnd;
        }

        public override bool HasDiagonalUp()
        {
            return true;
        }

        public override DaProfileInput GetDiagonalUp()
        {
            return prUp;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            if (prUp.daProfile.connectionStart == null)
            {
                throw new Exception("prUp.daProfile.connectionStart == null");
            }

            return prUp.daProfile.connectionStart;
        }
    }
}
