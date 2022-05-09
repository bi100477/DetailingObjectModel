using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace DetailingObjectModel
{
    public abstract class MoObject : BaObject
    {
        #region Static params

        #region Selection
        public static uint entityType { get; set; }
        public static List<object> names { get; set; }
        public static int indLast { get; set; }

        #endregion Selection

        #region Filter

        public const uint DF_ALL_BITS_OPEN = 4294967295;

        //public static uint DF_VIS_EF;

        public static uint DF_VIS_FRONT;
        public static uint DF_VIS_BACK;
        public static uint DF_VIS_RIGHT;
        public static uint DF_VIS_LEFT;

        //public static uint DF_SEL_EF;

        public static uint DF_SEL_FRONT;
        public static uint DF_SEL_BACK;
        public static uint DF_SEL_RIGHT;
        public static uint DF_SEL_LEFT;

        #endregion Filter

        #region Drawing

        public static int PointSize;
        public static int LineWidth;
        public static Color PointColor;
        public static Color LineColor;

        public static Color LChorBottom;
        public static Color LChorTop;

        public static Color LCdiaBottom;
        public static Color LCdiaTop;

        public static Color PCmainLeg;
        public static Color LCmainLeg;

        public static double RadSphere;

        public static Color SC_CoM1D;
        public static Color SC_CoM2D;
        public static Color SC_CoM1H1D;
        public static Color SC_CoM1H;

        #endregion

        static MoObject()
        {
            entityType = 0u;
            names = new List<object>();
            indLast = 0;
            
            PointSize = 8;
            LineWidth = 2;
            PointColor = Color.White;
            LineColor = Color.White;

            LChorBottom = Color.Green;
            LChorTop = Color.DarkGreen;

            LCdiaBottom = Color.Cyan;
            LCdiaTop = Color.Magenta;

            PCmainLeg = Color.Orange;
            LCmainLeg = Color.Navy;

            RadSphere = 75.0;
            SC_CoM1D = Color.OrangeRed;
            SC_CoM2D = Color.Olive;
            SC_CoM1H1D = Color.HotPink;
            SC_CoM1H = Color.DarkSlateGray;
        }

    #endregion Static params

    public DaInput daObject { get; set; }
        public List<VisualEntity> Entities { get; set; }
        public List<VisualEntity> Points { get; set; }
        public List<VisualEntity> Lines { get; set; }
        public PropertyEditor PE { get; set; }

        protected MoObject(DaInput daobject) : base()
        {
            daObject = daobject;

            Entities = new List<VisualEntity>();
            Points = new List<VisualEntity>();
            Lines = new List<VisualEntity>();

            PE = null;
        }

        public override BaObType baObType()
        {
            return BaObType.Model;
        }

        public override void Write(StreamWriter sw)
        {
            throw new NotImplementedException();
        }

        public override void Read(StreamReader sr)
        {
            throw new NotImplementedException();
        }

        public abstract MoObType moObType();

        public abstract void Create();

        public virtual void DrawVisibles()
        {
            foreach (var item in Entities)
            {
                if (item.Visible == true)
                {
                    item.Draw();
                }
            }
        }

        public abstract void DrawSelectables();

        public virtual void SetBoundingBoxOnScreen(ref GVector3D wcMin, ref GVector3D wcMax)
        {
            if (Points.Count == 0)
            {
                return;
            }

            // get matrices
            GLint[] viewport = new GLint[4];
            GLdouble[] mvmatrix = new GLdouble[16];
            GLdouble[] projmatrix = new GLdouble[16];

            GL.GetIntegerv(GL.GL_VIEWPORT, viewport);
            GL.GetDoublev(GL.GL_MODELVIEW_MATRIX, mvmatrix);
            GL.GetDoublev(GL.GL_PROJECTION_MATRIX, projmatrix);

            // find min. and max. window coordiantes
            GLdouble wcXMin = double.MaxValue;
            GLdouble wcXMax = double.MinValue;
            GLdouble wcYMin = double.MaxValue;
            GLdouble wcYMax = double.MinValue;
            GLdouble wcZMin = double.MaxValue;
            GLdouble wcZMax = double.MinValue;

            double wcX = 0.0, wcY = 0.0, wcZ = 0.0;
            double ndX, ndY, ndZ;

            foreach (var poin in Points)
            {
                VisualPoint point = (VisualPoint)poin;

                if (point.Visible == true)
                {
                    ndX = point.Point.X;
                    ndY = point.Point.Y;
                    ndZ = point.Point.Z;

                    GLU.Project(
                        ndX,
                        ndY,
                        ndZ,
                        mvmatrix,
                        projmatrix,
                        viewport,
                        ref wcX,
                        ref wcY,
                        ref wcZ);

                    if (wcX > wcXMax) wcXMax = wcX;
                    if (wcX < wcXMin) wcXMin = wcX;

                    if (wcY > wcYMax) wcYMax = wcY;
                    if (wcY < wcYMin) wcYMin = wcY;

                    if (wcZ > wcZMax) wcZMax = wcZ;
                    if (wcZ < wcZMin) wcZMin = wcZ;
                }
            }

            if (wcXMin < wcMin.X) wcMin.X = wcXMin;
            if (wcXMax > wcMax.X) wcMax.X = wcXMax;
            if (wcYMin < wcMin.Y) wcMin.Y = wcYMin;
            if (wcYMax > wcMax.Y) wcMax.Y = wcYMax;
            if (wcZMin < wcMin.Z) wcMin.Z = wcZMin;
            if (wcZMax > wcMax.Z) wcMax.Z = wcZMax;
        }

        public virtual void UpdateVisualEntities()
        {
        }

        public virtual void SetVisibles(UInt64 visFlag)
        {
        }

        public virtual void SetSelectables(UInt64 selFlag)
        {
        }

        public void SetVisibles(bool vis_flag)
        {
            foreach (var entity in Entities)
            {
                entity.Visible = vis_flag;
            }
        }

        public void SetSelectables(bool sel_flag)
        {
            foreach (var entity in Entities)
            {
                entity.Selectable = sel_flag;
            }
        }

        #region Selection

        public static void OnBeforeSelection(uint entitytype, int namecount)
        {
            entityType = entitytype;
            indLast = namecount;
            names.Clear();
        }

        public static void OnAfterSelection()
        {
            entityType = 0u;
            indLast = 0;
            names.Clear();
        }

        #endregion Selection

    }
}
