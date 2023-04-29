using System.Windows;
using System.Windows.Media;
using Lab_2.Models;
using Lab__2_.Extensions;
using System.Collections.Generic;
using Lab__2_.Views;
using System.Windows.Media.Imaging;
using System;
using Lab__2_.Services;

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
        private readonly ICarService carService;
        public CarSelection(ClientVm client,ICarService carService)
        {
            InitializeComponent();
            _client = client;
            CarslList = FileExtension.GetCarFromFile("carlist.json");
            this.carService = carService;
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray;
            PrintCars(0);
            PrevBtn.IsEnabled = false;
        }
        public void PrintCars(int current_car)
        {
            //Task 3.5
            Action assignOrderValues = () =>
            {
                CarIdBox.Text = CarslList[current_car].CarID.ToString();
                CarNameBox.Text = CarslList[current_car].CarName.ToString();
                CarDesBox.Text = CarslList[current_car].Description.ToString();
                CarIsAvBox.Text = CarslList[current_car].IsAvailable.ToString();
                CarPriceBox.Text = CarslList[current_car].RentPrice.ToString();
                CarImage.Source = new BitmapImage(new Uri(CarslList[current_car].CarImagePath));
            };
            //
            assignOrderValues();
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --current_page;
            if (current_page < CarslList.Count) { NextBtn.IsEnabled = true; }
            if (current_page <= 1)
            {
                PrevBtn.IsEnabled = false;
            }

            PrintCars(current_page - 1);
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
                PrintCars(current_page );
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
                        carorder.FullName = client.UserName;
                        carorder.PassportNumber = client.PassportNumber;
                        carorder.TotalAmount = machine.RentPrice * rentdays;
                        OrderVm.orders.Add(carorder);
                        MessageBoxResult result = MessageBox.Show("Your order has been placed.","", MessageBoxButton.OK, MessageBoxImage.Information);
                        FileExtension.SaveToFile(CarslList);
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
                PrevBtn.Visibility = Visibility.Collapsed;
                NextBtn.Visibility = Visibility.Collapsed;
                ChooseBtn.Visibility = Visibility.Collapsed;
                OrderBtn.Visibility = Visibility.Collapsed;
                RentalCarVm machine = CarslList.Find(m => m.CarID == rentorder.CarID);
                CarImage.Source = new BitmapImage(new Uri(machine.CarImagePath));
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
                ShowOrdersPage.FastDriving(rentorder,_client);
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
