using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.Geometry
{
    public class RectangularPrism : Topology3D
    {
        #region Params
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        #endregion

        #region Constructor
        public RectangularPrism(double flength, double fwidth, double fheight, GCS3D cs) : base(cs)
        {
            Length = flength;
            Width = fwidth;
            Height = fheight;
        }
        #endregion


        #region Abstract functions
        public override Topology3DType topoType()
        {
            return Topology3DType.RectangularPrism;
        }

        public override void Create()
        {
            CreatePoints();
            CreateSegments();
            CreatePolygons();
        }

        #endregion

        #region Member functions
        private void CreatePoints()
        {
            List<GVector3D> ptsLocal = ComputePtsLocal();
            Points = Functions.ToGlobal(csg, ptsLocal);
        }

        private List<GVector3D> ComputePtsLocal()
        {
            List<GVector3D> ptsLocal = new List<GVector3D>();

            double L2 = Length / 2.0;
            double W2 = Width / 2.0;           

            GVector3D ptMove;

            //points on length plane
            ptMove = GVector3D.MovePoint(csl.Origin, csl.uX, -L2);
            ptMove = GVector3D.MovePoint(ptMove, csl.uY, -W2);

            //-1-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uX, Length);

            //-2-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uY, Width);

            //-3-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uX, -Length);

            //-4-
            ptsLocal.Add(ptMove);

            //points on width plane
            ptMove = GVector3D.MovePoint(csl.Origin, csl.uZ, Height);

            ptMove = GVector3D.MovePoint(ptMove, csl.uX, -L2);
            ptMove = GVector3D.MovePoint(ptMove, csl.uY, -W2);

            //-5-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uX, Length);

            //-6-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uY, Width);

            //-7-
            ptsLocal.Add(ptMove);

            ptMove = GVector3D.MovePoint(ptMove, csl.uX, -Length);

            //-8-
            ptsLocal.Add(ptMove);

            return ptsLocal;
        }

        private void CreateSegments()
        {
            Segments = new List<GSegment3D>();

            //segments on bottom face
            Segments.Add(new GSegment3D(Points[0], Points[1]));
            Segments.Add(new GSegment3D(Points[1], Points[2]));
            Segments.Add(new GSegment3D(Points[2], Points[3]));
            Segments.Add(new GSegment3D(Points[3], Points[0]));

            //segments on top face
            Segments.Add(new GSegment3D(Points[4], Points[5]));
            Segments.Add(new GSegment3D(Points[5], Points[6]));
            Segments.Add(new GSegment3D(Points[6], Points[7]));
            Segments.Add(new GSegment3D(Points[7], Points[4]));

            //side segments
            Segments.Add(new GSegment3D(Points[0], Points[4]));
            Segments.Add(new GSegment3D(Points[1], Points[5]));
            Segments.Add(new GSegment3D(Points[2], Points[6]));
            Segments.Add(new GSegment3D(Points[3], Points[7]));
        }
        private void CreatePolygons()
        {
            Polygons = new List<GPolygon3D>();

            List<GVector3D> points;

            points = new List<GVector3D>();
            points.Add(Points[0]);
            points.Add(Points[1]);
            points.Add(Points[2]);
            points.Add(Points[3]);

            Polygons.Add(new GPolygon3D(points, GVector3D.UnitZ()));

            points = new List<GVector3D>();
            points.Add(Points[4]);
            points.Add(Points[5]);
            points.Add(Points[6]);
            points.Add(Points[7]);

            Polygons.Add(new GPolygon3D(points, GVector3D.UnitZ()));

            points = new List<GVector3D>();
            points.Add(Points[0]);
            points.Add(Points[1]);
            points.Add(Points[5]);
            points.Add(Points[4]);

            Polygons.Add(new GPolygon3D(points, GVector3D.Negate(GVector3D.UnitY())));

            points = new List<GVector3D>();
            points.Add(Points[1]);
            points.Add(Points[2]);
            points.Add(Points[6]);
            points.Add(Points[5]);

            Polygons.Add(new GPolygon3D(points, GVector3D.UnitX()));

            points = new List<GVector3D>();
            points.Add(Points[2]);
            points.Add(Points[3]);
            points.Add(Points[7]);
            points.Add(Points[6]);

            Polygons.Add(new GPolygon3D(points, GVector3D.UnitY()));

            points = new List<GVector3D>();
            points.Add(Points[3]);
            points.Add(Points[0]);
            points.Add(Points[4]);
            points.Add(Points[7]);

            Polygons.Add(new GPolygon3D(points, GVector3D.Negate(GVector3D.UnitX())));
        }

        #endregion
    }
}
