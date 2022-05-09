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
    public class MoCoM1H1DLeftUp : MoCoM1H1D
    {
        public const int classIdentifier = 1;

        #region Create MoCoM1H1D class

        public static MoCoM1H1D CreateMoCoM1H1DClassLeftUp(DaConnection daConnection, M1H1DType m1h1dType, MoProfile prHor, MoProfile prDia)
        {
            if (m1h1dType == M1H1DType.LeftUp)
            {
                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                if (prHor.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prHor.inProfile.daProfile.connectionStart == null");
                }

                if (prDia.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prDia.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM1H1DLeftUp(daConnection, prHor, prDia);
            }

            return null;
        }

        public static MoCoM1H1D CreateMoCoM1H1DClassFromIdentifierLeftUp(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 2)
                {
                    throw new Exception("profileInput.Count != 2");
                }

                MoProfile prHor = profileInput[0];
                MoProfile prDia = profileInput[1];

                if (prHor.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prHor.daProfile.connectionStart == null");
                }

                if (prDia.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prDia.daProfile.connectionStart == null");
                }

                if (prHor == null || prDia == null)
                {
                    throw new Exception("prHor == null || prDia == null");
                }

                return new MoCoM1H1DLeftUp(daConnection, prHor, prDia);
            }

            return null;
        }


        #endregion Create MoCoM1H1D class

        public MoCoM1H1DLeftUp(DaConnection daConnection, MoProfile prhor, MoProfile prdia) : base(daConnection, prhor, prdia)
        {
        }

        public override M1H1DType m1h1dType()
        {
            return M1H1DType.LeftUp;
        }

        public override string Caption()
        {
            return "M1H1D-LeftUp";
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
                throw new Exception("prDown.daProfile.connectionStart == null");
            }

            return prHor.inProfile.daProfile.connectionStart;
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
            Entities.Add(new VisualSphere(prHor.cpS, MoObject.RadSphere, MoObject.SC_CoM1H1D));
        }
    }
}
