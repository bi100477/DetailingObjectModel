using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel
{
    public enum BaObType
    {
        None = -1,
        Data,
        Model,
        Control,
        IO
    }

    public enum DaInType
    {
        None = -1,
        ProfileInput,
        Profile,
        ProfileLayout,
        ProfileType,
        ProfileDetail,
        ProfileEndConnection,
        Bolt,
        BoltLayout,
        BoltDetail,
        BoltType,
        BoltOffset,
        BoltPoint,
        Bracing,
        BracingSystem,
        BracingCenter,
        BracingCouple,
        Connection,
        MainLeg,
        MainLegContainer,
        Plugin
    }

    public enum ProfileLayout
    {
        None = -1,
        Front,
        Back,
        FrontAndBack
    }

    public enum EBoltDetailType
    {
        None = -1,
        Vector,
        Array
    }


    public enum DaBracingType
    {
        None = -1,
        KBracing
    }

    public enum DaKBracingType
    {
        None = -1,
        Left,
        LeftTop,
        LeftBottom,
        LeftAll,
        Right,
        RightBottom,
        RightTop,
        RightAll
    }

    public enum MoBracingType
    {
        None = -1,
        KBracing
    }

    public enum MoKBracingType
    {
        None = -1,
        Left,
        LeftTop,
        LeftBottom,
        LeftAll,
        Right,
        RightBottom,
        RightTop,
        RightAll
    }

    public enum DaCtType
    {
        None = -1,
        ProfileInput,
        Profile,
        ProfileDetail,
        ProfileInputList,
        ProfileCountType,
        ProfileOrientation,
        ProfileOrientationList,
        ProfileOrientationType,
        ProfileLayout,
        ProfileType,
        ProfileDetailType,
        ProfileEndOffsets,
        ProfilePlaneOffsets,
        ProfileBoltLocations,
        Bolt,
        BoltLayout,
        BoltDetail,
        BoltType,
        BoltDetailType,
        BoltOffset,
        BoltPoint,
        DoubleList,
        KBracing,
        DaBracingSystem,
        CoBracingSystem,
        DaBracingCenter,
        CoBracingCenter,
        Connection,
        MainLeg,
        MainLegContainer,
        DaPlugin,
        CoPlugin
    }

    public enum EProfilePlaneOffsetType
    {
        None = -1,
        Front,
        Back,
        FrontAndBack
    }

    public enum DaConnectionType
    {
        None = -1,
        M1H,
        M1D,
        M1H1D,
        M2D
    }

    public enum MoConnectionType
    {
        None = -1,
        M1H,
        M1D,
        M1H1D,
        M2D
    }

    public enum M1HType
    {
        Left = 0,
        Right
    }

    public enum M1H1DType
    {
        LeftDown = 0,
        LeftUp,
        RightDown,
        RightUp
    }

    public enum M2DType
    {
        Left = 0,
        Right
    }

    public enum M1DType
    {
        LeftUp = 0,
        LeftDown,
        RightUp,
        RightDown
    }

    public enum Topology3DType
    {
        None = -1,
        RectangularPrism
    };

    public enum MoObType
    {
        None = -1,
        Profile,
        MainLeg,
        MainLegCenter,
        MainLegContainer,
        Bracing,
        BracingCouple,
        BracingSystem,
        BracingCenter,
        Connection,
        Plugin
    };

    public enum EProfileCount
    {
        None = -1,
        One,
        Two
    }

    public enum EProfileOrientation
    {
        None = -1,
        Front,
        Right,
        Back,
        Left
    }
}
