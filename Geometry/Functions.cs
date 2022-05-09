using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.Geometry
{
    public static class Functions
    {
        public static List<GVector3D> ToLocal(GCS3D csg, List<GVector3D> ptsGlobal)
        {
            List<GVector3D> ptsLocal = new List<GVector3D>();

            for (int i = 0; i < (int)ptsGlobal.Count; i++)
            {
                ptsLocal.Add(csg.ToLocal(ptsGlobal[i]));
            }

            return ptsLocal;
        }

        public static List<GVector3D> ToGlobal(GCS3D csg, List<GVector3D> ptsLocal)
        {
            List<GVector3D> ptsGlobal = new List<GVector3D>();

            for (int i = 0; i < (int)ptsLocal.Count; i++)
            {
                ptsGlobal.Add(csg.ToGlobal(ptsLocal[i]));
            }

            return ptsGlobal;
        }

        public static GVector3D CompIntSegPlaneZ(GSegment3D seg, double zLevel)
        {
            GVector3D uZ = GVector3D.UnitZ();
            GVector3D pointOnPlane = GVector3D.UnitZ();
            pointOnPlane.Z = zLevel;

            GPlane3D plane3D = new GPlane3D(pointOnPlane, uZ);

            return plane3D.IntersectWithSegment(seg);
        }

        public static GVector3D OffsetFromPlane(GPlane3D plane, GVector3D offDir, double offVal)
        {
            //try
            GVector3D ptTry = GVector3D.MovePoint(plane.pointOnPlane, offDir, 10.0);

            double offTry = plane.DistanceTo(in ptTry);
            double scale = offVal / offTry;

            return GVector3D.MovePoint(plane.pointOnPlane, offDir, scale * 10.0);
        }

        internal static bool IsPointConvex(GVector3D ptC, GVector3D ptB, GVector3D ptA, GVector3D nP)
        {
            GVector3D vB = (ptC - ptB).Normal();
            GVector3D vA = (ptA - ptC).Normal();

            GVector3D cBA = GVector3D.Cross(vB, vA);

            double xd = GVector3D.Dot(cBA, nP);

            return (xd > 0.0);
        }
    }
}
