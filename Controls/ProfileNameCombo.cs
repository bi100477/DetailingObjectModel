using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Controls
{
    public class ProfileNameCombo
    {
        public ComboBox comboBox { get; set; }

        public ProfileNameCombo(ComboBox combobox)
        {
            comboBox = combobox;

            comboBox.BeginUpdate();

            comboBox.Items.Clear();

            comboBox.Items.Add("L45x5");
            comboBox.Items.Add("L60x6");
            comboBox.Items.Add("L70x6");
            comboBox.Items.Add("L70x7");
            comboBox.Items.Add("L80x8");
            comboBox.Items.Add("L90x9");
            comboBox.Items.Add("L90x10");
            comboBox.Items.Add("L100x10");
            comboBox.Items.Add("L120x10");
            comboBox.Items.Add("L120x12");
            comboBox.Items.Add("L130x12");
            comboBox.Items.Add("L150x18");
            comboBox.Items.Add("L200x24");
            comboBox.Items.Add("L250x26");

            comboBox.EndUpdate();
        }
    }
}
