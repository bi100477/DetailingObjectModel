using DetailingObjectModel.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

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
using System.Windows.Forms;
using TypesInterface.drawing;
using DetailingObjectModel.Connection;
using DetailingObjectModel.MainLeg;
using DetailingObjectModel.Profile;

namespace DetailingObjectModel.Bracing
{
    public class MoBracingSystem : MoObject
    {
        public DaBracingSystem daBracingSystem { get; set; }
        public List<MoBracing> Bracings { get; set; }
        public List<MoBracingCouple> Couples { get; set; }
        public MoMainLegContainer mainLegsLeft { get; set; }
        public MoMainLegContainer mainLegsRight { get; set; }
        public GPolygon3D Polygon { get; set; }

        public MoBracingSystem(DaInput dainput, GPolygon3D polygon, MoMainLegContainer mainlegsleft, MoMainLegContainer mainlegsright) : base(dainput)
        {
            daBracingSystem = (DaBracingSystem)dainput;

            if (daBracingSystem == null)
            {
                throw new Exception("daBracingSystem == null");
            }

            Polygon = polygon;

            mainLegsLeft = mainlegsleft;
            mainLegsRight = mainlegsright;

            Bracings = new List<MoBracing>();
            Couples = new List<MoBracingCouple>();

            PE = new PeBracingSystem(this);
        }

        public override MoObType moObType()
        {
            return MoObType.BracingSystem;
        }

        public override void Create()
        {
            foreach (var item in daBracingSystem.Bracings)
            {
                Bracings.Add(MoBracing.CreateMoBracingClass(item, Polygon));
            }

            foreach (var bracing in Bracings)
            {
                bracing.Create();
            }

            CreateBracingCouples();
            CreateConnections();

            foreach (var bracing in Bracings)
            {
                bracing.CreateConnections();

                Entities.AddRange(bracing.Entities);
                Points.AddRange(bracing.Points);
                Lines.AddRange(bracing.Lines);
            }

            foreach (var couple in Couples)
            {
                couple.Create();

                Entities.AddRange(couple.Entities);
                Points.AddRange(couple.Points);
                Lines.AddRange(couple.Lines);
            }
        }

        public override void DrawSelectables()
        {
            bool sPressed = Convert.ToBoolean(WGL.GetAsyncKeyState(Keys.S) & 0x8000);

            if (sPressed == false)
            {
                foreach (var bracing in Bracings)
                {
                    bracing.DrawSelectables();
                }

                foreach (var couple in Couples)
                {
                    couple.DrawSelectables();
                }
            }
            else
            {
                string objTag = "MoBracingSystem";

                names.Add((object)null);
                names.Add((object)objTag);
                names.Add((object)this);
                names.Add((object)PE);

                GL.PushName((GLuint)indLast++);
                GL.PushName((GLuint)indLast++);
                GL.PushName((GLuint)indLast++);
                GL.PushName((GLuint)indLast++);

                DrawVisibles();

                GL.PopName();
                GL.PopName();
                GL.PopName();
                GL.PopName();
            }
        }

        #region Members

        private void CreateBracingCouples()
        {
            Couples.Clear();

            foreach (DaBracingCouple data in daBracingSystem.Couples)
            {
                Couples.Add(new MoBracingCouple(data, null, null));
            }

            for (int i = 0; i < Bracings.Count; i++)
            {
                Couples[i + 1].brBelow = Bracings[i];
            }

            for (int i = 0; i < Bracings.Count; i++)
            {
                Couples[i].brAbove = Bracings[i];
            }
        }

        private void CreateConnections()
        {
            CreateConnectionsOnCouples();
            CreateConnectionsOnBracings();
        }

        private void CreateConnectionsOnCouples()
        {
            foreach (var couple in Couples)
            {
                MoConnection conLeft = MoConnection.CreateMoConnectionClassLeft(couple);
                MoConnection conRight = MoConnection.CreateMoConnectionClassRight(couple);

                (MoProfile, MoProfile) mainProfilesLeft = MoConnection.SetMainProfiles(couple, mainLegsLeft);
                (MoProfile, MoProfile) mainProfilesRight = MoConnection.SetMainProfiles(couple, mainLegsRight);

                if (conLeft != null)
                {
                    conLeft.mainProfileBelow = mainProfilesLeft.Item1;
                    conLeft.mainProfileAbove = mainProfilesLeft.Item2;

                    couple.connLeft = conLeft;
                }

                if (conRight != null)
                {
                    conRight.mainProfileBelow = mainProfilesRight.Item1;
                    conRight.mainProfileAbove = mainProfilesRight.Item2;

                    couple.connRight = conRight;
                }
            }
        }

        private void CreateConnectionsOnBracings()
        {
            foreach (var bracing in Bracings)
            {
                bracing.CreateConnectionLeft();
                bracing.CreateConnectionRight();

                (MoProfile, MoProfile) mainProfilesLeft = MoConnection.SetMainProfiles(bracing, mainLegsLeft);
                (MoProfile, MoProfile) mainProfilesRight = MoConnection.SetMainProfiles(bracing, mainLegsRight);

                if (bracing.connLeft != null)
                {
                    bracing.connLeft.mainProfileBelow = mainProfilesLeft.Item1;
                    bracing.connLeft.mainProfileAbove = mainProfilesLeft.Item2;
                }

                if (bracing.connRight != null)
                {
                    bracing.connRight.mainProfileBelow = mainProfilesRight.Item1;
                    bracing.connRight.mainProfileAbove = mainProfilesRight.Item2;
                }
            }
        }

        #endregion Members
    }
}
