using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M2D
{
    public class DaCoM2DLeft : DaCoM2D
    {
        public const int classIdentifier = 0;

        #region Create DaCoM2D class

        public static DaCoM2D CreateDaCoM2DClassLeft(M2DType m2dType, DaProfileInput prDown, DaProfileInput prUp)
        {
            if (m2dType == M2DType.Left)
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

                return new DaCoM2DLeft(prDown, prUp);
            }

            return null;
        }

        public static DaCoM2D CreateDaCoM2DClassFromIdentifierLeft(int classidentifier, List<DaProfileInput> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 2)
                {
                    throw new Exception("profileInput.Count != 2");
                }

                DaProfileInput prHor = profileInput[0];
                DaProfileInput prDia = profileInput[1];

                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                return new DaCoM2DLeft(prHor, prDia);
            }

            return null;
        }

        #endregion Create DaCoM2D class

        public DaCoM2DLeft(DaProfileInput prdown, DaProfileInput prup) : base(prdown, prup)
        {
        }

        public override M2DType m2dType()
        {
            return M2DType.Left;
        }

        public override string Caption()
        {
            return "M2D-Left";
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
