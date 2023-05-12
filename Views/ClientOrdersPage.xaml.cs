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
        public List<OrderVm> ClientOrders;
        public List<RentalCarVm> CarslList;
        private ClientVm _client;
        public delegate bool WithdrawDelegate(decimal amount);
        public ClientOrdersPage(ICarService carService,ClientVm client)
        {
            InitializeComponent();
            ClientOrders = new List<OrderVm>();
            _applicationContext = new ApplicationContext();
            _orderService = new OrderService(_applicationContext);
            _carService = carService;
            _clientService = new ClientService(_applicationContext);   
            _client = client;
            CarslList = _carService.GetAll();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var allOrders = _orderService.GetAll();
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders[i].ClientID == _client.ClientID)
                {
                    ClientOrders.Add(allOrders[i]);
                }
            }

            if (ClientOrders != null)
            {
                RentalCarVm machine = CarslList.Find(m => m.CarID == ClientOrders[0].CarID);
                CarImage.Source = new BitmapImage(new Uri(machine.CarImagePath));
                CarPriceBox.Clear();
            }

            /*else if (ClientOrders != null)
            {
                //MessageBoxResult result = MessageBox.Show($"Admin rejects your order : {rentorder.RejectionReason}", "", MessageBoxButton.OK, MessageBoxImage.Error);

            }*/
            else
            {
                MessageBoxResult result = MessageBox.Show($"You have not placed any orders", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (current_page > 0) { PrevBtn.IsEnabled = true; }
            if (current_page <= CarslList.Count - 1)
            {
                CarImage.Source = new BitmapImage(new Uri(CarslList[current_page].CarImagePath));
                current_page++;
                if (current_page >= CarslList.Count)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderVm rentorder = ClientOrders.Find(m => m.ClientID == _client.ClientID);
            var withdrawDelegate = new WithdrawDelegate(Withdraw);
            if (withdrawDelegate(rentorder.TotalAmount))
            {
                rentorder.IsPaid = true;
                PayBtn.IsEnabled = false;
                MessageBox.Show("Payment was successful", "", MessageBoxButton.OK, MessageBoxImage.Information);
                FastDriving(rentorder, _client);
            }
            else
            {
                rentorder.IsPaid = false;
                MessageBoxResult result = MessageBox.Show("Insufficient funds to pay", "", MessageBoxButton.OK, MessageBoxImage.Error);
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

            CarImage.Source = new BitmapImage(new Uri(CarslList[current_page-1].CarImagePath));
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
        public void FastDriving(OrderVm order, ClientVm _client)
        {
            MessageBoxResult fastdriving = MessageBox.Show("You want to drive very fast?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (fastdriving == MessageBoxResult.Yes)
            {
                MessageBox.Show("You crashed the car, pay the fine", "", MessageBoxButton.OK, MessageBoxImage.Information);
                RentalCarVm machine = CarslList.Find(m => m.CarID == order.CarID);
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
