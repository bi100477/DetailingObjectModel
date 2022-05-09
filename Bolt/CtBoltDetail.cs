using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public delegate CtBoltDetail CreateCtBoltDetailClassFunc(DaBoltDetail daBoltDetail);

    public abstract class CtBoltDetail : DaControl
    {
        #region Create classes

        #region Create CtBoltDetail class
        public static List<CreateCtBoltDetailClassFunc> createCtBoltDetailClassFuncs { get; set; }

        public static CtBoltDetail CreateCtBoltDetailClass(DaBoltDetail daBoltDetail)
        {
            CtBoltDetail ctBoltDetail = null;

            for (int i = 0; i < createCtBoltDetailClassFuncs.Count; i++)
            {
                ctBoltDetail = createCtBoltDetailClassFuncs[i](daBoltDetail);

                if (ctBoltDetail != null)
                {
                    break;
                }
            }

            return ctBoltDetail;
        }

        #endregion Create CtBoltDetail class

        static CtBoltDetail()
        {
            createCtBoltDetailClassFuncs = new List<CreateCtBoltDetailClassFunc>();

            createCtBoltDetailClassFuncs.Add(CtBoltDetailVector.CreateCtBoltDetailVector);
            createCtBoltDetailClassFuncs.Add(CtBoltDetailArray.CreateCtBoltDetailArray);
        }

        #endregion Create classes

        public DaBoltDetail daBoltDetail { get; set; }

        public CtBoltDetail(DaBoltDetail daboltdetail) : base()
        {
            daBoltDetail = daboltdetail;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.BoltDetail;
        }
    }
}
