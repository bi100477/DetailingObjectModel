using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M2D
{
    public delegate MoCoM2D CreateMoCoM2DClassFunc(DaConnection daConnection, M2DType m2dType, MoProfile prDown, MoProfile prUp);
    public delegate MoCoM2D CreateMoCoM2DClassFromIdentifierFunc(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput);

    public abstract class MoCoM2D : MoConnection
    {
        #region Create MoConnection class

        public static MoConnection CreateMoCoM2DClassLeft(MoBracingCouple bracingCouple)
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
                    return CreateMoCoM2DClass(
                        bracingCouple.daBracingCouple.connLeft,
                        M2DType.Left,
                        below.GetDiagonalLeftTop(),
                        above.GetDiagonalLeftBottom());
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM2DClassRight(MoBracingCouple bracingCouple)
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
                    return CreateMoCoM2DClass(
                        bracingCouple.daBracingCouple.connRight,
                        M2DType.Right,
                        below.GetDiagonalRightTop(),
                        above.GetDiagonalRightBottom());
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM2DClass(DaConnection daConnection, MoConnectionType daConnectionType, int classIdentifier, List<MoProfile> profileInput)
        {
            if (daConnectionType == MoConnectionType.M2D)
            {
                return CreateMoCoM2DClassFromIdentifier(daConnection,classIdentifier, profileInput);
            }

            return null;
        }


        #endregion Create MoConnection class

        #region Create classes

        #region Create MoCoM2D class

        public static List<CreateMoCoM2DClassFunc> createMoCoM2DFuncs { get; set; }
        public static List<CreateMoCoM2DClassFromIdentifierFunc> createMoCoM2DFromIdentifierFuncs { get; set; }

        public static MoCoM2D CreateMoCoM2DClass(DaConnection daConnection, M2DType m2dType, MoProfile prDown, MoProfile prUp)
        {
            MoCoM2D daCoM2DClass = null;

            if (prDown == null || prUp == null)
            {
                throw new Exception("prDown == null || prUp == null");
            }

            for (int i = 0; i < createMoCoM2DFuncs.Count; i++)
            {
                daCoM2DClass = createMoCoM2DFuncs[i](daConnection, m2dType, prDown, prUp);

                if (daCoM2DClass != null)
                {
                    break;
                }
            }

            return daCoM2DClass;
        }

        public static MoCoM2D CreateMoCoM2DClassFromIdentifier(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput)
        {
            MoCoM2D daCoM2DClass = null;

            for (int i = 0; i < createMoCoM2DFromIdentifierFuncs.Count; i++)
            {
                daCoM2DClass = createMoCoM2DFromIdentifierFuncs[i](daConnection, classIdentifier, profileInput);

                if (daCoM2DClass != null)
                {
                    break;
                }
            }

            return daCoM2DClass;
        }

        #endregion Create MoCoM2D class

        static MoCoM2D()
        {
            createMoCoM2DFuncs = new List<CreateMoCoM2DClassFunc>();

            createMoCoM2DFuncs.Add(MoCoM2DLeft.CreateMoCoM2DClassLeft);
            createMoCoM2DFuncs.Add(MoCoM2DRight.CreateMoCoM2DClassRight);

            createMoCoM2DFromIdentifierFuncs = new List<CreateMoCoM2DClassFromIdentifierFunc>();

            createMoCoM2DFromIdentifierFuncs.Add(MoCoM2DLeft.CreateMoCoM2DClassFromIdentifierLeft);
            createMoCoM2DFromIdentifierFuncs.Add(MoCoM2DRight.CreateMoCoM2DClassFromIdentifierRight);
        }

        #endregion Create classes        

        public MoProfile prDown { get; set; }
        public MoProfile prUp { get; set; }

        public MoCoM2D(DaInput dainput, MoProfile prdown, MoProfile prup) : base(dainput)
        {
            prDown = prdown;
            prUp = prup;
        }

        public override MoConnectionType moConnectionType()
        {
            return MoConnectionType.M2D;
        }

        public abstract M2DType m2dType();
    }
}
