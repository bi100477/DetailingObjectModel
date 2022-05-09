using DetailingObjectModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypesUI;

namespace DetailingObjectModel.Controls
{
    public class ProfileControl
    {
        private DaProfileDetail profileDetail { get; set; }
        private GroupBox Group_PC { get; set; }
        private ComboBox Combo_SN { get; set; }
        private ComboBox Combo_BN { get; set; }
        private TextBox Text_NB { get; set; }
        private CheckBox Check_DoCreate { get; set; }
        private Button Button_ProfileData { get; set; }
        private Button Button_BoltData { get; set; }
        private Button Button_AppDef { get; set; }
        private BoltDiameterCombo BDC_BN { get; set; }
        private ProfileNameCombo PNC_PN { get; set; }
        private SpecialCombo SC_SectionName { get; set; }
        private SpecialCombo SC_BoltName { get; set; }
        private IntegerText IT_NumBolt { get; set; }
        private SpecialCheck SC_DoCreate { get; set; }

        public ProfileControl(
            DaProfileDetail profiledetail, 
            Control parentControl, 
            string postFix,
            string capText,
            int l, int t)
        {
            profileDetail = profiledetail;

            Group_PC = ControlRunTime.CreateGroupBox(
                "Group_PC" + postFix, 
                capText, 
                l, t, 220, 110);

            parentControl.Controls.Add(Group_PC);

            Combo_SN = ControlRunTime.CreateComboBox("Combo_SN" + postFix, "", 45, 25, 70, 23);
            Combo_BN = ControlRunTime.CreateComboBox("Combo_BN" + postFix, "", 45, 50, 70, 23);
            Text_NB = ControlRunTime.CreateTextBox("Text_NB" + postFix, "", 20, 50, 20, 23);
            Check_DoCreate = ControlRunTime.CreateCheckBox("Check_DC" + postFix, "Create", 20, 78, 75, 19);

            Button_ProfileData = ControlRunTime.CreateButton("Button_PD" + postFix, "Profile", 120, 25, 90, 23);
            Button_BoltData = ControlRunTime.CreateButton("Button_BD" + postFix, "Bolt", 120, 50, 90, 23);
            Button_AppDef = ControlRunTime.CreateButton("Button_AppDef" + postFix, "App. Def.", 120, 75, 90, 23);

            Group_PC.Controls.Add(Combo_SN);
            Group_PC.Controls.Add(Combo_BN);
            Group_PC.Controls.Add(Text_NB);
            Group_PC.Controls.Add(Check_DoCreate);
            Group_PC.Controls.Add(Button_ProfileData);
            Group_PC.Controls.Add(Button_BoltData);
            Group_PC.Controls.Add(Button_AppDef);

            BDC_BN = new BoltDiameterCombo(Combo_BN);
            PNC_PN = new ProfileNameCombo(Combo_SN);

            SC_SectionName = new SpecialCombo(Combo_SN);
            SC_BoltName = new SpecialCombo(Combo_BN);

            IT_NumBolt = new IntegerText(Text_NB, false, false, false);

            SC_DoCreate = new SpecialCheck(Check_DoCreate, true);
        }

        internal bool Check(out Control failedControl)
        {
            failedControl = null;

            if (SC_DoCreate.Check() == false)
            {
                failedControl = SC_DoCreate.Control;
                return false;
            }
            else if (SC_SectionName.Check() == false)
            {
                failedControl = SC_SectionName.Control;
                return false;
            }
            else if (SC_BoltName.Check() == false)
            {
                failedControl = SC_BoltName.Control;
                return false;
            }
            else if (IT_NumBolt.Check() == false)
            {
                failedControl = IT_NumBolt.Control;
                return false;
            }

            return true;
        }

        internal void Get()
        {
            //profileDetail.Create = SC_DoCreate.Get();
            //profileDetail.profileData.profileType.sectionName = SC_SectionName.Get();
            //profileDetail.boltDetail.boltData.boltInfo.boName = SC_BoltName.Get();
            //profileDetail.boltDetail.boltData.numBolt = IT_NumBolt.Get();
        }

        internal void Set()
        {
            //SC_DoCreate.Set(profileDetail.Create);
            //SC_SectionName.Set(profileDetail.profileData.profileType.sectionName);
            //SC_BoltName.Set(profileDetail.boltDetail.boltData.boltInfo.boName);
            //IT_NumBolt.Set(profileDetail.boltDetail.boltData.numBolt);
        }
    }
}
