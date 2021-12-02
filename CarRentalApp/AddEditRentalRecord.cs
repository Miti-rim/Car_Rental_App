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
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalDbEntities _db;
        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Rental Record";
            this.Text = "Add New Rental Record";
            isEditMode = false;
            _db = new CarRentalDbEntities();
        }


        //ctor press tab twice for constractor
        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you entered a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalDbEntities();
                PopulateFields(recordToEdit);
            }

        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            dtRented.Value = (DateTime)recordToEdit.DateRented;
            dtReturned.Value = (DateTime)recordToEdit.DateReturned; ;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblrecordId.Text = recordToEdit.id.ToString();

            //var carType = cbTypeOfCar.Text;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show($"Thank You for Renting: {tbCustomerName.Text}");
                string customerName = tbCustomerName.Text;
                // string dateOut = dtRented.Value.ToString();
                var dateOut = dtRented.Value;
                // string dateIn = dtReturned.Value.ToString();
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);

               // var carType = cbTypeOfCar.SelectedItem.ToString();
                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    //MessageBox.Show("Please enter missing data.");
                    errorMessage += "Error: Please enter missing data.\n\r";
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    //MessageBox.Show("Illigal Date Selection");
                    errorMessage += "Error: Illigal data Selection.\n\r";
                }

                //if(isValid == true)
                if (isValid)
                {
                    //Declare an object of the record to be addeds
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode)
                    {
                        //If in edit mode, then get the ID and retrive the record from the database and place
                        //the result in the record object
                        var id = int.Parse(lblrecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    }

                    //Populate record object with values from the form
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;
                    //If not in edit mode, then add the record object to the database
                    if (!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);

                    //Save changes made to the entity
                    _db.SaveChanges();

                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                        $"Date Rented:  {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"Cost: {cost}\n\r" +
                        $"Thank Your For Business");
                    
                    Close();
                }
                    //{
                    //    var id = int.Parse(lblrecordId.Text);

                    //    var rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    //    rentalRecord.CustomerName = customerName;
                    //    rentalRecord.DateRented = dateOut;
                    //    rentalRecord.DateReturned = dateIn;
                    //    rentalRecord.Cost = (decimal)cost;
                    //    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;

                    //    _db.SaveChanges();

                        

                    //}
                    //else
                    //{
                    //    var rentalRecord = new CarRentalRecord();
                    //    rentalRecord.CustomerName = customerName;
                    //    rentalRecord.DateRented = dateOut;
                    //    rentalRecord.DateReturned = dateIn;
                    //    rentalRecord.Cost = (decimal)cost;
                    //    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;

                    //    _db.CarRentalRecords.Add(rentalRecord);
                    //    _db.SaveChanges();

                    //    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                    //    $"Date Rented:  {dateOut}\n\r" +
                    //    $"Date Returned: {dateIn}\n\r" +
                    //    $"Car Type: {carType}\n\r" +
                    //    $"Cost: {cost}\n\r" +
                    //    $"Thank Your For Business");
                        
                    //}
                    //Close();

               // }
                else
                {
                    MessageBox.Show(errorMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Select * from TypesOfCcars
            //var cars = carRentalEntities.TypesOfCars.ToList();
            var cars = _db.TypesOfCars
                .Select(q => new { Id = q.Id, Name = q.Make, Model = q.Model + " " + q.Model })
                .ToList();

            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "Id";
            cbTypeOfCar.DataSource = cars;        }

        private void cbTypeOfCar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

       /* private void btn_MainWindow_Click(object sender, EventArgs e)
        {   //var mainWindow = new MainWindow();
            //As I wish
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }*/
    }


}
