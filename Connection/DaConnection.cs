using DetailingObjectModel.Bracing;
using DetailingObjectModel.Connection.M1D;
using DetailingObjectModel.Connection.M1H;
using DetailingObjectModel.Connection.M1H1D;
using DetailingObjectModel.Connection.M2D;
using DetailingObjectModel.MainLeg;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Connection
{
    public delegate DaConnection CreateDaConnectionClassFromBracingCoupleFunc(DaBracingCouple bracingCouple);
    public delegate DaConnection CreateDaConnectionClassFunc(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput);

    public abstract class DaConnection : DaInput
    {
        #region Create classes

        #region Create DaConnection class

        public static List<CreateDaConnectionClassFromBracingCoupleFunc> createDaConnectionsLeft { get; set; }
        public static List<CreateDaConnectionClassFromBracingCoupleFunc> createDaConnectionsRight { get; set; }
        public static List<CreateDaConnectionClassFunc> createDaConnections { get; set; }

        public static DaConnection CreateDaConnectionClassLeft(DaBracingCouple bracingCouple)
        {
            DaConnection daConnectionClass = null;

            for (int i = 0; i < createDaConnectionsLeft.Count; i++)
            {
                daConnectionClass = createDaConnectionsLeft[i](bracingCouple);

                if (daConnectionClass != null)
                {
                    break;
                }
            }

            return daConnectionClass;
        }

        public static DaConnection CreateDaConnectionClassRight(DaBracingCouple bracingCouple)
        {
            DaConnection daConnectionClass = null;

            for (int i = 0; i < createDaConnectionsRight.Count; i++)
            {
                daConnectionClass = createDaConnectionsRight[i](bracingCouple);

                if (daConnectionClass != null)
                {
                    break;
                }
            }

            return daConnectionClass;
        }

        public static DaConnection CreateDaConnectionClass(DaConnectionType daConnectionType, int classIdentifier, List<DaProfileInput> profileInput)
        {
            DaConnection daConnectionClass = null;

            for (int i = 0; i < createDaConnections.Count; i++)
            {
                daConnectionClass = createDaConnections[i](daConnectionType, classIdentifier, profileInput);

                if (daConnectionClass != null)
                {
                    break;
                }
            }

            return daConnectionClass;
        }        

        #endregion Create DaConnection class

        static DaConnection()
        {
            #region create DaConnectionLeft from bracing couple

            createDaConnectionsLeft = new List<CreateDaConnectionClassFromBracingCoupleFunc>();
            
            createDaConnectionsLeft.Add(DaCoM1H.CreateDaCoM1HClassLeft);
            createDaConnectionsLeft.Add(DaCoM1H1D.CreateDaCoM1H1DClassLeft);
            createDaConnectionsLeft.Add(DaCoM2D.CreateDaCoM2DClassLeft);
            createDaConnectionsLeft.Add(DaCoM1D.CreateDaCoM1DClassLeft);

            #endregion create DaConnectionLeft from bracing couple


            #region create DaConnectionRight from bracing couple

            createDaConnectionsRight = new List<CreateDaConnectionClassFromBracingCoupleFunc>();

            createDaConnectionsRight.Add(DaCoM1H.CreateDaCoM1HClassRight);
            createDaConnectionsRight.Add(DaCoM1H1D.CreateDaCoM1H1DClassRight);
            createDaConnectionsRight.Add(DaCoM2D.CreateDaCoM2DClassRight);
            createDaConnectionsRight.Add(DaCoM1D.CreateDaCoM1DClassRight);

            #endregion create DaConnectionRight from bracing couple

            #region create DaConnection from type and identifier

            createDaConnections = new List<CreateDaConnectionClassFunc>();

            createDaConnections.Add(DaCoM1H.CreateDaCoM1HClass);
            createDaConnections.Add(DaCoM1H1D.CreateDaCoM1H1DClass);
            createDaConnections.Add(DaCoM2D.CreateDaCoM2DClass);
            createDaConnections.Add(DaCoM1D.CreateDaCoM1DClass);

            #endregion create DaConnection from type and identifier

        }

        #endregion Create classes        

        public DaProfileInput mainProfileBelow { get; set; }
        public DaProfileInput mainProfileAbove { get; set; }

        public DaConnection() : base()
        {
            mainProfileBelow = null;
            mainProfileAbove = null;
        }

        public override DaInType daInType()
        {
            return DaInType.Connection;
        }

        public override bool SetDataFromDialog()
        {
            DiConnection form = new DiConnection(this);

            return (form.ShowDialog() == DialogResult.OK);
        }

        public abstract DaConnectionType daConnectionType();

        public abstract string Caption();

        public abstract int IntIdentifier();

        public bool HasMainBelow()
        {
            return (mainProfileBelow != null);
        }

        public DaProfileInput GetMainBelow()
        {
            return mainProfileBelow;
        }

        public bool HasMainAbove()
        {
            return (mainProfileAbove != null);
        }

        public DaProfileInput GetMainAbove()
        {
            return mainProfileAbove;
        }

        public virtual bool HasHorizontal()
        {
            return false;
        }

        public virtual DaProfileInput GetHorizontal()
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

        public virtual DaProfileInput GetDiagonalDown()
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

        public virtual DaProfileInput GetDiagonalUp()
        {
            return null;
        }

        public virtual DaProfileEndConnection GetEndConnectionDiagonalUp()
        {
            return null;
        }

        public List<DaProfileInput> GetProfiles()
        {
            List<DaProfileInput> profiles = new List<DaProfileInput>();

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

        internal static (DaProfileInput, DaProfileInput) SetMainProfiles(DaBracingCouple couple, DaMainLegContainer mainLegs)
        {
            DaProfileInput prBelow = null;
            DaProfileInput prAbove = null;

            if (couple.brBelow != null)
            {
                prBelow = mainLegs.ProfileAtLevel(couple.Level-5.0);
            }

            if (couple.brAbove != null)
            {
                prAbove = mainLegs.ProfileAtLevel(couple.Level + 5.0);
            }

            return (prBelow, prAbove);
        }

        internal static (DaProfileInput, DaProfileInput) SetMainProfiles(DaBracing bracing, DaMainLegContainer mainLegs)
        {
            DaProfileInput prBelow = null;
            DaProfileInput prAbove = null;

            prBelow = mainLegs.ProfileAtLevel(bracing.BottomLevel() + 5.0);
            prAbove = mainLegs.ProfileAtLevel(bracing.TopLevel() - 5.0);

            return (prBelow, prAbove);
        }
    }
}
