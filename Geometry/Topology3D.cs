using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.Geometry
{
    public abstract class Topology3D
    {
        protected GCS3D csl { get; set; }
        public GCS3D csg { get; set; }
        public List<GVector3D> Points { get; set; }
        public List<GSegment3D> Segments { get; set; }
        public List<GPolygon3D> Polygons { get; set; }

        public Topology3D(GCS3D csg_)
        {
            csg = csg_;
            Points = new List<GVector3D>();
            Segments = new List<GSegment3D>();
            Polygons = new List<GPolygon3D>();

            csl = new GCS3D(
                GVector3D.UnitX(), 
                GVector3D.UnitY(), 
                GVector3D.UnitZ(), 
                GVector3D.Zero());
        }

        public abstract Topology3DType topoType();

        public abstract void Create();

        //public abstract void Draw();
    }
}
