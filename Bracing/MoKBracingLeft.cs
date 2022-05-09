using DetailingObjectModel.Connection;
using DetailingObjectModel.Geometry;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DetailingObjectModel.Bracing
{
    public class MoKBracingLeft : MoKBracing
    {
        #region Create MoBracing class

        const int IClassIdentifier = 100;

        public static MoBracing CreateMoKBracingLeft(DaInput dainput, GPolygon3D polygon)
        {
            DaBracing daBracing = (DaBracing)dainput;

            if (daBracing == null)
            {
                throw new Exception("daBracing == null");
            }

            MoBracing moBracing = null;

            if (daBracing.daBracingType() == DaBracingType.KBracing)
            {
                if (daBracing.IntIdentifier() == IClassIdentifier)
                {
                    moBracing = new MoKBracingLeft(dainput, polygon);
                }
            }

            return moBracing;
        }

        #endregion Create MoBracing class

        protected MoKBracingLeft(DaInput dainput, GPolygon3D polygon) : base(dainput, polygon)
        {
        }

        public override MoKBracingType kBracingType()
        {
            return MoKBracingType.Left;
        }

        public override string Caption()
        {
            return "KBracingLeft";
        }

        public override int IntIdentifier()
        {
            return IClassIdentifier;
        }

        public override List<MoProfile> GetProfiles()
        {
            List<MoProfile> profiles = new List<MoProfile>();

            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);

            return profiles;
        }

        public override MoProfile GetHorizontalBottom()
        {
            return null;
        }

        public override MoProfile GetHorizontalTop()
        {
            return null;
        }

        public override MoProfile GetDiagonalLeftBottom()
        {
            return null;
        }

        public override MoProfile GetDiagonalLeftTop()
        {
            return null;
        }

        public override MoProfile GetDiagonalRightBottom()
        {
            return prDiaBottom;
        }

        public override MoProfile GetDiagonalRightTop()
        {
            return prDiaTop;
        }

        public override void Create()
        {
            base.Create();

            prDiaBottom = new MoProfile(daBracing.GetDiagonalRightBottom(), ptBR, ptML);
            prDiaBottom.Create();

            Entities.AddRange(prDiaBottom.Entities);
            Points.AddRange(prDiaBottom.Points);
            Lines.AddRange(prDiaBottom.Lines);

            prDiaTop = new MoProfile(daBracing.GetDiagonalRightTop(), ptML, ptTR);
            prDiaTop.Create();

            Entities.AddRange(prDiaTop.Entities);
            Points.AddRange(prDiaTop.Points);
            Lines.AddRange(prDiaTop.Lines);

            foreach (var line in prDiaBottom.Lines)
            {
                line.color = MoObject.LCdiaBottom;
            }

            foreach (var line in prDiaTop.Lines)
            {
                line.color = MoObject.LCdiaTop;
            }
        }

        public override void CreateConnectionLeft()
        {
            List<MoProfile> profiles = new List<MoProfile>();
            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);

            connLeft = MoConnection.CreateMoConnectionClass(daBracing.connLeft, MoConnectionType.M2D, 0, profiles);
        }

        public override void CreateConnectionRight()
        {
        }

        //public override DaConnection CreateConnectionLeft()
        //{
        //    List<MoProfile> profiles = new List<MoProfile>();
        //    profiles.Add(prDiaBottom);
        //    profiles.Add(prDiaTop);

        //    return DaConnection.CreateDaConnectionClass(DaConnectionType.M2D, 0, profiles);
        //}

        //public override DaConnection CreateConnectionRight()
        //{
        //    return null;
        //}

    }
}
