using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M1H1D
{
    public class DaCoM1H1DRightDown : DaCoM1H1D
    {
        public const int classIdentifier = 2;

        #region Create DaCoM1H1D class

        public static DaCoM1H1D CreateDaCoM1H1DClassRightDown(M1H1DType m1h1dType, DaProfileInput prHor, DaProfileInput prDia)
        {
            if (m1h1dType == M1H1DType.RightDown)
            {
                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                if (prHor.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prHor.daProfile.connectionEnd != null");
                }

                prHor.daProfile.connectionEnd = new DaProfileEndConnection("End");

                if (prDia.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prDia.daProfile.connectionEnd != null");
                }

                prDia.daProfile.connectionEnd = new DaProfileEndConnection("End");

                return new DaCoM1H1DRightDown(prHor, prDia);
            }

            return null;
        }

        public static DaCoM1H1D CreateDaCoM1H1DClassFromIdentifierRightDown(int classidentifier, List<DaProfileInput> profileInput)
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

                if (prHor.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prHor.daProfile.connectionEnd != null");
                }

                prHor.daProfile.connectionEnd = new DaProfileEndConnection("End");

                if (prDia.daProfile.connectionEnd != null)
                {
                    MessageBox.Show("prDia.daProfile.connectionEnd != null");
                }

                prDia.daProfile.connectionEnd = new DaProfileEndConnection("End");

                return new DaCoM1H1DRightDown(prHor, prDia);
            }

            return null;
        }

        #endregion Create DaCoM1H1D class

        public DaCoM1H1DRightDown(DaProfileInput prhor, DaProfileInput prdia) : base(prhor, prdia)
        {
        }

        public override M1H1DType m1h1dType()
        {
            return M1H1DType.RightDown;
        }

        public override string Caption()
        {
            return "M1H1D-RightDown";
        }

        public override int IntIdentifier()
        {
            return classIdentifier;
        }


        public override bool HasHorizontal()
        {
            return true;
        }

        public override DaProfileInput GetHorizontal()
        {
            return prHor;
        }

        public override DaProfileEndConnection GetEndConnectionHorizontal()
        {
            if (prHor.daProfile.connectionEnd == null)
            {
                throw new Exception("prDown.daProfile.connectionEnd == null");
            }

            return prHor.daProfile.connectionEnd;
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
