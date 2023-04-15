using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Lab_2.Models;
using Lab__2_.Extensions;
using System.Collections.Generic;

namespace Lab__2_
{
    /// <summary>
    /// Логика взаимодействия для CarSelection.xaml
    /// </summary>
    public partial class CarSelection : Window
    {
        int current_page = 1;
        private ClientVm _client;
        public static List<RentalCarVm> CarslList;
        public CarSelection(ClientVm client)
        {
            InitializeComponent();
            _client = client;
            CarslList = FileExtension.GetCarFromFile("carlist.json");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray; 
            CarIdBox.Text = CarslList[0].CarID.ToString(); 
            CarNameBox.Text = CarslList[0].CarName.ToString();
            CarDesBox.Text = CarslList[0].Description.ToString();
            CarIsAvBox.Text = CarslList[0].IsAvailable.ToString();
            CarPriceBox.Text = CarslList[0].RentPrice.ToString();
            PrevBtn.IsEnabled = false;
            Daewoo_Lanos.Visibility = Visibility.Collapsed;
            Toyota_Land_Cruiser.Visibility = Visibility.Collapsed;
            Mercedes_Benz.Visibility = Visibility.Collapsed;
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --current_page;
            if (current_page < CarslList.Count) { NextBtn.IsEnabled = true; }
            if (current_page <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            Image myImage = (Image)FindName(CarslList[current_page].CarName);
            myImage.Visibility = Visibility.Collapsed;
            myImage = (Image)FindName(CarslList[current_page -1].CarName);
            myImage.Visibility = Visibility.Visible;
            CarIdBox.Text = CarslList[current_page -1].CarID.ToString();
            CarNameBox.Text = CarslList[current_page -1].CarName.ToString();
            CarDesBox.Text = CarslList[current_page -1].Description.ToString();
            CarIsAvBox.Text = CarslList[current_page -1].IsAvailable.ToString();
            CarPriceBox.Text = CarslList[current_page -1].RentPrice.ToString();
        }
        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {   
            for (int k = 0; k < OrderVm.orders.Count; k++)
            {
                if (OrderVm.orders[k].ClientID == _client.ClientID )
                {
                    MessageBoxResult result = MessageBox.Show("You have already placed an order", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (CarslList[current_page -1].IsAvailable)
            {
                CarNameBox.Visibility = Visibility.Collapsed;
                CarIsAvBox.Visibility = Visibility.Collapsed;
                CarIdBox.Visibility = Visibility.Collapsed;
                CarDesBox.Visibility = Visibility.Collapsed;
                ChooseBtn.Visibility = Visibility.Collapsed;
                PrevBtn.Visibility = Visibility.Collapsed;
                NextBtn.Visibility = Visibility.Collapsed;
                label1.Visibility = Visibility.Collapsed;
                label2.Visibility = Visibility.Collapsed;
                label3.Visibility = Visibility.Collapsed;
                label4.Visibility = Visibility.Collapsed;
                BMW.Visibility = Visibility.Collapsed;
                Mercedes_Benz.Visibility = Visibility.Collapsed;
                Toyota_Land_Cruiser.Visibility = Visibility.Collapsed;
                Daewoo_Lanos.Visibility = Visibility.Collapsed;
                label5.Content = "Enter the number of rental days:";
                label5.Margin = new Thickness(70, 280, 0, 0);
                CarPriceBox.Text = "";
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("This car is currently unavailable", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if(current_page >0) { PrevBtn.IsEnabled=true; }
            if (current_page <= CarslList.Count - 1)
            {
                Image myImage = (Image)FindName(CarslList[current_page -1].CarName);
                myImage.Visibility = Visibility.Collapsed;
                myImage = (Image)FindName(CarslList[current_page].CarName);
                myImage.Visibility = Visibility.Visible;  
                CarIdBox.Text = CarslList[current_page].CarID.ToString();
                CarNameBox.Text = CarslList[current_page].CarName.ToString();
                CarDesBox.Text = CarslList[current_page].Description.ToString();
                CarIsAvBox.Text = CarslList[current_page].IsAvailable.ToString();
                CarPriceBox.Text = CarslList[current_page].RentPrice.ToString();
                current_page++;
                if (current_page >=CarslList.Count)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }
        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            int selectedcar = current_page;
            for (int j = 1; j <= CarslList.Count; j++)
            {
                RentalCarVm machine = CarslList.Find(m => m.CarID == j);
                if (selectedcar == machine.CarID)
                {
                    if (machine.IsAvailable)
                    {
                        machine.IsAvailable = false;
                        int rentdays = int.Parse(CarPriceBox.Text);
                        ClientVm client = ClientVm.clientlist.Find(m => m.ClientID == _client.ClientID);
                        OrderVm carorder = new OrderVm();
                        carorder.CarID = machine.CarID;
                        carorder.RentalTime = rentdays;
                        carorder.ClientID = client.ClientID;
                        carorder.FullName = client.FullName;
                        carorder.PassportNumber = client.PassportNumber;
                        carorder.TotalAmount = machine.RentPrice * rentdays;
                        OrderVm.orders.Add(carorder);
                        MessageBoxResult result = MessageBox.Show("Your order has been placed.","", MessageBoxButton.OK, MessageBoxImage.Information);
                        FileExtension.WriteCarToFile(CarslList,"carlist.json");
                        break;
                    }
                }
            }
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {   
            OrderVm rentorder =  OrderVm.orders.Find(m => m.ClientID == _client.ClientID);
            if (rentorder != null && rentorder.IsApproved  ) 
            {
                Daewoo_Lanos.Visibility = Visibility.Collapsed;
                Toyota_Land_Cruiser.Visibility = Visibility.Collapsed;
                Mercedes_Benz.Visibility = Visibility.Collapsed;
                BMW.Visibility = Visibility.Collapsed;
                PrevBtn.Visibility = Visibility.Collapsed;
                NextBtn.Visibility = Visibility.Collapsed;
                ChooseBtn.Visibility = Visibility.Collapsed;
                OrderBtn.Visibility = Visibility.Collapsed;
                RentalCarVm machine = CarslList.Find(m => m.CarID == rentorder.CarID);
                Image myImage = (Image)FindName(machine.CarName);
                myImage.Visibility = Visibility.Visible;
                CarIdBox.Text = machine.CarID.ToString();
                CarNameBox.Text = machine.CarName;
                CarDesBox.Text = machine.Description;
                CarIsAvBox.Text = "true";
                CarPriceBox.Text = machine.RentPrice.ToString();
            }

            else if (rentorder != null)
            {
                MessageBoxResult result = MessageBox.Show($"Admin rejects your order :{rentorder.RejectionReason}", "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else 
            {
                MessageBoxResult result = MessageBox.Show($"You have not placed any orders", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderVm rentorder = OrderVm.orders.Find(m => m.ClientID == _client.ClientID);
            if ((_client.Balance = _client.Balance - rentorder.TotalAmount) > 0)
            {
                rentorder.IsPaid = true;
                PayBtn.IsEnabled = false;
                MessageBoxResult result = MessageBox.Show("Payment was successful", "", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminForm.FastDriving(rentorder,_client);
            }
            else
            {
                rentorder.IsPaid = false;
                MessageBoxResult result = MessageBox.Show("Insufficient funds to pay", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Exittn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
