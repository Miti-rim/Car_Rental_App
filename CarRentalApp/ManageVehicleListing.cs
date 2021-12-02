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
    public partial class ManageVehicleListing : Form
    {
        
        private readonly CarRentalDbEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalDbEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {


            try
            {
                PopulateGird();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            //Select * from TypesOfCars
            //var cars = _db.TypesOfCars.ToList();

            //Select Id as CardId, name as CarName from TypesOfCar
            //var cars = _db.TypesOfCars.
            //    Select(q => new { CarId = q.Id, CarName = q.Make }).
            //    ToList();


            //var cars = _db.TypesOfCars
            //    .Select(q => new
            //    {

            //        Make = q.Make,
            //        Model = q.Model,
            //        VIN = q.VIN,
            //        Year = q.Year,
            //        LicensePlateNumber = q.LicencePlateNumber,
            //        q.Id
            //    })
            //    .ToList();
            //gvVehicleList.DataSource = cars;
            //gvVehicleList.Columns[4].HeaderText = "Licence Plate Number";
            //gvVehicleList.Columns[5].Visible = false;
            
            
            //gvVehicleList.Columns[0].HeaderText = "ID";
            //gvVehicleList.Columns[1].HeaderText = "NAME";

        }

        //private void btnAddCar_Click(object sender, EventArgs e)
        //{
        //    var addEditVehicle = new AddEditVehicle();
        //    addEditVehicle.MdiParent = this.MdiParent;
        //    addEditVehicle.Show();
        //}

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();

            //var addEditVehicle = new AddEditVehicle();
            //addEditVehicle.MdiParent = this.MdiParent;
            //addEditVehicle.Show();

        }


        //private void btnEditCar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //get Id of Selected row

        //        var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

        //        //query database record
        //        var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

        //        //launch AddEditVehicle window with data
        //        var addEditVehicle = new AddEditVehicle(car);
        //        addEditVehicle.MdiParent = this.MdiParent;
        //        addEditVehicle.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}");
        //    }


        //}

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of Selected row

                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                //launch AddEditVehicle window with data
                if (!Utils.FormIsOpen("AddEditVehicle"))
                {
                    var addEditVehicle = new AddEditVehicle(car, this);
                    addEditVehicle.MdiParent = this.MdiParent;
                    addEditVehicle.Show();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Close();
                //get Id of seleced row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record 
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if(dr == DialogResult.Yes)
                {
                    //delete vehicle from the table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGird();

                //gvVehicleList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            
        }

        //New function to populateGuird. can be called anytime we need a gird refresh
        public void PopulateGird()
        {
            //Select a custom model collect
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicencePlateNumber = q.LicencePlateNumber,
                    q.Id
                }).ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from hard coded column value to the name;
            //to make it more dynamic
            gvVehicleList.Columns["Id"].Visible = false;


        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //Simple refresh option
            PopulateGird();
            gvVehicleList.Update();
            gvVehicleList.Refresh();
        }

        private void gvRecordList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
