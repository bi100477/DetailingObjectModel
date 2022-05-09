using DetailingObjectModel.Bracing;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel.Connection.M1H1D
{
    public delegate DaCoM1H1D CreateDaCoM1H1DClassFunc(M1H1DType m1h1dType, DaProfileInput prHor, DaProfileInput prDia);
    public delegate DaCoM1H1D CreateDaCoM1H1DClassFromIdentifierFunc(int classIdentifier, List<DaProfileInput> profileInput);

    public abstract class DaCoM1H1D : DaConnection
    {
        #region Create DaConnection class

        public static DaConnection CreateDaCoM1H1DClassLeft(DaBracingCouple bracingCouple)
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
                            return CreateDaCoM1H1DClass(
                                M1H1DType.LeftDown,
                                below.GetHorizontalTop(),
                                below.GetDiagonalLeftTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateDaCoM1H1DClass(
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
                            return CreateDaCoM1H1DClass(
                                M1H1DType.LeftDown,
                                above.GetHorizontalBottom(),
                                below.GetDiagonalLeftTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateDaCoM1H1DClass(
                                M1H1DType.LeftUp,
                                above.GetHorizontalBottom(),
                                above.GetDiagonalLeftBottom());
                        }
                    }
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1H1DClassRight(DaBracingCouple bracingCouple)
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
                            return CreateDaCoM1H1DClass(
                                M1H1DType.RightDown,
                                below.GetHorizontalTop(),
                                below.GetDiagonalRightTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateDaCoM1H1DClass(
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
                            return CreateDaCoM1H1DClass(
                                M1H1DType.RightDown,
                                above.GetHorizontalBottom(),
                                below.GetDiagonalRightTop());
                        }
                        else if (aboveHasDia == true)
                        {
                            return CreateDaCoM1H1DClass(
                                M1H1DType.RightUp,
                                above.GetHorizontalBottom(),
                                above.GetDiagonalRightBottom());
                        }
                    }
                }
            }

            return null;
        }

        public static DaConnection CreateDaCoM1H1DClass(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput)
        {
            if (daConnectionType == DaConnectionType.M1H1D)
            {
                return CreateDaCoM1H1DClassFromIdentifier(classIdentifier, profileInput);
            }

            return null;
        }

        #endregion Create DaConnection class

        #region Create classes

        #region Create DaCoM1H1D class

        public static List<CreateDaCoM1H1DClassFunc> createDaCoM1H1DFuncs { get; set; }
        public static List<CreateDaCoM1H1DClassFromIdentifierFunc> createDaCoM1H1DFromIdentifierFuncs { get; set; }

        public static DaCoM1H1D CreateDaCoM1H1DClass(M1H1DType m1h1dType, DaProfileInput prHor, DaProfileInput prDia)
        {
            DaCoM1H1D daCoM1H1DClass = null;

            if (prHor == null || prDia == null)
            {
                throw new Exception("prHor == null || prDia == null");
            }

            for (int i = 0; i < createDaCoM1H1DFuncs.Count; i++)
            {
                daCoM1H1DClass = createDaCoM1H1DFuncs[i](m1h1dType, prHor, prDia);

                if (daCoM1H1DClass != null)
                {
                    break;
                }
            }

            return daCoM1H1DClass;
        }

        public static DaCoM1H1D CreateDaCoM1H1DClassFromIdentifier(int classIdentifier, List<DaProfileInput> profileInput)
        {
            DaCoM1H1D daCoM1HClass = null;

            for (int i = 0; i < createDaCoM1H1DFromIdentifierFuncs.Count; i++)
            {
                daCoM1HClass = createDaCoM1H1DFromIdentifierFuncs[i](classIdentifier, profileInput);

                if (daCoM1HClass != null)
                {
                    break;
                }
            }

            return daCoM1HClass;
        }

        #endregion Create DaCoM1H1D class

        static DaCoM1H1D()
        {
            createDaCoM1H1DFuncs = new List<CreateDaCoM1H1DClassFunc>();

            createDaCoM1H1DFuncs.Add(DaCoM1H1DLeftDown.CreateDaCoM1H1DClassLeftDown);
            createDaCoM1H1DFuncs.Add(DaCoM1H1DLeftUp.CreateDaCoM1H1DClassLeftUp);
            createDaCoM1H1DFuncs.Add(DaCoM1H1DRightDown.CreateDaCoM1H1DClassRightDown);
            createDaCoM1H1DFuncs.Add(DaCoM1H1DRightUp.CreateDaCoM1H1DClassRightUp);

            createDaCoM1H1DFromIdentifierFuncs = new List<CreateDaCoM1H1DClassFromIdentifierFunc>();

            createDaCoM1H1DFromIdentifierFuncs.Add(DaCoM1H1DLeftDown.CreateDaCoM1H1DClassFromIdentifierLeftDown);
            createDaCoM1H1DFromIdentifierFuncs.Add(DaCoM1H1DLeftUp.CreateDaCoM1H1DClassFromIdentifierLeftUp);
            createDaCoM1H1DFromIdentifierFuncs.Add(DaCoM1H1DRightDown.CreateDaCoM1H1DClassFromIdentifierRightDown);
            createDaCoM1H1DFromIdentifierFuncs.Add(DaCoM1H1DRightUp.CreateDaCoM1H1DClassFromIdentifierRightUp);
        }

        #endregion Create classes     

        public DaProfileInput prHor { get; set; }
        public DaProfileInput prDia { get; set; }

        public DaCoM1H1D(DaProfileInput prhor, DaProfileInput prdia) : base()
        {
            prHor = prhor;
            prDia = prdia;
        }

        public override DaConnectionType daConnectionType()
        {
            return DaConnectionType.M1H1D;
        }

        public abstract M1H1DType m1h1dType();
    }
}
