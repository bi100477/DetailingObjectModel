using DetailingObjectModel.Bracing;
using DetailingObjectModel.Connection.M1D;
using DetailingObjectModel.Connection.M1H;
using DetailingObjectModel.Connection.M1H1D;
using DetailingObjectModel.Connection.M2D;
using DetailingObjectModel.MainLeg;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GLenum = System.UInt32;
using GLuint = System.UInt32;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLchar = System.Byte;
using GLubyte = System.Byte;
using GLfloat = System.Single;
using GLdouble = System.Double;
using GLboolean = System.Boolean;
using GLsizeiptr = System.IntPtr;
using GLintptr = System.IntPtr;
using GLclampf = System.Single;
using GLbitfield = System.UInt32;
using TypesInterface.drawing;

namespace DetailingObjectModel.Connection
{
    public delegate MoConnection CreateMoConnectionClassFromBracingCoupleFunc(MoBracingCouple bracingCouple);
    public delegate MoConnection CreateMoConnectionClassFunc(DaConnection daConnection, MoConnectionType moConnectionType, int classIdentifier, List<MoProfile> profileInput);

    public abstract class MoConnection : MoObject
    {
        #region Create classes

        #region Create MoConnection class

        public static List<CreateMoConnectionClassFromBracingCoupleFunc> createMoConnectionsLeft { get; set; }
        public static List<CreateMoConnectionClassFromBracingCoupleFunc> createMoConnectionsRight { get; set; }
        public static List<CreateMoConnectionClassFunc> createMoConnections { get; set; }

        public static MoConnection CreateMoConnectionClassLeft(MoBracingCouple bracingCouple)
        {
            MoConnection daConnectionClass = null;

            for (int i = 0; i < createMoConnectionsLeft.Count; i++)
            {
                daConnectionClass = createMoConnectionsLeft[i](bracingCouple);

                if (daConnectionClass != null)
                {
                    break;
                }
            }

            return daConnectionClass;
        }

        public static MoConnection CreateMoConnectionClassRight(MoBracingCouple bracingCouple)
        {
            MoConnection daConnectionClass = null;

            for (int i = 0; i < createMoConnectionsRight.Count; i++)
            {
                daConnectionClass = createMoConnectionsRight[i](bracingCouple);

                if (daConnectionClass != null)
                {
                    break;
                }
            }

            return daConnectionClass;
        }

        public static MoConnection CreateMoConnectionClass(DaConnection daConnection, MoConnectionType moConnectionType, int classIdentifier, List<MoProfile> profileInput)
        {
            MoConnection moConnectionClass = null;

            for (int i = 0; i < createMoConnections.Count; i++)
            {
                moConnectionClass = createMoConnections[i](daConnection, moConnectionType, classIdentifier, profileInput);

                if (moConnectionClass != null)
                {
                    break;
                }
            }

            return moConnectionClass;
        }

        #endregion Create MoConnection class

        static MoConnection()
        {
            #region create MoConnectionLeft from bracing couple

            createMoConnectionsLeft = new List<CreateMoConnectionClassFromBracingCoupleFunc>();


            createMoConnectionsLeft.Add(MoCoM1D.CreateMoCoM1DClassLeft);
            createMoConnectionsLeft.Add(MoCoM1H.CreateMoCoM1HClassLeft);
            createMoConnectionsLeft.Add(MoCoM1H1D.CreateMoCoM1H1DClassLeft);
            createMoConnectionsLeft.Add(MoCoM2D.CreateMoCoM2DClassLeft);


            #endregion create MoConnectionLeft from bracing couple


            #region create MoConnectionRight from bracing couple

            createMoConnectionsRight = new List<CreateMoConnectionClassFromBracingCoupleFunc>();

            createMoConnectionsRight.Add(MoCoM1H.CreateMoCoM1HClassRight);
            createMoConnectionsRight.Add(MoCoM1H1D.CreateMoCoM1H1DClassRight);
            createMoConnectionsRight.Add(MoCoM2D.CreateMoCoM2DClassRight);
            createMoConnectionsRight.Add(MoCoM1D.CreateMoCoM1DClassRight);

            #endregion create MoConnectionRight from bracing couple

            #region create MoConnection from type and identifier

            createMoConnections = new List<CreateMoConnectionClassFunc>();

            createMoConnections.Add(MoCoM1H.CreateMoCoM1HClass);
            createMoConnections.Add(MoCoM1H1D.CreateMoCoM1H1DClass);
            createMoConnections.Add(MoCoM2D.CreateMoCoM2DClass);
            createMoConnections.Add(MoCoM1D.CreateMoCoM1DClass);

            #endregion create MoConnection from type and identifier

        }

