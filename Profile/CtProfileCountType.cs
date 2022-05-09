using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Profile
{
    public class SpecialProfileCount : SpecialControl<EProfileCount, GroupBox>
    {
        public SpecialProfileCount(GroupBox control) : base(control)
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

        public override EProfileCount Get()
        {
            for (int i = 0; i < Control.Controls.Count; i++)
            {
                RadioButton radioButton = (RadioButton)Control.Controls[i];

                if (radioButton.Checked == true)
                {
                    //return Enum.Parse<EProfileCount>(radioButton.Text);
                    return (EProfileCount)i;
                }
            }

            return EProfileCount.None;
        }

        public override void Set(EProfileCount profileCountType)
        {
            int ii = (int)profileCountType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Checked = true;
        }

        public void Disable(EProfileCount profileCountType)
        {
            int ii = (int)profileCountType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Enabled = false;
        }

        public void Enable(EProfileCount profileCountType)
        {
            int ii = (int)profileCountType;

            RadioButton radioButton = (RadioButton)Control.Controls[ii];
            radioButton.Enabled = true;
        }
    }

    public delegate void FuncProfileCountChanged(EProfileCount profileCountType);

    public class CtProfileCountType : DaControl
    {
        public SpecialProfileCount SC_profileCountType { get; set; }
        public FuncProfileCountChanged funcProfileCountChanged { get; set; }

        public CtProfileCountType() : base()
        {
            funcProfileCountChanged += OnProfileCountChangedDefault;
        }

        public override DaCtType daCtType()
        {
            return DaCtType.ProfileCountType;
        }

        public override void Create(Control parent, int l, int t)
        {
            GroupBox gb = ControlRunTime.CreateGroupBox("Group_profileCount", "Profile count", l, t, 90, 90);
            parent.Controls.Add(gb);

            RadioButton rb;

            rb = ControlRunTime.CreateRadioButton("Radio_Count_One", "One", 15, 20, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            rb = ControlRunTime.CreateRadioButton("Radio_Count_Two", "Two", 15, 45, 75, 19);
            rb.Click += new EventHandler(RadioButton_Click);
            gb.Controls.Add(rb);

            SC_profileCountType = new SpecialProfileCount(gb);
        }

        public override bool Check()
        {
            failedControl = null;

            if (SC_profileCountType.Check() == false)
            {
                failedControl = SC_profileCountType.Control;
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

            EProfileCount pdt = Enum.Parse<EProfileCount>(rb.Text);
            funcProfileCountChanged(pdt);
        }

        private void OnProfileCountChangedDefault(EProfileCount profileCountType)
        {

        }

    }
}
