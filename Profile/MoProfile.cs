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


namespace DetailingObjectModel.Profile
{
    public class MoProfile : MoObject
    {
        public DaProfileInput inProfile { get; set; }
        public GVector3D cpS { get; set; }
        public GVector3D cpE { get; set; }
        public VisualPoint vpS { get; set; }
        public VisualPoint vpE { get; set; }
        public VisualLine vlSE { get; set; }
        public GVector3D Unit { get; set; }

        public MoProfile(DaInput daprofileinput, GVector3D cps, GVector3D cpe) : base(daprofileinput)
        {
            inProfile = (DaProfileInput)daprofileinput;

            if (inProfile == null)
            {
                throw new Exception("inProfile == null");
            }

            cpS = cps;
            cpE = cpe;

            Unit = (cpE - cpS).Normal();

            vpS = null;
            vpE = null;
            vlSE = null;

            PE = new PeProfile(this);
        }

        public override MoObType moObType()
        {
            return MoObType.Profile;
        }

        public override void Create()
        {
            int PointSize = MoObject.PointSize;
            Color PointColor = MoObject.PointColor;

            vpS = new VisualPoint(cpS, PointColor, PointSize);

            Points.Add(vpS);
            Entities.Add(vpS);

            vpE = new VisualPoint(cpE, PointColor, PointSize);

            Points.Add(vpE);
            Entities.Add(vpE);

            int LineWidth = MoObject.LineWidth;
            Color LineColor = MoObject.LineColor;

            vlSE = new VisualLine(vpS, vpE, LineColor, LineWidth);

            Lines.Add(vlSE);
            Entities.Add(vlSE);
        }

        public override void DrawSelectables()
        {
            #region draw selectable points

            if (entityType == DrawingFlags.SF_ENTITY_ALL || entityType == DrawingFlags.SF_ENTITY_ANY_POINT)
            {
                foreach (var point in Points)
                {
                    if (point.Selectable == true)
                    {
                        string objTag = "MoProfile";

                        names.Add((object)point);
                        names.Add((object)objTag);
                        names.Add((object)this);
                        names.Add((object)PE);

                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);

                        point.Draw();

                        GL.PopName();
                        GL.PopName();
                        GL.PopName();
                        GL.PopName();
                    }
                }
            }

            #endregion draw selectable points

            #region draw selectable lines

            if (entityType == DrawingFlags.SF_ENTITY_ALL || entityType == DrawingFlags.SF_ENTITY_ANY_LINE)
            {
                foreach (var line in Lines)
                {
                    if (line.Selectable == true)
                    {
                        string objTag = "MoProfile";

                        names.Add((object)line);
                        names.Add((object)objTag);
                        names.Add((object)this);
                        names.Add((object)PE);

                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);
                        GL.PushName((GLuint)indLast++);

                        line.Draw();

                        GL.PopName();
                        GL.PopName();
                        GL.PopName();
                        GL.PopName();
                    }
                }
            }

            #endregion draw selectable lines
        }
    }
}
