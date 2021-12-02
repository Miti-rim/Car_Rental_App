using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class MainWindow : Form
        //private Login _login;
    {
        private Login _login;
        public string _roleName;
        public User _user;
        public MainWindow(Login login)
        {
            InitializeComponent();
        }

        public MainWindow(Login login, User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            _roleName = user.UsersRoles.FirstOrDefault().Role.shortname;
        }
        //public MainWindow(Login login)
        //{
        //    InitializeComponent();
        //    _login = login;
        //}
        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                var addRentalRecord = new AddEditRentalRecord();
                //addRentalRecord.ShowDialog();
                addRentalRecord.MdiParent = this;
                addRentalRecord.Show();
                //addRentalRecord.Show();
            }

        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var Openforms = Application.OpenForms.Cast<Form>();
            //var isOpen = Openforms.Any(q => q.Name == "ManageVehicleListing");
           // if(!isOpen)
           if(!Utils.FormIsOpen("ManageVehicleListing"))
            {
                var vehicleListing = new ManageVehicleListing();
                vehicleListing.MdiParent = this;
                vehicleListing.Show();
            }
           
            
        }
        
        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageRentalRecords"))
            {
                var manageRentalRecords = new ManageRentalRecords();
                manageRentalRecords.MdiParent = this;
                manageRentalRecords.Show();
            }
                
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageUsers"))
            {
                var manageUsers = new ManageUsers();
                manageUsers.MdiParent = this;
                manageUsers.Show();
            }


                
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_user.password == Utils.DefaultHashedPassword())
            {
                var resetPassword = new ResetPassword(_user);
                resetPassword.ShowDialog();
            }

            var username = _user.username;
            tsiLoginText.Text = $"Logged in as : {username}";
            if(_roleName != "admin")
            {
                manageUsersToolStripMenuItem.Visible = false;
            }
        }



        //private void MainWindow_FormClosing(object sender , FromClosingEventArgs e)
        //{
        //     _login.Close();
        //}
    }

    //internal class Utils
    //{
    //    internal static bool FormIsOpen(string v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    
}
