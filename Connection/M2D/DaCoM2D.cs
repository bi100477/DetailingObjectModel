using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M2D
{
    public delegate DaCoM2D CreateDaCoM2DClassFunc(M2DType m2dType, DaProfileInput prDown, DaProfileInput prUp);
    public delegate DaCoM2D CreateDaCoM2DClassFromIdentifierFunc(int classIdentifier, List<DaProfileInput> profileInput);

    public abstract class DaCoM2D : DaConnection
    {
        #region Create DaConnection class

        public static DaConnection CreateDaCoM2DClassLeft(DaBracingCouple bracingCouple)
        {
            DaBracing below = bracingCouple.brBelow;
            DaBracing above = bracingCouple.brAbove;

            bool belowHasTop = (below != null) ? below.HasHorizontalTop() : false;
            bool aboveHasBottom = (above != null) ? above.HasHorizontalBottom() : false;

            if (belowHasTop && aboveHasBottom)
            {
                throw new Exception("invalid bracing couple!");
            }

            if (belowHasTop == false && aboveHasBottom == false)
            {
                bool belowHasDia = (below != null) ? below.HasDiagonalLeftTop() : false;
                bool aboveHasDia = (above != null) ? above.HasDiagonalLeftBottom() : false;

                if (belowHasDia == true && aboveHasDia == true)
                {
                    return CreateDaCoM2DClass(
                        M2DType.Left, 
                        below.GetDiagonalLeftTop(), 
                        above.GetDiagonalLeftBottom());
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM2DClassRight(DaBracingCouple bracingCouple)
        {
            DaBracing below = bracingCouple.brBelow;
            DaBracing above = bracingCouple.brAbove;

            bool belowHasTop = (below != null) ? below.HasHorizontalTop() : false;
            bool aboveHasBottom = (above != null) ? above.HasHorizontalBottom() : false;

            if (belowHasTop && aboveHasBottom)
            {
                throw new Exception("invalid bracing couple!");
            }

            if (belowHasTop == false && aboveHasBottom == false)
            {
                bool belowHasDia = (below != null) ? below.HasDiagonalRightTop() : false;
                bool aboveHasDia = (above != null) ? above.HasDiagonalRightBottom() : false;

                if (belowHasDia == true && aboveHasDia == true)
                {
                    return CreateDaCoM2DClass(
                        M2DType.Right,
                        below.GetDiagonalRightTop(),
                        above.GetDiagonalRightBottom());
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM2DClass(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput)
        {
            if (daConnectionType == DaConnectionType.M2D)
            {
                return CreateDaCoM2DClassFromIdentifier(classIdentifier, profileInput);
            }

            return null;
        }


        #endregion Create DaConnection class

        #region Create classes

        #region Create DaCoM2D class

        public static List<CreateDaCoM2DClassFunc> createDaCoM2DFuncs { get; set; }
        public static List<CreateDaCoM2DClassFromIdentifierFunc> createDaCoM2DFromIdentifierFuncs { get; set; }

        public static DaCoM2D CreateDaCoM2DClass(M2DType m2dType, DaProfileInput prDown, DaProfileInput prUp)
        {
            DaCoM2D daCoM2DClass = null;

            if (prDown == null || prUp == null)
            {
                throw new Exception("prDown == null || prUp == null");
            }               

            for (int i = 0; i < createDaCoM2DFuncs.Count; i++)
            {
                daCoM2DClass = createDaCoM2DFuncs[i](m2dType, prDown, prUp);

                if (daCoM2DClass != null)
                {
                    break;
                }
            }

            return daCoM2DClass;
        }

        public static DaCoM2D CreateDaCoM2DClassFromIdentifier(int classIdentifier, List<DaProfileInput> profileInput)
        {
            DaCoM2D daCoM2DClass = null;

            for (int i = 0; i < createDaCoM2DFromIdentifierFuncs.Count; i++)
            {
                daCoM2DClass = createDaCoM2DFromIdentifierFuncs[i](classIdentifier, profileInput);

                if (daCoM2DClass != null)
                {
                    break;
                }
            }

            return daCoM2DClass;
        }

        #endregion Create DaCoM2D class

        static DaCoM2D()
        {
            createDaCoM2DFuncs = new List<CreateDaCoM2DClassFunc>();

            createDaCoM2DFuncs.Add(DaCoM2DLeft.CreateDaCoM2DClassLeft);
            createDaCoM2DFuncs.Add(DaCoM2DRight.CreateDaCoM2DClassRight);

            createDaCoM2DFromIdentifierFuncs = new List<CreateDaCoM2DClassFromIdentifierFunc>();

            createDaCoM2DFromIdentifierFuncs.Add(DaCoM2DLeft.CreateDaCoM2DClassFromIdentifierLeft);            
            createDaCoM2DFromIdentifierFuncs.Add(DaCoM2DRight.CreateDaCoM2DClassFromIdentifierRight);            
        }

        #endregion Create classes        

        public DaProfileInput prDown { get; set; }
        public DaProfileInput prUp { get; set; }

        public DaCoM2D(DaProfileInput prdown, DaProfileInput prup) : base()
        {
            prDown = prdown;
            prUp = prup;
        }

        public override DaConnectionType daConnectionType()
        {
            return DaConnectionType.M2D;
        }

        public abstract M2DType m2dType();
    }
}
