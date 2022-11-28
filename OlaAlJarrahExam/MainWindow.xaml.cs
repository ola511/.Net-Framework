using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace OlaAlJarrahExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> car = new List<string>();

        DataSet1 ds1 = new DataSet1();
        DataSet1TableAdapters.VehicleTableAdapter taVehicle = new DataSet1TableAdapters.VehicleTableAdapter();

        DataSet1.VehicleDataTable tbCars = new DataSet1.VehicleDataTable();

        DataSet1TableAdapters.RepairTableAdapter taRepair = new DataSet1TableAdapters.RepairTableAdapter();
      
        DataSet1.RepairDataTable tbRepairs = new DataSet1.RepairDataTable();


        DataSet1TableAdapters.InventoryTableAdapter taInventory = new DataSet1TableAdapters.InventoryTableAdapter();

        DataSet1.InventoryDataTable tbInventory = new DataSet1.InventoryDataTable();

        public MainWindow()
        {
            InitializeComponent();

            car.Clear();
            car.Add("Ford");
            car.Add("Mustang");
            car.Add("Civic");
            cmbVehicle.ItemsSource = car;


            refreshCarGrids();
            refreshRepairGrids();
            refreshInventoryGrids();
        }

        private void refreshInventoryGrids()
        {
            taInventory.Fill(ds1.Inventory);
            taInventory.Fill(tbInventory);
            dgInventoryView.ItemsSource = tbInventory;

            cmbInventory.ItemsSource = tbInventory;
            cmbInventory.SelectedValuePath = "ID";
            cmbInventory.DisplayMemberPath = "vehicleID";

           
        }

        private void refreshRepairGrids()
        {
            taRepair.Fill(ds1.Repair);
            taRepair.Fill(tbRepairs);

            dgVehicleView.ItemsSource = tbRepairs;

            cmbRepair.ItemsSource = tbRepairs;
            cmbRepair.SelectedValuePath = "ID";
            cmbRepair.DisplayMemberPath = "RepairDetails";
        }

        private void refreshCarGrids()
        {
            taVehicle.Fill(ds1.Vehicle);
            taVehicle.Fill(tbCars);
            dgCarMake.ItemsSource = tbCars;

            cmbVehicle.ItemsSource = tbCars;
            cmbVehicle.SelectedValuePath = "ID";
            cmbVehicle.DisplayMemberPath = "Make";


            //cmbNextID.ItemsSource = tbCars;
            //cmbNextID.SelectedValuePath = "ID";
            //cmbNextID.DisplayMemberPath = "Make";
        }

        private void cmbRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbRepair.SelectedValue);
            DataRow[] dr = ds1.Repair.Select("ID=" + selected);

            if(dr.Length > 0)
            {
                tbInventoryID.Text = dr[0]["inventoryID"].ToString();
                tbRepairDetails.Text = dr[0]["RepairDetails"].ToString();
                //cmbNextFlight.SelectedValue = dr[0]["Vehicle"].ToString();
            }



        }

        private void btnRepairAdd_Click(object sender, RoutedEventArgs e)
        {
             DataSet1.RepairRow vRow = ds1.Repair.NewRepairRow();
            vRow.inventoryID = Convert.ToInt32(tbInventoryID.Text);
            vRow.RepairDetails = tbRepairDetails.Text;
           

            ds1.Repair.Rows.Add(vRow);
          taRepair.Update(ds1.Repair);
            refreshRepairGrids();
        }

        private void btnRepairUpdate_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbRepair.SelectedValue);

            DataRow[] dr = ds1.Repair.Select("ID=" + selected);

            dr[0]["inventoryID"] = tbInventoryID.Text;
            dr[0]["RepairDetails"] = tbRepairDetails.Text;
          

            taRepair.Update(ds1.Repair);

            refreshRepairGrids();
        }

        private void btnRepairDelete_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbRepair.SelectedValue);

            DataRow[] dr = ds1.Repair.Select("ID=" + selected);

            dr[0].Delete();


            taRepair.Update(ds1.Repair);

            refreshRepairGrids();
        }
        private void btnRepairRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshRepairGrids();
        }

        private void cmbVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbVehicle.SelectedValue);
            DataRow[] dr = ds1.Vehicle.Select("ID=" + selected);

            if(dr.Length > 0)
            {
                tbMake.Text = dr[0]["Make"].ToString();
                tbModel.Text = dr[0]["Model"].ToString();
                tbYear.Text = dr[0]["Year"].ToString();
                tbNewUsed.Text = dr[0]["NewUsed"].ToString();
               
            }
        }

        private void btnVehicleAdd_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.VehicleRow vRow = ds1.Vehicle.NewVehicleRow();
            vRow.Make = tbMake.Text;
            vRow.Model = tbModel.Text;
            vRow.Year = Convert.ToInt32(tbYear.Text);
            vRow.NewUsed = Convert.ToBoolean(tbNewUsed.Text);

            ds1.Vehicle.Rows.Add(vRow);
          taVehicle.Update(ds1.Vehicle);
            refreshCarGrids();

        }

        private void btnVehicleUpdate_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbVehicle.SelectedValue);

            DataRow[] dr = ds1.Vehicle.Select("ID=" + selected);

            dr[0]["Make"] = tbMake.Text;
            dr[0]["Model"] = tbModel.Text;
            dr[0]["Year"] = tbYear.Text;
            dr[0]["NewUsed"] =tbNewUsed.Text;  

            taVehicle.Update(ds1.Vehicle);

            refreshCarGrids();
        }

        private void btnVehicleDelete_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbVehicle.SelectedValue);

            DataRow[] dr = ds1.Vehicle.Select("ID=" + selected);

            dr[0].Delete();
            taVehicle.Update(ds1.Vehicle);
            refreshCarGrids();
        }
        private void btnVehicleRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshCarGrids();
           
        }

        private void cmbInventroy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbInventory.SelectedValue);
            DataRow[] dr = ds1.Inventory.Select("ID=" + selected);

            if (dr.Length > 0)
            {
                tbVehicle.Text = dr[0]["vehicleID"].ToString();
                tbNum.Text = dr[0]["NumberOnHand"].ToString();
                tbPrice.Text = dr[0]["Price"].ToString();
                tbCost.Text = dr[0]["Cost"].ToString();
                //cmbNextID.SelectedValue = dr[0]["Vehicle"].ToString();

            }
        }

        private void btnInventroyAdd_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.InventoryRow vRow = ds1.Inventory.NewInventoryRow();
            vRow.vehicleID = Convert.ToInt32(tbVehicle.Text);
            vRow.NumberOnHand = Convert.ToInt32(tbNum.Text);
            vRow.Price = Convert.ToInt32(tbPrice.Text);
            vRow.Cost = Convert.ToInt32(tbCost.Text);

            ds1.Inventory.Rows.Add(vRow);
            taInventory.Update(ds1.Inventory);
            refreshInventoryGrids();
        }

        private void btnInventroyUpdate_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbInventory.SelectedValue);

            DataRow[] dr = ds1.Inventory.Select("ID=" + selected);

            dr[0]["vehicleID"] = tbVehicle.Text;
            dr[0]["NumberOnHand"] = tbNum.Text;
            dr[0]["Price"] = tbPrice.Text;
            dr[0]["Cost"] = tbCost.Text;

            taInventory.Update(ds1.Inventory);

            refreshInventoryGrids();
        }

        private void btnInventroyDelete_Click(object sender, RoutedEventArgs e)
        {
            int selected = Convert.ToInt32(cmbInventory.SelectedValue);

            DataRow[] dr = ds1.Inventory.Select("ID=" + selected);

            dr[0].Delete();
            taInventory.Update(ds1.Inventory);
            refreshInventoryGrids();
        }

        private void btnInventroyRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshInventoryGrids();
        }

       
    }
}
