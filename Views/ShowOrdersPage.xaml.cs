using Lab__2_.Database;
using Lab__2_.Services;
using Lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab__2_.Views
{
    /// <summary>
    /// Логика взаимодействия для ShowOrdersPage.xaml
    /// </summary>
    public partial class ShowOrdersPage : Page
    {
        private  readonly ICarService _carService ;
        private readonly IOrderService _orderService;
        private  readonly ApplicationContext _applicationContext;
        public List<OrderVm> OrderList;
        public ShowOrdersPage(ICarService carService)
        {
            InitializeComponent();
            _carService = carService;
            _applicationContext = new ApplicationContext();
            _orderService = new OrderService(_applicationContext);
            OrderList = _orderService.GetAll();
            OrderList.GroupBy(s => s.TotalAmount);
        }
        int current_page = 1;
        public void PrintOrders(int current_order)
        {
            //Task 3.5
            Action assignOrderValues = () =>
            {
                CarIdBox.Text = OrderList[current_order].Car.Id.ToString();
                ClientIdBox.Text = OrderList[current_order].Client.Id.ToString();
                RentTimeBox.Text = OrderList[current_order].RentalTime.ToString();
                FullNameBox.Text = OrderList[current_order].Client.Username;
                PassportBox.Text = OrderList[current_order].Client.PassportNumber.ToString();
                Is_ApprovBox.Text = OrderList[current_order].IsApproved.ToString();
                AmountBox.Text = OrderList[current_order].TotalAmount.ToString();
                IsPaidBox.Text = OrderList[current_order].IsPaid.ToString();
                RejectBox.Text = OrderList[current_order].RejectionReason;
            };
            //
            assignOrderValues();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            label10.Visibility = Visibility.Collapsed;
            label11.Visibility = Visibility.Collapsed;
            ConfirmReject.Visibility = Visibility.Collapsed;
            PrevBtn.IsEnabled = false;
            PrintOrders(0);
            CrashInfo(0);
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (current_page > 0) { PrevBtn.IsEnabled = true; }
            CheckApproved(current_page);
            if (current_page <= OrderList.Count - 1)
            {
                PrintOrders(current_page);
                CrashInfo(current_page);
                current_page++;
                if (current_page >= OrderList.Count || OrderList.Count == 2)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }
        public void CheckApproved(int current_order)
        {
            if (OrderList[current_order].IsApproved)
            {
                ConfirmBtn.IsEnabled = false;
                label10.Visibility = Visibility.Visible;
                label10.Content = "The order has already been confirmed!";
                RejectBox.Text = "";
            }
            else if (!OrderList[current_order].IsApproved && OrderList[current_order].IsConsidered)
            {
                label10.Visibility = Visibility.Visible;
                label10.Content = "This order has been rejected";
                RejectBox.Text = OrderList[current_page].RejectionReason;
            }
            else if (!OrderList[current_order].IsApproved && !OrderList[current_order].IsConsidered)
            {
                RejectBox.Text = "";
                ConfirmBtn.IsEnabled = true;
                label10.Visibility = Visibility.Collapsed;
            }
            ConfirmReject.Visibility = Visibility.Collapsed;
            ConfirmBtn.Visibility = Visibility.Visible;
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --current_page;
            if (current_page < OrderList.Count) { NextBtn.IsEnabled = true; }
            CheckApproved(current_page - 1);
            if (current_page <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            PrintOrders(current_page - 1);
            CrashInfo(current_page - 1);
        }
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfirmBtn.IsEnabled = false;
            label10.Visibility = Visibility.Visible;
            OrderList[current_page - 1].IsApproved = true;
            OrderList[current_page - 1].IsConsidered = true;
            OrderList[current_page - 1].RejectionReason = "-";
            _orderService.UpDate(OrderList[current_page - 1]);
            Is_ApprovBox.Text = OrderList[current_page - 1].IsApproved.ToString();
            RejectBox.Text = OrderList[current_page - 1].RejectionReason.ToString();
        }
        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderList[current_page - 1].IsApproved = false;
            _orderService.UpDate(OrderList[current_page - 1]);
            label9.Visibility = Visibility.Visible;
            label10.Visibility = Visibility.Collapsed;
            RejectBox.Visibility = Visibility.Visible;
            ConfirmBtn.Visibility = Visibility.Collapsed;
            ConfirmReject.Visibility = Visibility.Visible;
            ConfirmReject.IsEnabled = true;
        }
        private void ConfirmReject_Click(object sender, RoutedEventArgs e)
        {
            ConfirmReject.IsEnabled = false;
            label10.Visibility = Visibility.Visible;
            label10.Content = "This order has been rejected";
            OrderList[current_page - 1].RejectionReason = RejectBox.Text;
            OrderList[current_page - 1].IsConsidered = true;
            _orderService.UpDate(OrderList[current_page - 1]);
            Is_ApprovBox.Text = OrderList[current_page - 1].IsApproved.ToString();
        }
        public void CrashInfo(int current_car)
        {
            var carslist = _carService.GetAll();
            RentalCarVm machine = carslist.Find(m => m.Id == OrderList[current_car].Car.Id);
            if (machine.IsDamaged)
            {
                label11.Visibility = Visibility.Visible;
            }
            else
            {
                label11.Visibility = Visibility.Collapsed;
            }
        }
    }
}
