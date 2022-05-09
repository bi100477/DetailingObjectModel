using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Bolt
{
    public class SpecialBoltDetailType : SpecialControl<EBoltDetailType, GroupBox>
    {
        public SpecialBoltDetailType(GroupBox control) : base(control)
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

        public override EBoltDetailType Get()
        {
            for (int i = 0; i < Control.Controls.Count; i++)
            {
                RadioButton radioButton = (RadioButton)Control.Controls[i];

                if (radioButton.Checked == true)
                {
                    //return Enum.Parse<EBoltDetailType>(radioButton.Text);
                    return (EBoltDetailType)i;
                }
            }

            return EBoltDetailType.None;
        }

        public override void Set(EBoltDetailType boltDetailType)
        {
            int ii = (int)boltDetailType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Checked = true;
        }
    }

    public delegate void FuncBoltDetailChanged(EBoltDetailType boltDetailType);

    public class CtBoltDetailType : DaControl
    {
        public SpecialBoltDetailType SC_boltDetailType { get; set; }
        public FuncBoltDetailChanged funcBoltDetailChanged { get; set; }        

        public CtBoltDetailType() : base()
        {
            funcBoltDetailChanged += OnBoltDetailChangedDefault;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.BoltDetailType;
        }

        public override void Create(Control parent, int l, int t)
        {
            GroupBox gb = ControlRunTime.CreateGroupBox("Group_boltLayout", "Layout", l, t, 110, 90);
            parent.Controls.Add(gb);

            RadioButton rb;

            rb = ControlRunTime.CreateRadioButton("Radio_Layout_Vector", "Vector", 15, 20, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            rb = ControlRunTime.CreateRadioButton("Radio_Layout_Array", "Array", 15, 45, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            SC_boltDetailType = new SpecialBoltDetailType(gb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_boltDetailType.Check() == false)
            {
                failedControl = SC_boltDetailType.Control;
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

            EBoltDetailType bdt = Enum.Parse<EBoltDetailType>(rb.Text);
            funcBoltDetailChanged(bdt);
        }

        public void OnBoltDetailChangedDefault(EBoltDetailType boltDetailType)
        {

        }
    }
}
