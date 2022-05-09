using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Controls
{
    public class BoltDiameterCombo
    {
        public ComboBox comboBox { get; set; }

        public BoltDiameterCombo(ComboBox combobox)
        {
            comboBox = combobox;

            comboBox.BeginUpdate();

            comboBox.Items.Clear();

            comboBox.Items.Add("M16");
            comboBox.Items.Add("M18");
            comboBox.Items.Add("M20");
            comboBox.Items.Add("M24");
            comboBox.Items.Add("M27");

            comboBox.EndUpdate();
        }
    }
}
