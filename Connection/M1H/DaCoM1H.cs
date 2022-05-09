using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M1H
{
    public delegate DaCoM1H CreateDaCoM1HClassFunc(M1HType m1hType, DaProfileInput profileInput);
    public delegate DaCoM1H CreateDaCoM1HClassFromIdentifierFunc(int classIdentifier, List<DaProfileInput> profileInput);

    public abstract class DaCoM1H : DaConnection
    {
        #region Create DaConnection class

        public static DaConnection CreateDaCoM1HClassLeft(DaBracingCouple bracingCouple)
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
                        return CreateDaCoM1HClass(M1HType.Left, below.GetHorizontalTop());
                    }
                    else if (aboveHasBottom == true)
                    {
                        return CreateDaCoM1HClass(M1HType.Left, above.GetHorizontalBottom());
                    }
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1HClassRight(DaBracingCouple bracingCouple)
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
                        return CreateDaCoM1HClass(M1HType.Right, below.GetHorizontalTop());
                    }
                    else if (aboveHasBottom == true)
                    {
                        return CreateDaCoM1HClass(M1HType.Right, above.GetHorizontalBottom());
                    }
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1HClass(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput)
        {
            if (daConnectionType == DaConnectionType.M1H)
            {
                return CreateDaCoM1HClassFromIdentifier(classIdentifier, profileInput);
            }

            return null;
        }

        #endregion Create DaConnection class

        #region Create classes

        #region Create DaCoM1H class

        public static List<CreateDaCoM1HClassFunc> createDaCoM1HFuncs { get; set; }
        public static List<CreateDaCoM1HClassFromIdentifierFunc> createDaCoM1HFromIdentifierFuncs { get; set; }

        public static DaCoM1H CreateDaCoM1HClass(M1HType m1hType, DaProfileInput profileInput)
        {
            DaCoM1H daCoM1HClass = null;

            for (int i = 0; i < createDaCoM1HFuncs.Count; i++)
            {
                daCoM1HClass = createDaCoM1HFuncs[i](m1hType, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        public static DaCoM1H CreateDaCoM1HClassFromIdentifier(int classIdentifier, List<DaProfileInput> profileInput)
        {
            DaCoM1H daCoM1HClass = null;

            for (int i = 0; i < createDaCoM1HFromIdentifierFuncs.Count; i++)
            {
                daCoM1HClass = createDaCoM1HFromIdentifierFuncs[i](classIdentifier, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        #endregion Create DaCoM1H class

        static DaCoM1H()
        {
            createDaCoM1HFuncs = new List<CreateDaCoM1HClassFunc>();

            createDaCoM1HFuncs.Add(DaCoM1HLeft.CreateDaCoM1HClassLeft);
            createDaCoM1HFuncs.Add(DaCoM1HRight.CreateDaCoM1HClassRight);

            createDaCoM1HFromIdentifierFuncs = new List<CreateDaCoM1HClassFromIdentifierFunc>();

            createDaCoM1HFromIdentifierFuncs.Add(DaCoM1HLeft.CreateDaCoM1HClassFromIdentifierLeft);
            createDaCoM1HFromIdentifierFuncs.Add(DaCoM1HRight.CreateDaCoM1HClassFromIdentifierRight);
        }

        #endregion Create classes        

        public DaProfileInput prHor { get; set; }

        public DaCoM1H(DaProfileInput prhor) : base()
        {
            prHor = prhor;
        }

        public override DaConnectionType daConnectionType()
        {
            return DaConnectionType.M1H;
        }

        public abstract M1HType m1hType();
    }
}
