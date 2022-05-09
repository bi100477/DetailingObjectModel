using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M1D
{
    public delegate MoCoM1D CreateMoCoM1DClassFunc(DaConnection daConnection, M1DType m1dType, MoProfile profileInput);
    public delegate MoCoM1D CreateMoCoM1DClassFromIdentifierFunc(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput);

    public abstract class MoCoM1D : MoConnection
    {
        #region Create MoConnection class

        public static MoConnection CreateMoCoM1DClassLeft(MoBracingCouple bracingCouple)
        {
            MoBracing below = bracingCouple.brBelow;
            MoBracing above = bracingCouple.brAbove;

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
                    return CreateMoCoM1DClass(bracingCouple.daBracingCouple.connLeft, M1DType.LeftDown, below.GetDiagonalLeftTop());
                }
                else if (aboveHasDia == true)
                {
                    return CreateMoCoM1DClass(bracingCouple.daBracingCouple.connLeft, M1DType.LeftUp, above.GetDiagonalLeftBottom());
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1DClassRight(MoBracingCouple bracingCouple)
        {
            MoBracing below = bracingCouple.brBelow;
            MoBracing above = bracingCouple.brAbove;

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
                    return CreateMoCoM1DClass(bracingCouple.daBracingCouple.connRight, M1DType.RightDown, below.GetDiagonalRightTop());
                }
                else if (aboveHasDia == true)
                {
                    return CreateMoCoM1DClass(bracingCouple.daBracingCouple.connRight, M1DType.RightUp, above.GetDiagonalRightBottom());
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1DClass(DaConnection daConnection, MoConnectionType moConnectionType, int classIdentifier, List<MoProfile> profileInput)
        {
            if (moConnectionType == MoConnectionType.M1D)
            {
                return CreateMoCoM1DClassFromIdentifier(daConnection, classIdentifier, profileInput);
            }

            return null;
        }

        #endregion Create MoConnection class

        #region Create classes

        #region Create MoCoM1D class

        public static List<CreateMoCoM1DClassFunc> createMoCoM1DFuncs { get; set; }
        public static List<CreateMoCoM1DClassFromIdentifierFunc> createMoCoM1DFromIdentifierFuncs { get; set; }

        public static MoCoM1D CreateMoCoM1DClass(DaConnection daConnection, M1DType m1dType, MoProfile profileInput)
        {
            MoCoM1D moCoM1DClass = null;

            for (int i = 0; i < createMoCoM1DFuncs.Count; i++)
            {
                moCoM1DClass = createMoCoM1DFuncs[i](daConnection, m1dType, profileInput);

                if (moCoM1DClass != null)
                {
                    break;
                }
            }

            return moCoM1DClass;
        }

        public static MoCoM1D CreateMoCoM1DClassFromIdentifier(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput)
        {
            MoCoM1D moCoM1DClass = null;

            for (int i = 0; i < createMoCoM1DFromIdentifierFuncs.Count; i++)
            {
                moCoM1DClass = createMoCoM1DFromIdentifierFuncs[i](daConnection, classIdentifier, profileInput);

                if (moCoM1DClass != null)
                {
                    break;
                }
            }

            return moCoM1DClass;
        }

        #endregion Create MoCoM1D class

        static MoCoM1D()
        {
            createMoCoM1DFuncs = new List<CreateMoCoM1DClassFunc>();

            createMoCoM1DFuncs.Add(MoCoM1DLeftDown.CreateMoCoM1DClassLeftDown);
            createMoCoM1DFuncs.Add(MoCoM1DLeftUp.CreateMoCoM1DClassLeftUp);
            createMoCoM1DFuncs.Add(MoCoM1DRightDown.CreateMoCoM1DClassRightDown);
            createMoCoM1DFuncs.Add(MoCoM1DRightUp.CreateMoCoM1DClassRightUp);

            createMoCoM1DFromIdentifierFuncs = new List<CreateMoCoM1DClassFromIdentifierFunc>();

            createMoCoM1DFromIdentifierFuncs.Add(MoCoM1DLeftDown.CreateMoCoM1DClassFromIdentifierLeftDown);
            createMoCoM1DFromIdentifierFuncs.Add(MoCoM1DLeftUp.CreateMoCoM1DClassFromIdentifierLeftUp);
            createMoCoM1DFromIdentifierFuncs.Add(MoCoM1DRightDown.CreateMoCoM1DClassFromIdentifierRightDown);
            createMoCoM1DFromIdentifierFuncs.Add(MoCoM1DRightUp.CreateMoCoM1DClassFromIdentifierRightUp);
        }

        #endregion Create classes        

        public MoProfile prDia { get; set; }

        public MoCoM1D(DaInput dainput, MoProfile prdia) : base(dainput)
        {
            prDia = prdia;
        }

        public override MoConnectionType moConnectionType()
        {
            return MoConnectionType.M1D;
        }

        public abstract M1DType m1dType();
    }
}