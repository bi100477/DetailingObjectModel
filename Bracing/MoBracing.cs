using DetailingObjectModel.Geometry;
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
using System.Windows.Forms;
using DetailingObjectModel.Connection;

namespace DetailingObjectModel.Bracing
{
    public delegate MoBracing CreateMoBracingClassFunc(DaInput dainput, GPolygon3D polygon);

    public abstract class MoBracing : MoObject
    {
        #region Create classes

        #region Create MoBracing class

        private static List<CreateMoBracingClassFunc> createMoBracingFunctions { get; set; }

        public static MoBracing CreateMoBracingClass(DaInput dainput, GPolygon3D polygon)
        {
            MoBracing moBracingClass = null;

            for (int i = 0; i < createMoBracingFunctions.Count; i++)
            {
                moBracingClass = createMoBracingFunctions[i](dainput, polygon);

                if (moBracingClass != null)
                {
                    break;
                }
            }

            return moBracingClass;
        }

        #endregion Create MoBracing class

        static MoBracing()
        {
            createMoBracingFunctions = new List<CreateMoBracingClassFunc>();

            createMoBracingFunctions.Add(MoKBracingLeft.CreateMoKBracingLeft);
            createMoBracingFunctions.Add(MoKBracingLeftAll.CreateMoKBracingLeftAll);
            createMoBracingFunctions.Add(MoKBracingLeftBottom.CreateMoKBracingLeftBottom);
            createMoBracingFunctions.Add(MoKBracingLeftTop.CreateMoKBracingLeftTop);
            createMoBracingFunctions.Add(MoKBracingRight.CreateMoKBracingRight);
            createMoBracingFunctions.Add(MoKBracingRightAll.CreateMoKBracingRightAll);
            createMoBracingFunctions.Add(MoKBracingRightBottom.CreateMoKBracingRightBottom);
            createMoBracingFunctions.Add(MoKBracingRightTop.CreateMoKBracingRightTop);
        }

        #endregion Create classes

        public DaBracing daBracing { get; set; }
        public GPolygon3D Polygon { get; set; }
        public MoConnection connLeft { get; set; }
        public MoConnection connRight { get; set; }

        protected MoBracing(DaInput dainput, GPolygon3D polygon) : base(dainput)
        {
            daBracing = (DaBracing)dainput;

            if (daBracing == null)
            {
                throw new Exception("daBracing == null");
            }

            Polygon = polygon;

            connLeft = null;
            connRight = null;

            PE = new PeBracing(this);
        }

        public override MoObType moObType()
        {
            return MoObType.Bracing;
        }

        public override void DrawSelectables()
        {
            bool bPressed = Convert.ToBoolean(WGL.GetAsyncKeyState(Keys.B) & 0x8000);

            if (bPressed == false)
            {
                List<MoProfile> profiles = GetProfiles();

                foreach (var profile in profiles)
                {
                    profile.DrawSelectables();
                }

                if (connLeft != null)
                {
                    connLeft.DrawSelectables();
                }

                if (connRight != null)
                {
                    connRight.DrawSelectables();
                }
            }
            else
            {
                string objTag = daBracing.Caption();

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

        public void CreateConnections()
        {
            if (connLeft != null)
            {
                connLeft.Create();

                Entities.AddRange(connLeft.Entities);
                Points.AddRange(connLeft.Points);
                Lines.AddRange(connLeft.Lines);
            }

            if (connRight != null)
            {
                connRight.Create();

                Entities.AddRange(connRight.Entities);
                Points.AddRange(connRight.Points);
                Lines.AddRange(connRight.Lines);
            }
        }

        #region Interface MoBracing

        public abstract MoBracingType moBracingType();

        public abstract string Caption();

        public abstract int IntIdentifier();

        public abstract List<MoProfile> GetProfiles();

        public bool HasHorizontalBottom()
        {
            return daBracing.HasHorizontalBottom();
        }

        public bool HasHorizontalTop()
        {
            return daBracing.HasHorizontalTop();
        }

        public bool HasDiagonalLeftBottom()
        {
            return daBracing.HasDiagonalLeftBottom();
        }

        public bool HasDiagonalLeftTop()
        {
            return daBracing.HasDiagonalLeftTop();
        }

        public bool HasDiagonalRightBottom()
        {
            return daBracing.HasDiagonalRightBottom();
        }

        public bool HasDiagonalRightTop()
        {
            return daBracing.HasDiagonalRightTop();
        }

        public abstract MoProfile GetHorizontalBottom();

        public abstract MoProfile GetHorizontalTop();

        public abstract MoProfile GetDiagonalLeftBottom();

        public abstract MoProfile GetDiagonalLeftTop();

        public abstract MoProfile GetDiagonalRightBottom();

        public abstract MoProfile GetDiagonalRightTop();

        public double BottomLevel()
        {
            return daBracing.BottomLevel();
        }

        public double TopLevel()
        {
            return daBracing.TopLevel();
        }

        public abstract void CreateConnectionLeft();

        public abstract void CreateConnectionRight();


        #endregion Interface MoBracing
    }
}
