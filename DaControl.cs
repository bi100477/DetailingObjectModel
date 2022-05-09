using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailingObjectModel
{
    public abstract class DaControl : BaObject
    {
        public Control failedControl { get; set; }
        public TabPage failedTabPage { get; set; }
        public static bool canLeaveTabPage { get; set; }
        public static TabControl tabControl { get; set; }

        protected DaControl() : base()
        {
            failedControl = null;
            failedTabPage = null;
        }

        public abstract DaCtType daCtType();

        public abstract void Create(Control parent, int l, int t);

        public abstract bool Check();

        public abstract void Get();

        public abstract void Set();

        public virtual void Refresh()
        {

        }

        public override BaObType baObType()
        {
            return BaObType.Control;
        }

        public override void Write(StreamWriter sw)
        {
            throw new NotImplementedException();
        }

        public override void Read(StreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void ShowFailedControl()
        {
            if (failedControl != null)
            {
                MessageBox.Show("invalid input", "Warning");
                failedControl.Visible = true;
                failedControl.Focus();
            }
        }

        public static void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !canLeaveTabPage;
            canLeaveTabPage = true;
        }

        public static void SetTabControl(Control parent)
        {
            tabControl = null;

            if (parent is TabPage)
            {
                tabControl = (TabControl)parent.Parent;
            }
            else if (parent is TabControl)
            {
                tabControl = (TabControl)parent;
            }
            else if (parent is Form)
            {
                var tabControls = parent.Controls.OfType<TabControl>().ToList();

                if (tabControls.Count == 0)
                {
                    tabControl = new TabControl();
                    tabControl.Dock = DockStyle.Fill;
                    parent.Controls.Add(tabControl);

                    tabControl.Selecting += new TabControlCancelEventHandler(TabControl_Selecting);
                }
                else
                {
                    if (tabControls.Count > 1)
                    {
                        throw new Exception("tabControls.Count > 1");
                    }

                    tabControl = tabControls[0];
                }
            }
        }

        public static void SetSelectedIndex(int selectedIndex)
        {
            if (tabControl != null)
            {
                tabControl.SelectedIndex = selectedIndex;
            }
        }

        public static int GetSelectedIndex()
        {
            if (tabControl != null)
            {
                return tabControl.SelectedIndex;
            }

            return -1;
        }
    }
}
