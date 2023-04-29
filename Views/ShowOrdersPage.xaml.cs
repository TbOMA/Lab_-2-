using Lab__2_.Extensions;
using Lab__2_.Services;
using Lab_2.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab__2_.Views
{
    /// <summary>
    /// Логика взаимодействия для ShowOrdersPage.xaml
    /// </summary>
    public partial class ShowOrdersPage : Page
    {
        public static List<ClientVm> BlackList;
        private readonly ICarService carService;

        public ShowOrdersPage(ICarService carService)
        {
            InitializeComponent();
            BlackList = new List<ClientVm>();
            this.carService = carService;
        }
        int current_page = 1;
        public void PrintOrders(int current_order)
        {
            //Task 3.5
            Action assignOrderValues = () =>
            {
                CarIdBox.Text = OrderVm.orders[current_order].CarID.ToString();
                ClientIdBox.Text = OrderVm.orders[current_order].ClientID.ToString();
                RentTimeBox.Text = OrderVm.orders[current_order].RentalTime.ToString();
                FullNameBox.Text = OrderVm.orders[current_order].FullName.ToString();
                PassportBox.Text = OrderVm.orders[current_order].PassportNumber.ToString();
                Is_ApprovBox.Text = OrderVm.orders[current_order].IsApproved.ToString();
                AmountBox.Text = OrderVm.orders[current_order].TotalAmount.ToString();
                IsPaidBox.Text = OrderVm.orders[current_order].IsPaid.ToString();
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
            if (current_page <= OrderVm.orders.Count - 1)
            {
                PrintOrders(current_page);
                CrashInfo(current_page);
                current_page++;
                if (current_page >= OrderVm.orders.Count || OrderVm.orders.Count == 2)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }
        public void CheckApproved(int current_order)
        {
            if (OrderVm.orders[current_order].IsApproved)
            {
                ConfirmBtn.IsEnabled = false;
                label10.Visibility = Visibility.Visible;
                label10.Content = "The order has already been confirmed!";
                RejectBox.Text = "";
            }
            else if (!OrderVm.orders[current_order].IsApproved && OrderVm.orders[current_order].IsConsidered)
            {
                label10.Visibility = Visibility.Visible;
                label10.Content = "This order has been rejected";
                RejectBox.Text = OrderVm.orders[current_page].RejectionReason;
            }
            else if (!OrderVm.orders[current_order].IsApproved && !OrderVm.orders[current_order].IsConsidered)
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
            if (current_page < OrderVm.orders.Count) { NextBtn.IsEnabled = true; }
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
            OrderVm.orders[current_page - 1].IsApproved = true;
            OrderVm.orders[current_page - 1].RejectionReason = "-";
            Is_ApprovBox.Text = OrderVm.orders[current_page - 1].IsApproved.ToString();
            RejectBox.Text = OrderVm.orders[current_page - 1].RejectionReason.ToString();
        }
        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderVm.orders[current_page - 1].IsApproved = false;
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
            OrderVm.orders[current_page - 1].RejectionReason = RejectBox.Text;
            OrderVm.orders[current_page - 1].IsConsidered = true;
            Is_ApprovBox.Text = OrderVm.orders[current_page - 1].IsApproved.ToString();
        }
        public static void FastDriving(OrderVm order, ClientVm _client)
        {
            MessageBoxResult fastdriving = MessageBox.Show("You want to drive very fast?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (fastdriving == MessageBoxResult.Yes)
            {
                fastdriving = MessageBox.Show("You crashed the car, pay the fine", "", MessageBoxButton.OK, MessageBoxImage.Information);
                var carslist = FileExtension.GetCarFromFile("carlist.json");
                //Task 3.5
                RentalCarVm machine = carslist.Find(m => m.CarID == order.CarID);
                //
                machine.IsDamaged = true;
                FileExtension.SaveToFile(carslist);
                if ((_client.Balance = _client.Balance - (order.TotalAmount * 2)) < 0)
                {
                    MessageBox.Show("You don't have enough funds, you are blacklisted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    BlackList = FileExtension.GetBlacklistFile();
                    BlackList.Add(_client);
                    FileExtension.SaveToFile(BlackList);
                }
            }
        }
        public void CrashInfo(int current_car)
        {
            var carslist = FileExtension.GetCarFromFile("carlist.json");
            RentalCarVm machine = carslist.Find(m => m.CarID == OrderVm.orders[current_car].CarID);
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
