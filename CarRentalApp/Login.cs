using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalDbEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalDbEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //try tap bouble tab
            try
            {
                SHA256 sha = SHA256.Create();

                var username = tbUser.Text.Trim();
                var password = tbPass.Text;

                ////convert the input string to a byte array and compute the hash.
                //byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                ////Create a new Stringbuilder to collect the bytes
                ////and create a string.
                //StringBuilder sBuilder = new StringBuilder();

                ////Loop through each byte of the hashed data
                //// and format each one as a hexadecimal string
                //for (int i = 0; i < data.Length; i++)
                //{
                //    sBuilder.Append(data[i].ToString("x2"));
                //}

                //var hashed_password = sBuilder.ToString();
                var hashed_password = Utils.HashPassword(password);

                //check for matching username, password and active flag
                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashed_password && q.isActive == true);
                if(user == null)
                {
                    MessageBox.Show("Please provide valid credential");
                }
                else
                {
                    var mainWindow = new MainWindow(this, user);
                    mainWindow.Show();
                    Hide();

                    //var role = user.UsersRoles.FirstOrDefault();
                    //var roleShortName = role.Role.shortname;
                    //////Ctrl MainWindow
                    //var mainwindow = new MainWindow(this, roleShortName);
                    //mainwindow.Show();
                    //Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Someyhing went wrong. Please try again {ex.Message}");
            }
          
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
