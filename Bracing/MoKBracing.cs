using DetailingObjectModel.Geometry;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.Bracing
{
    public abstract class MoKBracing : MoBracing
    {
        public MoProfile prDiaBottom { get; set; }
        public MoProfile prDiaTop { get; set; }
        public MoProfile prHorBottom { get; set; }
        public MoProfile prHorTop { get; set; }
        protected GVector3D ptBL { get; set; }
        protected GVector3D ptBR { get; set; }
        protected GVector3D ptTL { get; set; }
        protected GVector3D ptTR { get; set; }
        protected GVector3D ptML { get; set; }
        protected GVector3D ptMR { get; set; }

        protected MoKBracing(DaInput dainput, GPolygon3D polygon) : base(dainput, polygon)
        {
            prDiaBottom = null;
            prDiaTop = null;

            prHorBottom = null;
            prHorTop = null;

            ptBL = null;
            ptBR = null;
            ptTL = null;
            ptTR = null;
            ptML = null;
            ptMR = null;
        }

        public abstract MoKBracingType kBracingType();

        public override MoBracingType moBracingType()
        {
            return MoBracingType.KBracing;
        }

        public override void Create()
        {
            GSegment3D segL = new GSegment3D(Polygon.Points[0], Polygon.Points[3]);
            GSegment3D segR = new GSegment3D(Polygon.Points[1], Polygon.Points[2]);

            GPlane3D plBottom = new GPlane3D(new GVector3D(0.0, 0.0, daBracing.BottomLevel()), GVector3D.UnitZ());
            GPlane3D plTop = new GPlane3D(new GVector3D(0.0, 0.0, daBracing.TopLevel()), GVector3D.UnitZ());
            GPlane3D plMid = new GPlane3D(new GVector3D(0.0, 0.0, daBracing.MidLevel()), GVector3D.UnitZ());

            ptBL = plBottom.IntersectWithSegment(segL);
            ptBR = plBottom.IntersectWithSegment(segR);

            ptTL = plTop.IntersectWithSegment(segL);
            ptTR = plTop.IntersectWithSegment(segR);

            ptML = plMid.IntersectWithSegment(segL);
            ptMR = plMid.IntersectWithSegment(segR);

            if (ptBL == null)
            {
                throw new Exception("ptBL == null");
            }

            if (ptBR == null)
            {
                throw new Exception("ptBR == null");
            }

            if (ptTL == null)
            {
                throw new Exception("ptTL == null");
            }

            if (ptTR == null)
            {
                throw new Exception("ptTR == null");
            }

            if (ptML == null)
            {
                throw new Exception("ptML == null");
            }

            if (ptMR == null)
            {
                throw new Exception("ptMR == null");
            }
        }
    }
}
