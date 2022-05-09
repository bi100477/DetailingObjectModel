using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel.Profile
{
    public class CtProfile : DaControl
    {
        public DaProfile daProfile { get; set; }
        public CtProfileLayout ctProfileLayout { get; set; }
        public CtProfileOrientationList ctProfileOrientationList { get; set; }
        public CtProfileType ctProfileType { get; set; }
        public CtProfileBoltLocations ctProfileBoltLocations { get; set; }
        public CtProfileEndOffsets ctProfileEndOffsets { get; set; }
        public TabPage tabPageLayout { get; set; }
        public TabPage tabPageOrientations { get; set; }
        public TabPage tabPageProfile { get; set; }
        public TabControl tabControlProfile { get; set; }
        public TabPage tabPageProfileType { get; set; }
        public TabPage tabPageProfileDetail { get; set; }

        public CtProfile(DaProfile da_profile) : base()
        {
            daProfile = da_profile;

            ctProfileLayout = new CtProfileLayout(daProfile.profileLayout);

            ctProfileOrientationList = new CtProfileOrientationList(daProfile.GetProfileOrientations());

            ctProfileType = new CtProfileType(daProfile.profileType);
            ctProfileBoltLocations = new CtProfileBoltLocations(daProfile.profileBoltLocations);            
            ctProfileEndOffsets = new CtProfileEndOffsets(daProfile.profileEndOffsets);
        }

        public override DaCtType daCtType()
        {
            return DaCtType.Profile;
        }

        public override void Create(Control parent, int l, int t)
        {
            #region profile

            #region orientations tab page

            tabPageOrientations = new TabPage();

            tabPageOrientations.Name = "TabPage_Orientations";
            tabPageOrientations.Text = "Orientations";
            tabPageOrientations.UseVisualStyleBackColor = true;

            tabPageOrientations.Enter += new EventHandler(TabPageOrientations_Enter);
            tabPageOrientations.Leave += new EventHandler(TabPageOrientations_Leave);

            parent.Controls.Add(tabPageOrientations);

            #endregion orientations tab page

            #region profile tab page

            tabPageProfile = new TabPage();

            tabPageProfile.Name = "TabPage_Profile";
            tabPageProfile.Text = "Profile";
            tabPageProfile.UseVisualStyleBackColor = true;

            tabPageProfile.Enter += new EventHandler(TabPageProfile_Enter);
            tabPageProfile.Leave += new EventHandler(TabPageProfile_Leave);

            parent.Controls.Add(tabPageProfile);

            #endregion profile tab page

            #region profile tab control

            tabControlProfile = new TabControl();
            tabControlProfile.Dock = DockStyle.Fill;
            tabPageProfile.Controls.Add(tabControlProfile);

            #endregion profile tab control

            #region tab page profile type

            tabPageProfileType = new TabPage();

            tabPageProfileType.Name = "TabPage_ProfileType";
            tabPageProfileType.Text = "Profile Type";
            tabPageProfileType.UseVisualStyleBackColor = true;

            tabControlProfile.Controls.Add(tabPageProfileType);

            #endregion tab page profile type

            #region tab page profile detail

            tabPageProfileDetail = new TabPage();

            tabPageProfileDetail.Name = "TabPage_ProfileDetail";
            tabPageProfileDetail.Text = " ProfileDetail";
            tabPageProfileDetail.UseVisualStyleBackColor = true;

            tabControlProfile.Controls.Add(tabPageProfileDetail);

            #endregion tab page profile detail

            ctProfileLayout.Create(tabPageLayout, 40, 40);

            ctProfileOrientationList.Create(tabPageOrientations, 40, 40);

            ctProfileType.Create(tabPageProfileType, 40, 40);
            ctProfileBoltLocations.Create(tabPageProfileDetail, 40, 100);
            ctProfileEndOffsets.Create(tabPageProfileDetail, 40, 40);

            #endregion profile
        }

        public override bool Check()
        {
            failedControl = null;

            //if (ctProfileLayout.Check() == false)
            //{
            //    failedControl = ctProfileLayout.failedControl;
            //    failedTabPage = tabPageLayout;
            //    return false;
            //}
            if (ctProfileOrientationList.Check() == false)
            {
                failedControl = ctProfileOrientationList.failedControl;
                return false;
            }
            else if (ctProfileType.Check() == false)
            {
                failedControl = ctProfileType.failedControl;
                tabControlProfile.SelectedTab = tabPageProfileType;
                failedTabPage = tabPageProfile;
                return false;
            }
            else if (ctProfileBoltLocations.Check() == false)
            {
                failedControl = ctProfileBoltLocations.failedControl;
                tabControlProfile.SelectedTab = tabPageProfileDetail;
                failedTabPage = tabPageProfile;
                return false;
            }
            else if (ctProfileEndOffsets.Check() == false)
            {
                failedControl = ctProfileEndOffsets.failedControl;
                tabControlProfile.SelectedTab = tabPageProfileDetail;
                failedTabPage = tabPageProfile;
                return false;
            }

            return true;
        }

        public override void Get()
        {
            ctProfileLayout.Get();

            ctProfileOrientationList.Get();

            ctProfileType.Get();
            ctProfileBoltLocations.Get();
            ctProfileEndOffsets.Get();            
        }

        public override void Set()
        {
            ctProfileLayout.Set();

            ctProfileOrientationList.Set();

            ctProfileType.Set();
            ctProfileBoltLocations.Set();
            ctProfileEndOffsets.Set();
        }

        public override void Refresh()
        {
            ctProfileLayout.daProfileLayout = daProfile.profileLayout;

            ctProfileOrientationList.Orientations = daProfile.GetProfileOrientations();
            ctProfileOrientationList.Refresh();

            ctProfileType.daProfileType = daProfile.profileType;
            ctProfileBoltLocations.boltLocations = daProfile.profileBoltLocations;
            ctProfileEndOffsets.endOffsets = daProfile.profileEndOffsets;
        }

        private void TabPageProfile_Enter(object sender, EventArgs e)
        {
            Set();          
            tabControlProfile.SelectedTab = tabPageProfileType;
        }

        private void TabPageProfile_Leave(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                Get();
            }
            else
            {
                canLeaveTabPage = false;
                ShowFailedControl();
            }
        }

        private void TabPageOrientations_Enter(object sender, EventArgs e)
        {
            ctProfileOrientationList.Orientations = daProfile.GetProfileOrientations();
            ctProfileOrientationList.Refresh();
        }

        private void TabPageOrientations_Leave(object sender, EventArgs e)
        {           
            if (ctProfileOrientationList.Check() == true)
            {
                ctProfileOrientationList.Get();
            }
            else
            {
                canLeaveTabPage = false;
                ShowFailedControl();
            }
        }
    }
}
