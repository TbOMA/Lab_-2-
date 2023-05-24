using System.Windows;
using System.Windows.Media;
using Lab_2.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using Lab__2_.Services;
using Lab__2_.Database;
using Lab__2_.Views;
using System.Runtime.ConstrainedExecution;

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
        public List<OrderVm> OrderList;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly ApplicationContext _applicationContext;
        public static List<ClientVm> BlackList;
        public delegate bool WithdrawDelegate(decimal amount);
        public CarSelection(ClientVm client,ICarService carService,IClientService clientService)
        {
            InitializeComponent();
            CarslList = carService.GetAll();
            
            _client = client;
            _clientService = clientService;
            _carService = carService;
            _applicationContext = new ApplicationContext();
            _orderService = new OrderService(_applicationContext);
            BlackList = new List<ClientVm>();
            OrderList = _orderService.GetAll();
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray;
            PrintCars(0);
            PrevBtn.IsEnabled = false;
            ClientOrdersPage.Visibility = Visibility.Collapsed;
        }
        public void PrintCars(int current_car)
        {
            //Task 3.5
            Action assignOrderValues = () =>
            {
                CarIdBox.Text = CarslList[current_car].Id.ToString();
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
                MessageBox.Show("This car is currently unavailable", "", MessageBoxButton.OK, MessageBoxImage.Error);
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
            CarslList[current_page-1].IsAvailable = false;
            int rentdays = int.Parse(CarPriceBox.Text);
            var ClientsList = _clientService.GetAll();
            ClientVm client = ClientsList.Find(m => m.Id == _client.Id);
            OrderVm carorder = new OrderVm();
            carorder.Car = CarslList[current_page-1];
            carorder.RentalTime = rentdays;
            carorder.Client = client;
            carorder.TotalAmount = CarslList[current_page-1].RentPrice * rentdays;
            OrderList.Add(carorder);
            _orderService.Create(carorder);
            MessageBox.Show("Your order has been placed.","", MessageBoxButton.OK, MessageBoxImage.Information);
            _carService.UpDate(CarslList[current_page-1]);
            /*OrderVm orderVm = new OrderVm
            {
                RentalTime = 2,
                TotalAmount = 1000,
                IsPaid = false,
                IsApproved = false,
                Car = CarslList[current_page - 1],
                Client = _client,
                RejectionReason = "",
                IsConsidered = false
            };
            OrderList.Add(orderVm);
            _orderService.Create(orderVm);
            MessageBox.Show("Your order has been placed.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            _carService.UpDate(CarslList[current_page - 1]);*/

        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderVm rentorder = OrderList.Find(m => m.Client.Id == _client.Id);
            if (rentorder != null)
            {
                ClientOrdersPage.Visibility = Visibility.Visible;
                ClientOrdersPage.Navigate(new ClientOrdersPage(_carService, _client));
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
                CarImage.Visibility = Visibility.Collapsed;
                Exittn.Visibility = Visibility.Collapsed;
                ShowOrders.Visibility = Visibility.Collapsed;
                OrderBtn.Visibility = Visibility.Collapsed;
                CarPriceBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show($"You have not placed any orders", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Exittn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
