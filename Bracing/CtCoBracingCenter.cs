using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bracing
{
    public class CtCoBracingCenter : DaControl
    {
        private DaBracingCenter daBracingCenter { get; set; }
        public Button Button_Front { get; set; }
        public Button Button_Back { get; set; }
        public Button Button_Right { get; set; }
        public Button Button_Left { get; set; }

        public CtCoBracingCenter(DaBracingCenter dabracingcenter) : base()
        {
            daBracingCenter = dabracingcenter;
        }

        public override bool Check()
        {
            return true;
        }

        public override void Create(Control parent, int l, int t)
        {
            Button_Front = ControlRunTime.CreateButton("Button_Front", "Front", l + 100, 135, 75, 23);
            Button_Back = ControlRunTime.CreateButton("Button_Back", "Back", l + 100, 40, 75, 23);
            Button_Right = ControlRunTime.CreateButton("Button_Right", "Right", l + 200, 90, 75, 23);
            Button_Left = ControlRunTime.CreateButton("Button_Left", "Left", l, 90, 75, 23);

            parent.Controls.Add(Button_Front);
            parent.Controls.Add(Button_Back);
            parent.Controls.Add(Button_Right);
            parent.Controls.Add(Button_Left);

            Button_Front.Click += new EventHandler(Button_Front_Click);
            Button_Back.Click += new EventHandler(Button_Back_Click);
            Button_Right.Click += new EventHandler(Button_Right_Click);
            Button_Left.Click += new EventHandler(Button_Left_Click);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.CoBracingCenter;
        }

        public override void Get()
        {
        }

        public override void Set()
        {
        }

        protected void Button_Front_Click(object sender, EventArgs e)
        {
            daBracingCenter.Front.SetConnectionsFromDialog();
        }

        protected void Button_Back_Click(object sender, EventArgs e)
        {
            daBracingCenter.Back.SetConnectionsFromDialog();
        }

        protected void Button_Right_Click(object sender, EventArgs e)
        {
            daBracingCenter.Right.SetConnectionsFromDialog();
        }

        protected void Button_Left_Click(object sender, EventArgs e)
        {
            daBracingCenter.Left.SetConnectionsFromDialog();
        }
    }
}
