using System.Text.Json;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using Lab_2.Models;
namespace Lab__2_
{
    /// <summary>
    /// Логика взаимодействия для CarSelection.xaml
    /// </summary>
    public partial class CarSelection : Window
    {
        int i = 1;
        private ClientVm _client;
        public CarSelection(ClientVm client)
        {
            InitializeComponent();
            _client = client;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray;
            CarIdBox.Text = RentalCarVm.carslist[0].CarID.ToString(); 
            CarNameBox.Text = RentalCarVm.carslist[0].CarName.ToString();
            CarDesBox.Text = RentalCarVm.carslist[0].Description.ToString();
            CarIsAvBox.Text = RentalCarVm.carslist[0].IsAvailable.ToString();
            CarPriceBox.Text = RentalCarVm.carslist[0].RentPrice.ToString();
            PrevBtn.IsEnabled = false;
            Daewoo_Lanos.Visibility = Visibility.Collapsed;
            Toyota_Land_Cruiser.Visibility = Visibility.Collapsed;
            Mercedes_Benz.Visibility = Visibility.Collapsed;
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --i;
            if (i<RentalCarVm.carslist.Count) { NextBtn.IsEnabled = true; }
            if (i <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            Image myImage = (Image)FindName(RentalCarVm.carslist[i].CarName);
            myImage.Visibility = Visibility.Collapsed;
            myImage = (Image)FindName(RentalCarVm.carslist[i-1].CarName);
            myImage.Visibility = Visibility.Visible;
            CarIdBox.Text = RentalCarVm.carslist[i-1].CarID.ToString();
            CarNameBox.Text = RentalCarVm.carslist[i-1].CarName.ToString();
            CarDesBox.Text = RentalCarVm.carslist[i-1].Description.ToString();
            CarIsAvBox.Text = RentalCarVm.carslist[i-1].IsAvailable.ToString();
            CarPriceBox.Text = RentalCarVm.carslist[i-1].RentPrice.ToString();
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
            if (RentalCarVm.carslist[i-1].IsAvailable)
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
            if(i>0) { PrevBtn.IsEnabled=true; }
            if (i <= RentalCarVm.carslist.Count - 1)
            {
                Image myImage = (Image)FindName(RentalCarVm.carslist[i-1].CarName);
                myImage.Visibility = Visibility.Collapsed;
                myImage = (Image)FindName(RentalCarVm.carslist[i].CarName);
                myImage.Visibility = Visibility.Visible;  
                CarIdBox.Text = RentalCarVm.carslist[i].CarID.ToString();
                CarNameBox.Text = RentalCarVm.carslist[i].CarName.ToString();
                CarDesBox.Text = RentalCarVm.carslist[i].Description.ToString();
                CarIsAvBox.Text = RentalCarVm.carslist[i].IsAvailable.ToString();
                CarPriceBox.Text = RentalCarVm.carslist[i].RentPrice.ToString();
                i++;
                if (i>=RentalCarVm.carslist.Count)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }
        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            int selectedcar = i;
            for (int j = 1; j <= RentalCarVm.carslist.Count; j++)
            {
                RentalCarVm machine = RentalCarVm.carslist.Find(m => m.CarID == j);
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
                RentalCarVm machine = RentalCarVm.carslist.Find(m => m.CarID == rentorder.CarID);
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
