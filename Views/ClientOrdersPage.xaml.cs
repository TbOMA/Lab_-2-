using Lab__2_.Database;
using Lab__2_.Extensions;
using Lab__2_.Services;
using Lab_2.Models;
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
using static Lab__2_.CarSelection;

namespace Lab__2_.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientOrdersPage.xaml
    /// </summary>
    public partial class ClientOrdersPage : Page
    {
        int current_page = 1;
        private readonly IOrderService _orderService;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly ApplicationContext _applicationContext;
        public List<RentalCarVm> CarslList;
        public List<OrderVm> ClientOrders;
        private ClientVm _client;
        public delegate bool WithdrawDelegate(decimal amount);
        public ClientOrdersPage(ICarService carService,ClientVm client)
        {
            InitializeComponent();
            _applicationContext = new ApplicationContext();
            _orderService = new OrderService(_applicationContext);
            _carService = carService;
            _clientService = new ClientService(_applicationContext);   
            _client = client;
            CarslList = _carService.GetAll();
            ClientOrders = new List<OrderVm>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            label6.Visibility = Visibility.Collapsed;
            var allOrders = _orderService.GetAll();
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders[i].Client.Id == _client.Id)
                {
                    ClientOrders.Add(allOrders[i]);
                }
            }
            CheckIsApproved(0);
            if (ClientOrders != null)
            {
                RentalCarVm machine = CarslList.Find(m => m.Id == ClientOrders[0].Car.Id);
                PrintOrders(machine.Id);
                PrevBtn.IsEnabled = false;
                CheckIsPaid(0);
            }
        }

        private void CheckIsApproved(int current_page)
        {
           if (ClientOrders != null && !ClientOrders[current_page].IsApproved)
            {
                label6.Visibility = Visibility.Visible;
                PayBtn.IsEnabled = false;
                label6.Content = $"Admin rejects your order : {ClientOrders[current_page].RejectionReason}";
            }
            else
            {
                label6.Visibility = Visibility.Collapsed;
                //label6.Content = "You have not placed any orders";
            }
        }

        private void CheckIsPaid(int current_page)
        {
            if (ClientOrders[current_page].IsPaid)
            {
                PayBtn.IsEnabled = false;
            }
            else
            {
                PayBtn.IsEnabled = true;
            }
        }

        public void PrintOrders(int carId)
        {
            var rentalCar = CarslList.Find(m => m.Id == carId);
            CarIdBox.Text = rentalCar.Id.ToString();
            CarNameBox.Text = rentalCar.CarName.ToString();
            CarDesBox.Text = rentalCar.Description.ToString();
            CarIsAvBox.Text = rentalCar.IsAvailable.ToString();
            CarPriceBox.Text = rentalCar.RentPrice.ToString();
            CarImage.Source = new BitmapImage(new Uri(rentalCar.CarImagePath));
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (current_page > 0) { PrevBtn.IsEnabled = true; }
            if (current_page <= ClientOrders.Count - 1)
            {
                PrintOrders(ClientOrders[current_page].Car.Id);
                current_page++;
                if (current_page >= ClientOrders.Count || ClientOrders.Count == 2)
                {
                    NextBtn.IsEnabled = false;
                }
                CheckIsPaid(current_page-1);
                CheckIsApproved(current_page-1);

            }
        }
        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientOrders[current_page-1].IsPaid)
            {
                var withdrawDelegate = new WithdrawDelegate(Withdraw);
                if (withdrawDelegate(ClientOrders[current_page - 1].TotalAmount))
                {
                    ClientOrders[current_page - 1].IsPaid = true;
                    PayBtn.IsEnabled = false;
                    MessageBox.Show("Payment was successful", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    FastDriving(ClientOrders[current_page - 1], _client);
                }
                else
                {
                    ClientOrders[current_page - 1].IsPaid = false;
                    MessageBox.Show("Insufficient funds to pay", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _orderService.UpDate(ClientOrders[current_page - 1]);
            }
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --current_page;
            if (current_page < CarslList.Count) { NextBtn.IsEnabled = true; }
            if (current_page <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            PrintOrders(ClientOrders[current_page-1].Car.Id);
            CheckIsPaid(current_page - 1);
            CheckIsApproved(current_page-1);
        }
        public bool Withdraw(decimal amount)
        {
            if (_client.Balance >= amount)
            {
                var ClientList = _clientService.GetAll();
                _client = ClientList.Find(x => x.Id == _client.Id);
                _client.Balance -= amount;
                _clientService.UpDate(_client);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FastDriving(OrderVm order, ClientVm _client)
        {
            MessageBoxResult fastdriving = MessageBox.Show("You want to drive very fast?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (fastdriving == MessageBoxResult.Yes)
            {
                MessageBox.Show("You crashed the car, pay the fine", "", MessageBoxButton.OK, MessageBoxImage.Information);
                RentalCarVm machine = CarslList.Find(m => m.Id == order.Car.Id);
                machine = _carService.GetById(machine.Id);
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
    }
}
