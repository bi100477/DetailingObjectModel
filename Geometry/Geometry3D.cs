using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.Geometry
{
	public class GCS3D
    {
        public GVector3D uX { get; set; }
		public GVector3D uY { get; set; }
		public GVector3D uZ { get; set; }
		public GVector3D Origin { get; set; }
        public double[] rotMatrix { get; set; }
		public double[] irotMatrix { get; set; }

		public GCS3D(GVector3D ux, GVector3D uy, GVector3D uz, GVector3D origin)
        {
			uX = ux;
			uY = uy;
			uZ = uz;
			Origin = origin;

            #region rotation matrix

            rotMatrix = new double[16];

			rotMatrix[0] = uX.X;
			rotMatrix[1] = uX.Y;
			rotMatrix[2] = uX.Z;
			rotMatrix[3] = 0.0;

			rotMatrix[4] = uY.X;
			rotMatrix[5] = uY.Y;
			rotMatrix[6] = uY.Z;
			rotMatrix[7] = 0.0;

			rotMatrix[8] = uZ.X;
			rotMatrix[9] = uZ.Y;
			rotMatrix[10] = uZ.Z;
			rotMatrix[11] = 0.0;

			rotMatrix[12] = Origin.X;
			rotMatrix[13] = Origin.Y;
			rotMatrix[14] = Origin.Z;
			rotMatrix[15] = 0.0;

			#endregion rotation matrix

			#region inverse rotation matrix

			irotMatrix = new double[16];

			irotMatrix[0] = uX.X;
			irotMatrix[1] = uY.X;
			irotMatrix[2] = uZ.X;
			irotMatrix[3] = 0.0;

			irotMatrix[4] = uX.Y;
			irotMatrix[5] = uY.Y;
			irotMatrix[6] = uZ.Y;
			irotMatrix[7] = 0.0;

			irotMatrix[8] = uX.Z;
			irotMatrix[9] = uY.Z;
			irotMatrix[10] = uZ.Z;
			irotMatrix[11] = 0.0;

			irotMatrix[12] = -Origin.X;
			irotMatrix[13] = -Origin.Y;
			irotMatrix[14] = -Origin.Z;
			irotMatrix[15] = 0.0;

			#endregion inverse rotation matrix
		}

		public GVector3D ToGlobal(in GVector3D ptLocal)
        {
			double[] res = new double[3] { 0.0, 0.0, 0.0 };
			double[] pt = new double[3] { 0.0, 0.0, 0.0 };

			pt[0] = ptLocal.X;
			pt[1] = ptLocal.Y;
			pt[2] = ptLocal.Z;

			for (int I = 0; I < 3; I++)
			{
				for (int J = 0; J < 3; J++) res[I] += rotMatrix[4 * J + I] * pt[J];
				res[I] += rotMatrix[4 * 3 + I];
			}

			return new GVector3D(res[0], res[1], res[2]);
		}

		public GVector3D ToLocal(in GVector3D ptGlobal)
		{
			double[] res = new double[3] { 0.0, 0.0, 0.0 };
			double[] pt = new double[3] { 0.0, 0.0, 0.0 };

			pt[0] = ptGlobal.X;
			pt[1] = ptGlobal.Y;
			pt[2] = ptGlobal.Z;

			for (int I = 0; I < 3; I++)
			{
				for (int J = 0; J < 3; J++) res[I] += irotMatrix[4 * J + I] * pt[J];
				res[I] += irotMatrix[4 * 3 + I];
			}

			return new GVector3D(res[0], res[1], res[2]);
		}

		public static GCS3D DefaultCS()
        {
			return new GCS3D(
				GVector3D.UnitX(),
				GVector3D.UnitY(),
				GVector3D.UnitZ(),
				GVector3D.Zero());
        }

	}

	public class GSegment3D
	{
		public GVector3D Start { get; set; }
		public GVector3D End { get; set; }
		public GVector3D Dir { get; set; }

		public GSegment3D(GVector3D start, GVector3D end)
		{
			Start = start;
			End = end;

			Dir = (End - Start).Normal();
		}
	}

	public class GPolygon3D
	{
		public List<GVector3D> Points { get; set; }
		public GVector3D Normal { get; set; }

		public GPolygon3D(List<GVector3D> points, GVector3D normal)
		{
			Points = points;
			Normal = normal;
		}
	}

	public class GPlane3D
	{
        private double tolVal { get; set; }
        public GVector3D pointOnPlane { get; set; }
		public GVector3D Normal { get; set; }

		public GPlane3D(GVector3D pointonplane, GVector3D normal, double tolval = 1.0E-05)
		{
			pointOnPlane = pointonplane;
			Normal = normal;

			tolVal = tolval;
		}

		public GVector3D ProjectPoint(in GVector3D ptIn)
		{
			GVector3D dP = ptIn - pointOnPlane;
			GVector3D dN = Normal.Scale(GVector3D.Dot(dP, Normal));

			return ptIn - dN;
		}

		public double SignedDistance(in GVector3D ptIn)
		{
			GVector3D ptP = ProjectPoint(ptIn);
			return GVector3D.Dot(ptIn - ptP, Normal);
		}

		public double DistanceTo(in GVector3D ptIn)
		{
			GVector3D ptP = ProjectPoint(ptIn);
			return (ptIn - ptP).Length();
		}

		public GVector3D IntersectWithSegment(in GSegment3D segIn)
		{
			double dot = GVector3D.Dot(segIn.Dir, Normal);

			//check if line is parallel
			if (Math.Abs(dot) > tolVal)
			{
				double dS = SignedDistance(segIn.Start);
				double dE = SignedDistance(segIn.End);

				double s = dS + dE;
				double d = dE - dS;

				double k = -s / d;

				return segIn.Start.Scale(0.5 * (1.0 - k)) + segIn.End.Scale(0.5 * (1.0 + k));
			}

			return null;
		}

		public bool PointOnPlane(in GVector3D ptIn)
        {
			double dist = DistanceTo(in ptIn);

			return (dist < tolVal);
        }

		public bool PointAbovePlane(in GVector3D ptIn)
		{
			if (PointOnPlane(ptIn) == true)
            {
				return false;
            }

			return (SignedDistance(ptIn) > tolVal);
		}
	}
}
