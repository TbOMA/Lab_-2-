using System.Windows;
using System.Windows.Media;
using Lab_2.Models;
using Lab__2_.Extensions;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using Lab__2_.Services;
using Lab__2_.Database;

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
            for (int k = 0; k < OrderList.Count; k++)
            {
                if (OrderList[k].ClientID == _client.ClientID)
                {
                    MessageBox.Show("You have already placed an order", "", MessageBoxButton.OK, MessageBoxImage.Error);
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
            ClientVm client = ClientsList.Find(m => m.ClientID == _client.ClientID);
            OrderVm carorder = new OrderVm();
            carorder.CarID = CarslList[current_page-1].CarID;
            carorder.RentalTime = rentdays;
            carorder.ClientID = client.ClientID;
            carorder.FullName = client.Username;
            carorder.PassportNumber = client.PassportNumber;
            carorder.TotalAmount = CarslList[current_page-1].RentPrice * rentdays;
            OrderList.Add(carorder);
            _orderService.Create(carorder);
            MessageBox.Show("Your order has been placed.","", MessageBoxButton.OK, MessageBoxImage.Information);
            _carService.UpDate(CarslList[current_page-1]);
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {   


            OrderVm rentorder =  OrderList.Find(m => m.ClientID == _client.ClientID);
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
        public bool Withdraw(decimal amount)
        {
            if (_client.Balance >= amount)
            {
                var ClientList = _clientService.GetAll();
                _client = ClientList.Find(x => x.ClientID == _client.ClientID);
                _client.Balance -= amount;
                _clientService.UpDate(_client);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderVm rentorder = OrderList.Find(m => m.ClientID == _client.ClientID);
            var withdrawDelegate = new WithdrawDelegate(Withdraw);
            if (withdrawDelegate(rentorder.TotalAmount))
            {
                rentorder.IsPaid = true;
                PayBtn.IsEnabled = false;
                MessageBox.Show("Payment was successful", "", MessageBoxButton.OK, MessageBoxImage.Information);
                FastDriving(rentorder,_client);
            }
            else
            {
                rentorder.IsPaid = false;
                MessageBoxResult result = MessageBox.Show("Insufficient funds to pay", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void FastDriving(OrderVm order, ClientVm _client)
        {
            MessageBoxResult fastdriving = MessageBox.Show("You want to drive very fast?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (fastdriving == MessageBoxResult.Yes)
            {
                MessageBox.Show("You crashed the car, pay the fine", "", MessageBoxButton.OK, MessageBoxImage.Information);
                RentalCarVm machine = CarslList.Find(m => m.CarID == order.CarID);
                machine =  _carService.GetById(machine.Id);
                machine.IsDamaged = true;
                _carService.UpDate(machine);
                var withdrawDelegate = new WithdrawDelegate(Withdraw);

                if (withdrawDelegate(order.TotalAmount * 2))
                {
                    MessageBox.Show("You don't have enough funds, you are blacklisted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    BlackList = FileExtension.GetBlacklistFile();
                    BlackList.Add(_client);
                    FileExtension.SaveToFile(BlackList);
                }
            }
        }
        private void Exittn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
