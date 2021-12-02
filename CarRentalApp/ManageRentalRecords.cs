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
    //private readonly CarRentalDbEntities _db;

    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalDbEntities _db;

        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalDbEntities();
        }


        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord
            {
                MdiParent = this.MdiParent
            };
            addRentalRecord.Show();
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of Selected row

                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //query database record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                //////launch AddEditVehicle window with data
                //var addEditVehicle = new AddEditVehicle(record);
                var addEditRentalRecord = new AddEditRentalRecord(record);
                addEditRentalRecord.MdiParent = this.MdiParent;
                addEditRentalRecord.Show();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Close();
                //get Id of seleced row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //query database for record 
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete thi record?", 
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes)
                {
                    //delete vehicle from the table
                    _db.CarRentalRecords.Remove(record);
                    _db.SaveChanges();

                    //gvRecordList.Refresh();
                    PopulateGird();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGird();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void PopulateGird()
        {
            var records = _db.CarRentalRecords.Select(q => new
            {
                Customer = q.CustomerName,
                DateOut = q.DateRented,
                DateIn = q.DateReturned,
                Id = q.id,
                q.Cost,
                Car = q.TypesOfCar.Make + "" + q.TypesOfCar.Model

            }).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "Date In";
            gvRecordList.Columns["DateOut"].HeaderText = "Date Out";

            //Hide the column for ID. Changed from hard coded column value to the name;
            //to make it more dynamic
            gvRecordList.Columns["Id"].Visible = false;

        }
    }
}
