using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public class CtBoltDetailArray : CtBoltDetail
    {
        #region Create CtBoltDetail class

        public static CtBoltDetail CreateCtBoltDetailArray(DaBoltDetail daBoltDetail)
        {
            CtBoltDetail ctBoltDetail = null;

            if (daBoltDetail.boltDetailType() == EBoltDetailType.Array)
            {
                ctBoltDetail = new CtBoltDetailArray(daBoltDetail);
            }

            return ctBoltDetail;
        }

        #endregion Create DaBoltDetail class


        public DaBoltDetailArray daBoltDetailArray { get; set; }
        public CtBoltPoint ctBoltPoint { get; set; }

        public CtBoltDetailArray(DaBoltDetail daboltdetail) : base(daboltdetail)
        {
            daBoltDetailArray = (DaBoltDetailArray)daBoltDetail;

            if (daBoltDetailArray == null)
            {
                throw new Exception("daBoltDetailArray == null");
            }
        }

        public override void Create(Control parent, int l, int t)
        {
            ctBoltPoint = new CtBoltPoint(daBoltDetailArray.boltPoints);
            ctBoltPoint.Create(parent, l, t);
        }

        public override bool Check()
        {
            if (ctBoltPoint.Check() == false)
            {
                failedControl = ctBoltPoint.failedControl;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            ctBoltPoint.Get();
        }

        public override void Set()
        {
            ctBoltPoint.Set();
        }

        public override void Refresh()
        {
            daBoltDetailArray = (DaBoltDetailArray)daBoltDetail;

            if (daBoltDetailArray == null)
            {
                throw new Exception("daBoltDetailArray == null");
            }

            ctBoltPoint.boltPoints = daBoltDetailArray.boltPoints;
        }
    }
}
