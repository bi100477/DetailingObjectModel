using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection.M1H
{
    public class DaCoM1HLeft : DaCoM1H
    {
        public const int classIdentifier = 0;

        #region Create DaCoM1H class

        public static DaCoM1H CreateDaCoM1HClassLeft(M1HType m1hType, DaProfileInput profileInput)
        {
            if (m1hType == M1HType.Left)
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

                return new DaCoM1HLeft(profileInput);
            }

            return null;
        }

        public static DaCoM1H CreateDaCoM1HClassFromIdentifierLeft(int classidentifier, List<DaProfileInput> profileInput)
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


                return new DaCoM1HLeft(profileInput[0]);
            }

            return null;
        }

        #endregion Create DaCoM1H class

        public DaCoM1HLeft(DaProfileInput prhor) : base(prhor)
        {
        }

        public override M1HType m1hType()
        {
            return M1HType.Left;
        }

        public override string Caption()
        {
            return "M1H-Left";
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
            if (prHor.daProfile.connectionStart == null)
            {
                throw new Exception("prHor.daProfile.connectionStart == null");
            }

            return prHor.daProfile.connectionStart;
        }
    }
}
