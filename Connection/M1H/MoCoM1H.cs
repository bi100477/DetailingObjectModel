using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DetailingObjectModel.Connection.M1H
{
    public delegate MoCoM1H CreateMoCoM1HClassFunc(DaConnection daConnection, M1HType m1hType, MoProfile profileInput);
    public delegate MoCoM1H CreateMoCoM1HClassFromIdentifierFunc(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput);

    public abstract class MoCoM1H : MoConnection
    {
        #region Create DaConnection class

        public static MoConnection CreateMoCoM1HClassLeft(MoBracingCouple bracingCouple)
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
                return null;
            }
            else
            {
                bool belowHasDia = (below != null) ? below.HasDiagonalLeftTop() : false;
                bool aboveHasDia = (above != null) ? above.HasDiagonalLeftBottom() : false;

                if (belowHasDia == false && aboveHasDia == false)
                {
                    if (belowHasTop == true)
                    {
                        return CreateMoCoM1HClass(bracingCouple.daBracingCouple.connLeft, M1HType.Left, below.GetHorizontalTop());
                    }
                    else if (aboveHasBottom == true)
                    {
                        return CreateMoCoM1HClass(bracingCouple.daBracingCouple.connLeft, M1HType.Left, above.GetHorizontalBottom());
                    }
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1HClassRight(MoBracingCouple bracingCouple)
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
                return null;
            }
            else
            {
                bool belowHasDia = (below != null) ? below.HasDiagonalRightTop() : false;
                bool aboveHasDia = (above != null) ? above.HasDiagonalRightBottom() : false;

                if (belowHasDia == false && aboveHasDia == false)
                {
                    if (belowHasTop == true)
                    {
                        return CreateMoCoM1HClass(bracingCouple.daBracingCouple.connRight, M1HType.Right, below.GetHorizontalTop());
                    }
                    else if (aboveHasBottom == true)
                    {
                        return CreateMoCoM1HClass(bracingCouple.daBracingCouple.connRight, M1HType.Right, above.GetHorizontalBottom());
                    }
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1HClass(DaConnection daConnection, MoConnectionType moConnectionType, int classIdentifier, List<MoProfile> profileInput)
        {
            if (moConnectionType == MoConnectionType.M1H)
            {
                return CreateMoCoM1HClassFromIdentifier(daConnection, classIdentifier, profileInput);
            }

            return null;
        }

        #endregion Create DaConnection class

        #region Create classes

        #region Create MoCoM1H class

        public static List<CreateMoCoM1HClassFunc> createMoCoM1HFuncs { get; set; }
        public static List<CreateMoCoM1HClassFromIdentifierFunc> createMoCoM1HFromIdentifierFuncs { get; set; }

        public static MoCoM1H CreateMoCoM1HClass(DaConnection daConnection, M1HType m1hType, MoProfile profileInput)
        {
            MoCoM1H daCoM1HClass = null;

            for (int i = 0; i < createMoCoM1HFuncs.Count; i++)
            {
                daCoM1HClass = createMoCoM1HFuncs[i](daConnection, m1hType, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        public static MoCoM1H CreateMoCoM1HClassFromIdentifier(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput)
        {
            MoCoM1H daCoM1HClass = null;

            for (int i = 0; i < createMoCoM1HFromIdentifierFuncs.Count; i++)
            {
                daCoM1HClass = createMoCoM1HFromIdentifierFuncs[i](daConnection, classIdentifier, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        #endregion Create MoCoM1H class

        static MoCoM1H()
        {
            createMoCoM1HFuncs = new List<CreateMoCoM1HClassFunc>();

            createMoCoM1HFuncs.Add(MoCoM1HLeft.CreateMoCoM1HClassLeft);
            createMoCoM1HFuncs.Add(MoCoM1HRight.CreateMoCoM1HClassRight);

            createMoCoM1HFromIdentifierFuncs = new List<CreateMoCoM1HClassFromIdentifierFunc>();

            createMoCoM1HFromIdentifierFuncs.Add(MoCoM1HLeft.CreateMoCoM1HClassFromIdentifierLeft);
            createMoCoM1HFromIdentifierFuncs.Add(MoCoM1HRight.CreateMoCoM1HClassFromIdentifierRight);
        }

        #endregion Create classes        

        public MoProfile prHor { get; set; }

        public MoCoM1H(DaInput dainput, MoProfile prhor) : base(dainput)
        {
            prHor = prhor;
        }

        public override MoConnectionType moConnectionType()
        {
            return MoConnectionType.M1H;
        }

        public abstract M1HType m1hType();
    }
}
