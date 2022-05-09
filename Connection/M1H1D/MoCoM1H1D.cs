using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M1H1D
{
    public delegate MoCoM1H1D CreateMoCoM1H1DClassFunc(DaConnection daConnection, M1H1DType m1h1dType, MoProfile prHor, MoProfile prDia);
    public delegate MoCoM1H1D CreateMoCoM1H1DClassFromIdentifierFunc(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput);

    public abstract class MoCoM1H1D : MoConnection
    {
        #region Create MoConnection class

        public static MoConnection CreateMoCoM1H1DClassLeft(MoBracingCouple bracingCouple)
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

                if (belowHasDia == true && aboveHasDia == true)
                {
                    throw new Exception("invalid bracing couple!");
                }

                if (belowHasDia == true || aboveHasDia == true)
                {
                    if (belowHasTop == true)
                    {
                        //here one can go one more level down
                        if (belowHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connLeft,
                                M1H1DType.LeftDown,
                                below.GetHorizontalTop(),
                                below.GetDiagonalLeftTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connLeft,
                                M1H1DType.LeftUp,
                                below.GetHorizontalTop(),
                                above.GetDiagonalLeftBottom());
                        }
                    }
                    else if (aboveHasBottom == true)
                    {
                        //here one can go one more level down
                        if (belowHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connLeft,
                                M1H1DType.LeftDown,
                                above.GetHorizontalBottom(),
                                below.GetDiagonalLeftTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connLeft,
                                M1H1DType.LeftUp,
                                above.GetHorizontalBottom(),
                                above.GetDiagonalLeftBottom());
                        }
                    }
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1H1DClassRight(MoBracingCouple bracingCouple)
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

                if (belowHasDia == true && aboveHasDia == true)
                {
                    throw new Exception("invalid bracing couple!");
                }

                if (belowHasDia == true || aboveHasDia == true)
                {
                    //here one can go one more level down
                    //M1H1DRightUp
                    //M1H1DRightDown
                    if (belowHasTop == true)
                    {
                        //here one can go one more level down
                        if (belowHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connRight,
                                M1H1DType.RightDown,
                                below.GetHorizontalTop(),
                                below.GetDiagonalRightTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connRight,
                                M1H1DType.RightUp,
                                below.GetHorizontalTop(),
                                above.GetDiagonalRightBottom());
                        }
                    }
                    else if (aboveHasBottom == true)
                    {
                        //here one can go one more level down
                        if (belowHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connRight,
                                M1H1DType.RightDown,
                                above.GetHorizontalBottom(),
                                below.GetDiagonalRightTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateMoCoM1H1DClass(
                                bracingCouple.daBracingCouple.connRight,
                                M1H1DType.RightUp,
                                above.GetHorizontalBottom(),
                                above.GetDiagonalRightBottom());
                        }
                    }
                }
            }

            return null;
        }

        public static MoConnection CreateMoCoM1H1DClass(DaConnection daConnection, MoConnectionType daConnectionType, int classIdentifier, List<MoProfile> profileInput)
        {
            if (daConnectionType == MoConnectionType.M1H1D)
            {
                return CreateMoCoM1H1DClassFromIdentifier(daConnection, classIdentifier, profileInput);
            }

            return null;
        }

        #endregion Create MoConnection class

        #region Create classes

        #region Create MoCoM1H1D class

        public static List<CreateMoCoM1H1DClassFunc> createMoCoM1H1DFuncs { get; set; }
        public static List<CreateMoCoM1H1DClassFromIdentifierFunc> createMoCoM1H1DFromIdentifierFuncs { get; set; }

        public static MoCoM1H1D CreateMoCoM1H1DClass(DaConnection daConnection, M1H1DType m1h1dType, MoProfile prHor, MoProfile prDia)
        {
            MoCoM1H1D daCoM1H1DClass = null;

            if (prHor == null || prDia == null)
            {
                throw new Exception("prHor == null || prDia == null");
            }

            for (int i = 0; i < createMoCoM1H1DFuncs.Count; i++)
            {
                daCoM1H1DClass = createMoCoM1H1DFuncs[i](daConnection, m1h1dType, prHor, prDia);

                if (daCoM1H1DClass != null)
                {
                    break;
                }
            }

            return daCoM1H1DClass;
        }

        public static MoCoM1H1D CreateMoCoM1H1DClassFromIdentifier(DaConnection daConnection, int classIdentifier, List<MoProfile> profileInput)
        {
            MoCoM1H1D daCoM1HClass = null;

            for (int i = 0; i < createMoCoM1H1DFromIdentifierFuncs.Count; i++)
            {
                daCoM1HClass = createMoCoM1H1DFromIdentifierFuncs[i](daConnection, classIdentifier, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        #endregion Create MoCoM1H1D class

        static MoCoM1H1D()
        {
            createMoCoM1H1DFuncs = new List<CreateMoCoM1H1DClassFunc>();

            createMoCoM1H1DFuncs.Add(MoCoM1H1DLeftDown.CreateMoCoM1H1DClassLeftDown);
            createMoCoM1H1DFuncs.Add(MoCoM1H1DLeftUp.CreateMoCoM1H1DClassLeftUp);
            createMoCoM1H1DFuncs.Add(MoCoM1H1DRightDown.CreateMoCoM1H1DClassRightDown);
            createMoCoM1H1DFuncs.Add(MoCoM1H1DRightUp.CreateMoCoM1H1DClassRightUp);

            createMoCoM1H1DFromIdentifierFuncs = new List<CreateMoCoM1H1DClassFromIdentifierFunc>();

            createMoCoM1H1DFromIdentifierFuncs.Add(MoCoM1H1DLeftDown.CreateMoCoM1H1DClassFromIdentifierLeftDown);
            createMoCoM1H1DFromIdentifierFuncs.Add(MoCoM1H1DLeftUp.CreateMoCoM1H1DClassFromIdentifierLeftUp);
            createMoCoM1H1DFromIdentifierFuncs.Add(MoCoM1H1DRightDown.CreateMoCoM1H1DClassFromIdentifierRightDown);
            createMoCoM1H1DFromIdentifierFuncs.Add(MoCoM1H1DRightUp.CreateMoCoM1H1DClassFromIdentifierRightUp);
        }

        #endregion Create classes     

        public MoProfile prHor { get; set; }
        public MoProfile prDia { get; set; }

        public MoCoM1H1D(DaInput dainput, MoProfile prhor, MoProfile prdia) : base(dainput)
        {
            prHor = prhor;
            prDia = prdia;
        }

        public override MoConnectionType moConnectionType()
        {
            return MoConnectionType.M1H1D;
        }

        public abstract M1H1DType m1h1dType();
    }
}
