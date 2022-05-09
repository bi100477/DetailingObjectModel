using DetailingObjectModel.Controls;
using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class CtDaBracingCenter : DaControl
    {
        private DaBracingCenter daBracingCenter { get; set; }
        public CheckBox Check_allAreSame { get; set; }
        public CheckBox Check_sameFrontBack { get; set; }
        public CheckBox Check_sameRightLeft { get; set; }
        public SpecialCheck SC_allAreSame { get; set; }
        public SpecialCheck SC_sameFrontBack { get; set; }
        public SpecialCheck SC_sameRightLeft { get; set; }
        public Button Button_Front { get; set; }
        public Button Button_Back { get; set; }
        public Button Button_Right { get; set; }
        public Button Button_Left { get; set; }
        public Button Button_MainLegs { get; set; }

        public CtDaBracingCenter(DaBracingCenter dabracingcenter) : base()
        {
            daBracingCenter = dabracingcenter;
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_allAreSame.Check() == false)
            {
                failedControl = SC_allAreSame.Control;
                return false;
            }
            else if (SC_sameFrontBack.Check() == false)
            {
                failedControl = SC_sameFrontBack.Control;
                return false;
            }
            else if (SC_sameRightLeft.Check() == false)
            {
                failedControl = SC_sameRightLeft.Control;
                return false;
            }

            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Check_allAreSame = ControlRunTime.CreateCheckBox("Check_allAreSame", "All are same", l, 170, 130, 19);
            Check_sameFrontBack = ControlRunTime.CreateCheckBox("Check_sameFrontBack", "Same as front", l+100, 67, 130, 19); ;
            Check_sameRightLeft = ControlRunTime.CreateCheckBox("Check_sameRightLeft", "Same as right", l, 117, 130, 19);

            Check_allAreSame.Click += new EventHandler(Check_All_Click);
            Check_sameFrontBack.Click += new EventHandler(Check_All_Click);
            Check_sameRightLeft.Click += new EventHandler(Check_All_Click);

            SC_allAreSame = new SpecialCheck(Check_allAreSame, true);
            SC_sameFrontBack = new SpecialCheck(Check_sameFrontBack, true);
            SC_sameRightLeft = new SpecialCheck(Check_sameRightLeft, true);

            parent.Controls.Add(Check_allAreSame);
            parent.Controls.Add(Check_sameFrontBack);
            parent.Controls.Add(Check_sameRightLeft);

            Button_Front = ControlRunTime.CreateButton("Button_Front", "Front", l + 100, 135, 75, 23);
            Button_Back = ControlRunTime.CreateButton("Button_Back", "Back", l + 100, 40, 75, 23);
            Button_Right = ControlRunTime.CreateButton("Button_Right", "Right", l + 200, 90, 75, 23);
            Button_Left = ControlRunTime.CreateButton("Button_Left", "Left", l, 90, 75, 23);
            Button_MainLegs = ControlRunTime.CreateButton("Button_MainLegs", "Main Legs", l + 100, 90, 75, 23);

            parent.Controls.Add(Button_Front);
            parent.Controls.Add(Button_Back);
            parent.Controls.Add(Button_Right);
            parent.Controls.Add(Button_Left);
            parent.Controls.Add(Button_MainLegs);

            Button_Front.Click += new EventHandler(Button_Front_Click);
            Button_Back.Click += new EventHandler(Button_Back_Click);
            Button_Right.Click += new EventHandler(Button_Right_Click);
            Button_Left.Click += new EventHandler(Button_Left_Click);
            Button_MainLegs.Click += new EventHandler(Button_MainLegs_Click);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.DaBracingCenter;
        }

        public override void Get()
        {
            daBracingCenter.allAreSame = SC_allAreSame.Get();
            daBracingCenter.sameFrontBack = SC_sameFrontBack.Get();
            daBracingCenter.sameRightLeft = SC_sameRightLeft.Get();
        }

        public override void Set()
        {
            SC_allAreSame.Set(daBracingCenter.allAreSame);
            SC_sameFrontBack.Set(daBracingCenter.sameFrontBack);
            SC_sameRightLeft.Set(daBracingCenter.sameRightLeft);
        }

        protected void Button_Front_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
                daBracingCenter.Refresh();

                daBracingCenter.Front.SetDataFromDialog();
            }
        }

        protected void Button_Back_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
                daBracingCenter.Refresh();

                daBracingCenter.Back.SetDataFromDialog();
            }
        }

        protected void Button_Right_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
                daBracingCenter.Refresh();

                daBracingCenter.Right.SetDataFromDialog();
            }
        }

        protected void Button_Left_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
                daBracingCenter.Refresh();

                daBracingCenter.Left.SetDataFromDialog();
            }
        }

        protected void Check_All_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
                daBracingCenter.Refresh();
            }
        }

        protected void Button_MainLegs_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                daBracingCenter.mainLegs.SetDataFromDialog();
            }
        }
    }
}