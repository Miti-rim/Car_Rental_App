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
    public partial class ResetPassword : Form
    {
        private readonly CarRentalDbEntities _db;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalDbEntities();
            _user = user;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbPass.Text;
                var confirm_password = tbConfirmPass.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);
            if (password != confirm_password)
                {
                    MessageBox.Show("Password do not match. Please try again!");

                }

                _user.password = Utils.HashPassword(password);
                _db.SaveChanges();

                MessageBox.Show("Password was reset successfully");
            }
            catch (Exception)
            {

                MessageBox.Show("An Error has occured. Please try again");
                //throw;
            }
            

        }

        private void tbConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
