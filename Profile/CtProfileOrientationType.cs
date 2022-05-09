using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class SpecialProfileOrientationType : SpecialControl<EProfileOrientation, GroupBox>
    {
        public SpecialProfileOrientationType(GroupBox control) : base(control)
        {
        }

        public override bool Check()
        {
            foreach (var control in Control.Controls)
            {
                RadioButton radioButton = (RadioButton)control;

                if (radioButton.Checked == true)
                {
                    return true;
                }
            }

            return false;
        }

        public override EProfileOrientation Get()
        {
            for (int i = 0; i < Control.Controls.Count; i++)
            {
                RadioButton radioButton = (RadioButton)Control.Controls[i];

                if (radioButton.Checked == true)
                {
                    //return Enum.Parse<EProfileOrientation>(radioButton.Text);
                    return (EProfileOrientation)i;
                }
            }

            return EProfileOrientation.None;
        }

        public override void Set(EProfileOrientation profileDetailType)
        {
            int ii = (int)profileDetailType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Checked = true;
        }

        public void Disable(EProfileOrientation profileDetailType)
        {
            int ii = (int)profileDetailType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Enabled = false;
        }

        public void Enable(EProfileOrientation profileDetailType)
        {
            int ii = (int)profileDetailType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Enabled = true;
        }
    }

    public delegate void FuncProfileOrientationChanged(EProfileOrientation profileDetailType);

    public class CtProfileOrientationType : DaControl
    {
        public SpecialProfileOrientationType SC_profileOrientationType { get; set; }
        public FuncProfileOrientationChanged funcProfileOrientationChanged { get; set; }

        public CtProfileOrientationType() : base()
        {
            funcProfileOrientationChanged += OnProfileOrientationChangedDefault;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileOrientationType;
        }

        public override void Create(Control parent, int l, int t)
        {
            GroupBox gb = ControlRunTime.CreateGroupBox("Group_profileOrientation", "Orientation", l, t, 110, 140);
            parent.Controls.Add(gb);

            RadioButton rb;

            rb = ControlRunTime.CreateRadioButton("Radio_Orientation_Front", "Front", 15, 20, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            rb = ControlRunTime.CreateRadioButton("Radio_Orientation_Right", "Right", 15, 45, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            rb = ControlRunTime.CreateRadioButton("Radio_Orientation_Back", "Back", 15, 70, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            rb = ControlRunTime.CreateRadioButton("Radio_Orientation_Left", "Left", 15, 95, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            SC_profileOrientationType = new SpecialProfileOrientationType(gb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_profileOrientationType.Check() == false)
            {
                failedControl = SC_profileOrientationType.Control;
                return false;
            }

            return true;
        }

        public override void Get()
        {
        }

        public override void Set()
        {
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb == null)
            {
                throw new Exception("rb == null");
            }

            EProfileOrientation pdt = Enum.Parse<EProfileOrientation>(rb.Text);
            funcProfileOrientationChanged(pdt);
        }

        private void OnProfileOrientationChangedDefault(EProfileOrientation profileDetailType)
        {

        }

    }
}
