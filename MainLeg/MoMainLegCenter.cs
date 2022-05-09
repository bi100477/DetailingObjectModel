using DetailingObjectModel.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesInterface.geometry;

namespace DetailingObjectModel.MainLeg
{
    public class MoMainLegCenter : MoObject
    {
        public MoMainLegContainer frontLeft { get; set; }
        public MoMainLegContainer frontRight { get; set; }
        public MoMainLegContainer backLeft { get; set; }
        public MoMainLegContainer backRight { get; set; }
        public MoMainLegContainer rightLeft { get; set; }
        public MoMainLegContainer rightRight { get; set; }
        public MoMainLegContainer leftLeft { get; set; }
        public MoMainLegContainer leftRight { get; set; }
        public DaMainLegContainer daMainLegContainer { get; set; }
        public Topology3D topo3D { get; set; }

        public MoMainLegCenter(DaInput dainput, Topology3D topo3d) : base(dainput)
        {
            daMainLegContainer = (DaMainLegContainer)dainput;

            if (daMainLegContainer == null)
            {
                throw new Exception("daMainLegContainer == null");
            }

            topo3D = topo3d;

            frontLeft = new MoMainLegContainer(daMainLegContainer);
            frontRight = new MoMainLegContainer(daMainLegContainer);
            backLeft = new MoMainLegContainer(daMainLegContainer);
            backRight = new MoMainLegContainer(daMainLegContainer);
            rightLeft = new MoMainLegContainer(daMainLegContainer);
            rightRight = new MoMainLegContainer(daMainLegContainer);
            leftLeft = new MoMainLegContainer(daMainLegContainer);
            leftRight = new MoMainLegContainer(daMainLegContainer);
        }

        public override MoObType moObType()
        {
            return MoObType.MainLegCenter;
        }

        public override void Create()
        {
            foreach (DaMainLeg mainLegData in daMainLegContainer.mainLegs)
            {
                MoMainLeg mainLeg;

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[8]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                frontLeft.Entities.AddRange(mainLeg.Entities);
                frontLeft.Points.AddRange(mainLeg.Points);
                frontLeft.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                frontLeft.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[9]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                frontRight.Entities.AddRange(mainLeg.Entities);
                frontRight.Points.AddRange(mainLeg.Points);
                frontRight.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                frontRight.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[9]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                rightLeft.Entities.AddRange(mainLeg.Entities);
                rightLeft.Points.AddRange(mainLeg.Points);
                rightLeft.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                rightLeft.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[10]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                rightRight.Entities.AddRange(mainLeg.Entities);
                rightRight.Points.AddRange(mainLeg.Points);
                rightRight.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                rightRight.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[10]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                backLeft.Entities.AddRange(mainLeg.Entities);
                backLeft.Points.AddRange(mainLeg.Points);
                backLeft.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                backLeft.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[11]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                backRight.Entities.AddRange(mainLeg.Entities);
                backRight.Points.AddRange(mainLeg.Points);
                backRight.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                backRight.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[11]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                leftLeft.Entities.AddRange(mainLeg.Entities);
                leftLeft.Points.AddRange(mainLeg.Points);
                leftLeft.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                leftLeft.mainLegs.Add(mainLeg);

                mainLeg = new MoMainLeg(mainLegData, topo3D.Segments[8]);
                mainLeg.Create();

                #region visual entities

                Entities.AddRange(mainLeg.Entities);
                Points.AddRange(mainLeg.Points);
                Lines.AddRange(mainLeg.Lines);

                leftRight.Entities.AddRange(mainLeg.Entities);
                leftRight.Points.AddRange(mainLeg.Points);
                leftRight.Lines.AddRange(mainLeg.Lines);

                #endregion visual entities

                leftRight.mainLegs.Add(mainLeg);

                foreach (var point in Points)
                {
                    point.color = MoObject.PCmainLeg;
                }

                foreach (var line in Lines)
                {
                    line.color = MoObject.LCmainLeg;
                }
            }
        }

        public override void DrawSelectables()
        {
            frontLeft.DrawSelectables();
            frontRight.DrawSelectables();
            rightLeft.DrawSelectables();
            rightRight.DrawSelectables();
            backLeft.DrawSelectables();
            backRight.DrawSelectables();
            leftLeft.DrawSelectables();
            leftRight.DrawSelectables();
        }

        public override void SetVisibles(UInt64 visFlag)
        {
            bool visFront = Convert.ToBoolean(visFlag & MoObject.DF_VIS_FRONT);
            frontLeft.SetVisibles(visFront);
            frontRight.SetVisibles(visFront);

            bool visBack = Convert.ToBoolean(visFlag & MoObject.DF_VIS_BACK);
            backLeft.SetVisibles(visBack);
            backRight.SetVisibles(visBack);

            bool visRight = Convert.ToBoolean(visFlag & MoObject.DF_VIS_RIGHT);
            rightLeft.SetVisibles(visRight);
            rightRight.SetVisibles(visRight);

            bool visLeft = Convert.ToBoolean(visFlag & MoObject.DF_VIS_LEFT);
            leftLeft.SetVisibles(visLeft);
            leftRight.SetVisibles(visLeft);
        }

        public override void SetSelectables(UInt64 selFlag)
        {
            bool selFront = Convert.ToBoolean(selFlag & MoObject.DF_SEL_FRONT);
            frontLeft.SetSelectables(selFront);
            frontRight.SetSelectables(selFront);

            bool selBack = Convert.ToBoolean(selFlag & MoObject.DF_SEL_BACK);
            backLeft.SetSelectables(selBack);
            backRight.SetSelectables(selBack);

            bool selRight = Convert.ToBoolean(selFlag & MoObject.DF_SEL_RIGHT);
            rightLeft.SetSelectables(selRight);
            rightRight.SetSelectables(selRight);

            bool selLeft = Convert.ToBoolean(selFlag & MoObject.DF_SEL_LEFT);
            leftLeft.SetSelectables(selLeft);
            leftRight.SetSelectables(selLeft);
        }
    }
}
