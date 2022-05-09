using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection.M1H1D
{
    public class MoCoM1H1DRightUp : MoCoM1H1D
    {
        public const int classIdentifier = 3;

        #region Create MoCoM1H1D class

        public static MoCoM1H1D CreateMoCoM1H1DClassRightUp(DaConnection daConnection, M1H1DType m1h1dType, MoProfile prHor, MoProfile prDia)
        {
            if (m1h1dType == M1H1DType.RightUp)
            {
                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                if (prHor.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("prHor.inProfile.daProfile.connectionEnd == null");
                }

                if (prDia.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prDia.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1H1DRightUp(daConnection, prHor, prDia);
            }

            return null;
        }

        public static MoCoM1H1D CreateMoCoM1H1DClassFromIdentifierRightUp(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 2)
                {
                    throw new Exception("profileInput.Count != 2");
                }

                MoProfile prHor = profileInput[0];
                MoProfile prDia = profileInput[1];

                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                if (prHor.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("prHor.inProfile.daProfile.connectionEnd == null");
                }

                if (prDia.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prDia.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1H1DRightUp(daConnection, prHor, prDia);
            }

            return null;
        }

        #endregion Create MoCoM1H1D class

        public MoCoM1H1DRightUp(DaConnection daConnection, MoProfile prhor, MoProfile prdia) : base(daConnection, prhor, prdia)
        {
        }

        public override M1H1DType m1h1dType()
        {
            return M1H1DType.RightUp;
        }

        public override string Caption()
        {
            return "M1H1D-RightUp";
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
                throw new Exception("prDown.daProfile.connectionEnd == null");
            }

            return prHor.inProfile.daProfile.connectionEnd;
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
                throw new Exception("prDia.daProfile.connectionStart == null");
            }

            return prDia.inProfile.daProfile.connectionStart;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prHor.cpE, MoObject.RadSphere, MoObject.SC_CoM1H1D));
        }
    }
}
