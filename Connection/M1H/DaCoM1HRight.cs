using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M1H
{
    public class DaCoM1HRight : DaCoM1H
    {
        public const int classIdentifier = 1;

        #region Create DaCoM1H class

        public static DaCoM1H CreateDaCoM1HClassRight(M1HType m1hType, DaProfileInput profileInput)
        {
            if (m1hType == M1HType.Right)
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

                return new DaCoM1HRight(profileInput);
            }

            return null;
        }

        public static DaCoM1H CreateDaCoM1HClassFromIdentifierRight(int classidentifier, List<DaProfileInput> profileInput)
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

                return new DaCoM1HRight(profileInput[0]);
            }

            return null;
        }

        #endregion Create DaCoM1H class

        public DaCoM1HRight(DaProfileInput prhor) : base(prhor)
        {
        }

        public override M1HType m1hType()
        {
            return M1HType.Right;
        }

        public override string Caption()
        {
            return "M1H-Right";
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
                throw new Exception("prHor.daProfile.connectionEnd == null");
            }

            return prHor.daProfile.connectionEnd;
        }
    }
}