        #endregion Create classes        

        public DaConnection daConnection { get; set; }
        public MoProfile mainProfileBelow { get; set; }
        public MoProfile mainProfileAbove { get; set; }

        public MoConnection(DaInput dainput) : base(dainput)
        {
            daConnection = (DaConnection)dainput;

            if (daConnection == null)
            {
                throw new Exception("daConnection == null");
            }

            mainProfileBelow = null;
            mainProfileAbove = null;

            PE = new PeConnection(this);
        }

        public override MoObType moObType()
        {
            return MoObType.Connection;
        }

        public abstract MoConnectionType moConnectionType();

        public abstract string Caption();

        public bool HasMainBelow()
        {
            return (mainProfileBelow != null);
        }

        public MoProfile GetMainBelow()
        {
            return mainProfileBelow;
        }

        public bool HasMainAbove()
        {
            return (mainProfileAbove != null);
        }

        public MoProfile GetMainAbove()
        {
            return mainProfileAbove;
        }

        public virtual bool HasHorizontal()
        {
            return false;
        }

        public virtual MoProfile GetHorizontal()
        {
            return null;
        }

        public virtual DaProfileEndConnection GetEndConnectionHorizontal()
        {
            return null;
        }

        public virtual bool HasDiagonalDown()
        {
            return false;
        }

        public virtual MoProfile GetDiagonalDown()
        {
            return null;
        }

        public virtual DaProfileEndConnection GetEndConnectionDiagonalDown()
        {
            return null;
        }

        public virtual bool HasDiagonalUp()
        {
            return false;
        }

        public virtual MoProfile GetDiagonalUp()
        {
            return null;
        }

        public virtual DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            return null;
        }

        public List<MoProfile> GetProfiles()
        {
            List<MoProfile> profiles = new List<MoProfile>();

            if (HasHorizontal() == true)
            {
                profiles.Add(GetHorizontal());
            }

            if (HasDiagonalDown() == true)
            {
                profiles.Add(GetDiagonalDown());
            }

            if (HasDiagonalUp() == true)
            {
                profiles.Add(GetDiagonalUp());
            }

            if (HasMainBelow() == true)
            {
                profiles.Add(GetMainBelow());
            }

            if (HasMainAbove() == true)
            {
                profiles.Add(GetMainAbove());
            }

            return profiles;
        }

        internal static (MoProfile, MoProfile) SetMainProfiles(MoBracingCouple couple, MoMainLegContainer mainLegs)
        {
            MoProfile prBelow = null;
            MoProfile prAbove = null;

            if (couple.brBelow != null)
            {
                prBelow = mainLegs.ProfileAtLevel(couple.daBracingCouple.Level - 5.0);
            }

            if (couple.brAbove != null)
            {
                prAbove = mainLegs.ProfileAtLevel(couple.daBracingCouple.Level + 5.0);
            }

            return (prBelow, prAbove);
        }

        internal static (MoProfile, MoProfile) SetMainProfiles(MoBracing bracing, MoMainLegContainer mainLegs)
        {
            MoProfile prBelow = null;
            MoProfile prAbove = null;

            prBelow = mainLegs.ProfileAtLevel(bracing.BottomLevel() + 5.0);
            prAbove = mainLegs.ProfileAtLevel(bracing.TopLevel() - 5.0);

            return (prBelow, prAbove);
        }

        public override void DrawSelectables()
        {
            foreach (var entity in Entities)
            {
                if (entity.Selectable == true)
                {
                    string objTag = "MoConnection";

                    names.Add((object)entity);
                    names.Add((object)objTag);
                    names.Add((object)this);
                    names.Add((object)PE);

                    GL.PushName((GLuint)indLast++);
                    GL.PushName((GLuint)indLast++);
                    GL.PushName((GLuint)indLast++);
                    GL.PushName((GLuint)indLast++);

                    entity.Draw();

                    GL.PopName();
                    GL.PopName();
                    GL.PopName();
                    GL.PopName();
                }
            }
        }
    }
}
