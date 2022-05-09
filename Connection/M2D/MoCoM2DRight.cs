using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection.M2D
{
    public class MoCoM2DRight : MoCoM2D
    {
        public const int classIdentifier = 1;

        #region Create MoCoM2D class

        public static MoCoM2D CreateMoCoM2DClassRight(DaConnection daConnection, M2DType m2dType, MoProfile prDown, MoProfile prUp)
        {
            if (m2dType == M2DType.Right)
            {
                if (prDown == null || prUp == null)
                {
                    throw new Exception("prDown == null || prUp == null");
                }

                if (prDown.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("prDown.inProfile.daProfile.connectionEnd == null");
                }

                if (prUp.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prUp.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM2DRight(daConnection, prDown, prUp);
            }

            return null;
        }

        public static MoCoM2D CreateMoCoM2DClassFromIdentifierRight(DaConnection daConnection, int classidentifier, List<MoProfile> profileInput)
        {
            if (classidentifier == classIdentifier)
            {
                if (profileInput.Count != 2)
                {
                    throw new Exception("profileInput.Count != 2");
                }

                MoProfile prDown = profileInput[0];
                MoProfile prUp = profileInput[1];

                if (prDown == null || prUp == null)
                {
                    throw new Exception("prDown == null || prUp == null");
                }

                if (prDown.inProfile.daProfile.connectionEnd == null)
                {
                    MessageBox.Show("prDown.inProfile.daProfile.connectionEnd == null");
                }

                if (prUp.inProfile.daProfile.connectionStart == null)
                {
                    MessageBox.Show("prUp.inProfile.daProfile.connectionStart == null");
                }

                return new MoCoM2DRight(daConnection, prDown, prUp);
            }

            return null;
        }

        #endregion Create MoCoM2D class

        public MoCoM2DRight(DaConnection daConnection, MoProfile prdown, MoProfile prup) : base(daConnection, prdown, prup)
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

        public override bool HasDiagonalDown()
        {
            return true;
        }

        public override MoProfile GetDiagonalDown()
        {
            return prDown;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalDown()
        {
            if (prDown.inProfile.daProfile.connectionEnd == null)
            {
                throw new Exception("prDown.inProfile.daProfile.connectionEnd == null");
            }

            return prDown.inProfile.daProfile.connectionEnd;
        }

        public override bool HasDiagonalUp()
        {
            return true;
        }

        public override MoProfile GetDiagonalUp()
        {
            return prUp;
        }

        public override DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            if (prUp.inProfile.daProfile.connectionStart == null)
            {
                throw new Exception("prUp.inProfile.daProfile.connectionStart == null");
            }

            return prUp.inProfile.daProfile.connectionStart;
        }

        public override void Create()
        {
            Entities.Add(new VisualSphere(prDown.cpE, MoObject.RadSphere, MoObject.SC_CoM2D));
        }
    }
}
