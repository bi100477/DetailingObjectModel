using DetailingObjectModel.Geometry;
using DetailingObjectModel.MainLeg;
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


namespace DetailingObjectModel.Bracing
{
    public class MoBracingCenter : MoObject
    {
        public DaBracingCenter daBracingCenter { get; set; }
        public MoBracingSystem Front { get; set; }
        public MoBracingSystem Back { get; set; }
        public MoBracingSystem Right { get; set; }
        public MoBracingSystem Left { get; set; }
        public MoMainLegCenter mainLegCenter { get; set; }
        public Topology3D topo3D { get; set; }

        public MoBracingCenter(DaInput dainput, Topology3D topo3d) : base(dainput)
        {
            daBracingCenter = (DaBracingCenter)daObject;

            if (daBracingCenter == null)
            {
                throw new Exception("daBracingCenter == null");
            }

            topo3D = topo3d;

            //0 and 1 are reserved for bottom and top polygons

            mainLegCenter = new MoMainLegCenter(daBracingCenter.mainLegs, topo3D);

            Front = new MoBracingSystem(
                daBracingCenter.Front, 
                topo3d.Polygons[2], 
                mainLegCenter.frontLeft, 
                mainLegCenter.frontRight);
            Back = new MoBracingSystem(
                daBracingCenter.Back, 
                topo3d.Polygons[4], 
                mainLegCenter.backLeft, 
                mainLegCenter.backRight);
            Right = new MoBracingSystem(
                daBracingCenter.Right, 
                topo3d.Polygons[3], 
                mainLegCenter.rightLeft, 
                mainLegCenter.rightRight);
            Left = new MoBracingSystem(
                daBracingCenter.Left, 
                topo3d.Polygons[5], 
                mainLegCenter.leftLeft, 
                mainLegCenter.leftRight);

            PE = new PeBracingCenter(this);
        }

        public override MoObType moObType()
        {
            return MoObType.BracingCenter;
        }

        public override void Create()
        {
            mainLegCenter.Create();

            Entities.AddRange(mainLegCenter.Entities);
            Points.AddRange(mainLegCenter.Points);
            Lines.AddRange(mainLegCenter.Lines);

            Front.Create();

            Entities.AddRange(Front.Entities);
            Points.AddRange(Front.Points);
            Lines.AddRange(Front.Lines);

            Back.Create();

            Entities.AddRange(Back.Entities);
            Points.AddRange(Back.Points);
            Lines.AddRange(Back.Lines);

            Right.Create();

            Entities.AddRange(Right.Entities);
            Points.AddRange(Right.Points);
            Lines.AddRange(Right.Lines);

            Left.Create();

            Entities.AddRange(Left.Entities);
            Points.AddRange(Left.Points);
            Lines.AddRange(Left.Lines);
        }

        public override void DrawSelectables()
        {
            bool cPressed = Convert.ToBoolean(WGL.GetAsyncKeyState(Keys.C) & 0x8000);

            if (cPressed == false)
            {
                mainLegCenter.DrawSelectables();
                Front.DrawSelectables();
                Back.DrawSelectables();
                Right.DrawSelectables();
                Left.DrawSelectables();
            }
            else
            {
                string objTag = "MoBracingCenter";

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

        public override void SetVisibles(UInt64 visFlag)
        {
            bool visFront = Convert.ToBoolean(visFlag & MoObject.DF_VIS_FRONT);
            Front.SetVisibles(visFront);

            bool visBack = Convert.ToBoolean(visFlag & MoObject.DF_VIS_BACK);
            Back.SetVisibles(visBack);

            bool visRight = Convert.ToBoolean(visFlag & MoObject.DF_VIS_RIGHT);
            Right.SetVisibles(visRight);

            bool visLeft = Convert.ToBoolean(visFlag & MoObject.DF_VIS_LEFT);
            Left.SetVisibles(visLeft);

            mainLegCenter.SetVisibles(visFlag);
        }

        public override void SetSelectables(UInt64 selFlag)
        {
            bool selFront = Convert.ToBoolean(selFlag & MoObject.DF_SEL_FRONT);
            Front.SetSelectables(selFront);

            bool selBack = Convert.ToBoolean(selFlag & MoObject.DF_SEL_BACK);
            Back.SetSelectables(selBack);

            bool selRight = Convert.ToBoolean(selFlag & MoObject.DF_SEL_RIGHT);
            Right.SetSelectables(selRight);

            bool selLeft = Convert.ToBoolean(selFlag & MoObject.DF_SEL_LEFT);
            Left.SetSelectables(selLeft);

            mainLegCenter.SetSelectables(selFlag);
        }
    }
}
