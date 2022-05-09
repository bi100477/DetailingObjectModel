using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;

namespace DetailingObjectModel.Connection.M1D
{
    public delegate DaCoM1D CreateDaCoM1DClassFunc(M1DType m1dType, DaProfileInput profileInput);
    public delegate DaCoM1D CreateDaCoM1DClassFromIdentifierFunc(int classIdentifier, List<DaProfileInput> profileInput);

    public abstract class DaCoM1D : DaConnection
    {
        #region Create DaConnection class

        public static DaConnection CreateDaCoM1DClassLeft(DaBracingCouple bracingCouple)
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
                    return null;
                }

                if (belowHasDia == false && aboveHasDia == false)
                {
                    return null;
                }

                if (belowHasDia == true)
                {
                    return CreateDaCoM1DClass(M1DType.LeftDown, below.GetDiagonalLeftTop());
                }
                else if (aboveHasDia == true)
                {
                    return CreateDaCoM1DClass(M1DType.LeftUp, above.GetDiagonalLeftBottom());
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1DClassRight(DaBracingCouple bracingCouple)
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
                    return null;
                }

                if (belowHasDia == false && aboveHasDia == false)
                {
                    return null;
                }

                if (belowHasDia == true)
                {
                    return CreateDaCoM1DClass(M1DType.RightDown, below.GetDiagonalRightTop());
                }
                else if (aboveHasDia == true)
                {
                    return CreateDaCoM1DClass(M1DType.RightUp, above.GetDiagonalRightBottom());
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1DClass(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput)
        {
            if (daConnectionType == DaConnectionType.M1D)
            {
                return CreateDaCoM1DClassFromIdentifier(classIdentifier, profileInput);
            }
            

            return null;
        }

        #endregion Create DaConnection class

        #region Create classes

        #region Create DaCoM1D class

        public static List<CreateDaCoM1DClassFunc> createDaCoM1DFuncs { get; set; }
        public static List<CreateDaCoM1DClassFromIdentifierFunc> createDaCoM1DFromIdentifierFuncs { get; set; }

        public static DaCoM1D CreateDaCoM1DClass(M1DType m1dType, DaProfileInput profileInput)
        {
            DaCoM1D daCoM1DClass = null;

            for (int i = 0; i < createDaCoM1DFuncs.Count; i++)
            {
                daCoM1DClass = createDaCoM1DFuncs[i](m1dType, profileInput);

                if (daCoM1DClass != null)
                {
                    break;
                }
            }

            return daCoM1DClass;
        }

        public static DaCoM1D CreateDaCoM1DClassFromIdentifier(int classIdentifier, List<DaProfileInput> profileInput)
        {
            DaCoM1D daCoM1DClass = null;

            for (int i = 0; i < createDaCoM1DFromIdentifierFuncs.Count; i++)
            {
                daCoM1DClass = createDaCoM1DFromIdentifierFuncs[i](classIdentifier, profileInput);

                if (daCoM1DClass != null)
                {
                    break;
                }
            }

            return daCoM1DClass;
        }

        #endregion Create DaCoM1D class

        static DaCoM1D()
        {
            createDaCoM1DFuncs = new List<CreateDaCoM1DClassFunc>();

            createDaCoM1DFuncs.Add(DaCoM1DLeftDown.CreateDaCoM1DClassLeftDown);
            createDaCoM1DFuncs.Add(DaCoM1DLeftUp.CreateDaCoM1DClassLeftUp);
            createDaCoM1DFuncs.Add(DaCoM1DRightDown.CreateDaCoM1DClassRightDown);
            createDaCoM1DFuncs.Add(DaCoM1DRightUp.CreateDaCoM1DClassRightUp);

            createDaCoM1DFromIdentifierFuncs = new List<CreateDaCoM1DClassFromIdentifierFunc>();

            createDaCoM1DFromIdentifierFuncs.Add(DaCoM1DLeftDown.CreateDaCoM1DClassFromIdentifierLeftDown);
            createDaCoM1DFromIdentifierFuncs.Add(DaCoM1DLeftUp.CreateDaCoM1DClassFromIdentifierLeftUp);
            createDaCoM1DFromIdentifierFuncs.Add(DaCoM1DRightDown.CreateDaCoM1DClassFromIdentifierRightDown);
            createDaCoM1DFromIdentifierFuncs.Add(DaCoM1DRightUp.CreateDaCoM1DClassFromIdentifierRightUp);
        }

        #endregion Create classes        

        public DaProfileInput prDia { get; set; }

        public DaCoM1D(DaProfileInput prdia) : base()
        {
            prDia = prdia;
        }

        public override DaConnectionType daConnectionType()
        {
            return DaConnectionType.M1D;
        }

        public abstract M1DType m1dType();
    }
}
