using DetailingObjectModel.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.drawing;
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
using DetailingObjectModel.Profile;
using System.Windows.Forms;


namespace DetailingObjectModel.MainLeg
{
    public class MoMainLeg : MoObject
    {
        public DaMainLeg daMainLeg { get; set; }
        public GSegment3D Segment { get; set; }
        public MoProfile moProfile { get; set; }

        public MoMainLeg(DaInput dainput, GSegment3D seg) : base(dainput)
        {
            daMainLeg = (DaMainLeg)dainput;

            if (daMainLeg == null)
            {
                throw new Exception("daMainLeg == null");
            }

            Segment = seg;
            moProfile = null;

            PE = new PeMainLeg(this);
        }

        public override MoObType moObType()
        {
            return MoObType.MainLeg;
        }

        public override void Create()
        {
            (GVector3D, GVector3D) points = CreateRefPoints();
            moProfile = new MoProfile(daMainLeg.profileCouple, points.Item1, points.Item2);
            moProfile.Create();

            Entities = moProfile.Entities;
            Points = moProfile.Points;
            Lines = moProfile.Lines;
        }

        public override void DrawSelectables()
        {
            bool mPressed = Convert.ToBoolean(WGL.GetAsyncKeyState(Keys.M) & 0x8000);

            if (mPressed == false)
            {
                moProfile.DrawSelectables();
            }
            else
            {
                string objTag = "MoMainLeg";

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

        private (GVector3D, GVector3D) CreateRefPoints()
        {
            GPlane3D plBottom = new GPlane3D(new GVector3D(0.0, 0.0, daMainLeg.Bottom), GVector3D.UnitZ());
            GPlane3D plTop = new GPlane3D(new GVector3D(0.0, 0.0, daMainLeg.Top), GVector3D.UnitZ());

            GVector3D ptBottom = plBottom.IntersectWithSegment(Segment);
            GVector3D ptTop = plTop.IntersectWithSegment(Segment);

            if (ptBottom == null || ptTop == null)
            {
                throw new Exception("ptBottom == null || ptTop == null");
            }

            return (ptBottom, ptTop);
        }

        #endregion Members
    }
}
