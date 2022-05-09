using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetailingObjectModel.Connection;
using DetailingObjectModel.Geometry;
using DetailingObjectModel.Profile;


namespace DetailingObjectModel.Bracing
{
    public class MoKBracingRight : MoKBracing
    {
        #region Create MoBracing class

        const int IClassIdentifier = 104;

        public static MoBracing CreateMoKBracingRight(DaInput dainput, GPolygon3D polygon)
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
                    moBracing = new MoKBracingRight(dainput, polygon);
                }
            }

            return moBracing;
        }

        #endregion Create MoBracing class

        protected MoKBracingRight(DaInput dainput, GPolygon3D polygon) : base(dainput, polygon)
        {
        }

        public override MoKBracingType kBracingType()
        {
            return MoKBracingType.Right;
        }

        public override string Caption()
        {
            return "KBracingRight";
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
            return prDiaBottom;
        }

        public override MoProfile GetDiagonalLeftTop()
        {
            return prDiaTop;
        }

        public override MoProfile GetDiagonalRightBottom()
        {
            return null;
        }

        public override MoProfile GetDiagonalRightTop()
        {
            return null;
        }

        public override void Create()
        {
            base.Create();

            prDiaBottom = new MoProfile(daBracing.GetDiagonalLeftBottom(), ptBL, ptMR);
            prDiaBottom.Create();

            Entities.AddRange(prDiaBottom.Entities);
            Points.AddRange(prDiaBottom.Points);
            Lines.AddRange(prDiaBottom.Lines);

            prDiaTop = new MoProfile(daBracing.GetDiagonalLeftTop(), ptMR, ptTL);
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
         }

        public override void CreateConnectionRight()
        {
            List<MoProfile> profiles = new List<MoProfile>();
            profiles.Add(prDiaBottom);
            profiles.Add(prDiaTop);

            connRight = MoConnection.CreateMoConnectionClass(daBracing.connRight, MoConnectionType.M2D, 1, profiles);
        }
    }
}