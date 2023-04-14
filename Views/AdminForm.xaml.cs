using Lab_2.Models;
using System.Text.Json;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System.Collections.Generic;
using Lab__2_.Extensions;

namespace Lab__2_
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        int i = 1;
        public static List<ClientVm> blacklist;
        public AdminForm()
        {
            InitializeComponent();
            blacklist =  new List<ClientVm>();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfirmBtn.Visibility = Visibility.Collapsed;
            PrevBtn.Visibility = Visibility.Collapsed;
            NextBtn.Visibility = Visibility.Collapsed;
            ExitBtn.Visibility = Visibility.Collapsed;
            RejectBtn.Visibility = Visibility.Collapsed;
            labels.Visibility = Visibility.Collapsed;
            textboxs.Visibility = Visibility.Collapsed;
            ConfirmReject.Visibility = Visibility.Collapsed;
        }
        private void showorder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderVm.orders.Count > 0)
            {
                ConfirmBtn.Visibility = Visibility.Visible;
                PrevBtn.Visibility = Visibility.Visible;
                PrevBtn.IsEnabled = false;
                NextBtn.Visibility = Visibility.Visible;
                ExitBtn.Visibility = Visibility.Visible;
                RejectBtn.Visibility = Visibility.Visible;
                showorder.Visibility = Visibility.Collapsed;
                labels.Visibility = Visibility.Visible;
                label9.Visibility = Visibility.Collapsed;
                label10.Visibility = Visibility.Collapsed; 
                textboxs.Visibility = Visibility.Visible;
                RejectBox.Visibility = Visibility.Collapsed;
                CarIdBox.Text = OrderVm.orders[0].CarID.ToString();
                ClientIdBox.Text = OrderVm.orders[0].ClientID.ToString();
                RentTimeBox.Text = OrderVm.orders[0].RentalTime.ToString();
                FullNameBox.Text = OrderVm.orders[0].FullName.ToString();
                PassportBox.Text = OrderVm.orders[0].PassportNumber.ToString();
                Is_ApprovBox.Text = OrderVm.orders[0].IsApproved.ToString();
                AmountBox.Text = OrderVm.orders[0].TotalAmount.ToString();
                IsPaidBox.Text = OrderVm.orders[0].IsPaid.ToString();
                CrashInfo();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("There are no orders now", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0) { PrevBtn.IsEnabled = true; }
            if (OrderVm.orders[i].IsApproved)
            {
                ConfirmBtn.IsEnabled = false;
                label10.Visibility = Visibility.Visible;
                label10.Content = "The order has already been confirmed!";
                RejectBox.Text = "";
            }
            else if (!OrderVm.orders[i].IsApproved && OrderVm.orders[i].IsConsidered)
            {
                label10.Visibility = Visibility.Visible;
                label10.Content = "This order has been rejected";
                RejectBox.Text = OrderVm.orders[i].RejectionReason;
            }
            else if (!OrderVm.orders[i].IsApproved && !OrderVm.orders[i].IsConsidered)
            {
                RejectBox.Text = "";
                ConfirmBtn.IsEnabled = true;
                label10.Visibility = Visibility.Collapsed;
            }
            ConfirmReject.Visibility = Visibility.Collapsed;
            ConfirmBtn.Visibility = Visibility.Visible;
            if (i <= OrderVm.orders.Count - 1)
            {
                CarIdBox.Text = OrderVm.orders[i].CarID.ToString();
                ClientIdBox.Text = OrderVm.orders[i].ClientID.ToString();
                RentTimeBox.Text = OrderVm.orders[i].RentalTime.ToString();
                FullNameBox.Text = OrderVm.orders[i].FullName.ToString();
                PassportBox.Text = OrderVm.orders[i].PassportNumber.ToString();
                Is_ApprovBox.Text = OrderVm.orders[i].IsApproved.ToString();
                AmountBox.Text = OrderVm.orders[i].TotalAmount.ToString();
                IsPaidBox.Text = OrderVm.orders[i].IsPaid.ToString();
                CrashInfo();
                i++;
                if (i >= OrderVm.orders.Count || OrderVm.orders.Count == 2)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --i;
            if (i < OrderVm.orders.Count) { NextBtn.IsEnabled = true; }
            if (OrderVm.orders[i-1].IsApproved)
            {
                ConfirmBtn.IsEnabled = false;
                label10.Visibility = Visibility.Visible;
                label10.Content = "The order has already been confirmed!";
                RejectBox.Text = "";
            }
            else if (!OrderVm.orders[i - 1].IsApproved && OrderVm.orders[i - 1].IsConsidered)
            {
                label10.Visibility = Visibility.Visible; 
                label10.Content = "This order has been rejected";
                RejectBox.Text = OrderVm.orders[i - 1].RejectionReason;
            }
            else if (!OrderVm.orders[i-1].IsApproved && !OrderVm.orders[i-1].IsConsidered)
            {
                RejectBox.Text = "";
                ConfirmBtn.IsEnabled = true;
                label10.Visibility = Visibility.Collapsed;
            }
            if (i <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            ConfirmReject.Visibility = Visibility.Collapsed;
            ConfirmBtn.Visibility = Visibility.Visible;
            CarIdBox.Text = OrderVm.orders[i-1].CarID.ToString();
            ClientIdBox.Text = OrderVm.orders[i-1].ClientID.ToString();
            RentTimeBox.Text = OrderVm.orders[i - 1].RentalTime.ToString();
            FullNameBox.Text = OrderVm.orders[i - 1].FullName.ToString();
            PassportBox.Text = OrderVm.orders[i - 1].PassportNumber.ToString();
            Is_ApprovBox.Text = OrderVm.orders[i - 1].IsApproved.ToString();
            AmountBox.Text = OrderVm.orders[i - 1].TotalAmount.ToString();
            IsPaidBox.Text = OrderVm.orders[i - 1].IsPaid.ToString();
            CrashInfo();
        }
        private void ConfirmBtn_Click( object sender, RoutedEventArgs e )
        {
            ConfirmBtn.IsEnabled = false;
            label10.Visibility = Visibility.Visible;
            OrderVm.orders[i-1].IsApproved = true;
            OrderVm.orders[i - 1].RejectionReason = "-";
            Is_ApprovBox.Text = OrderVm.orders[i-1].IsApproved.ToString();
            RejectBox.Text = OrderVm.orders[i - 1].RejectionReason.ToString();
        }
        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderVm.orders[i-1].IsApproved = false;
            label9.Visibility = Visibility.Visible;
            label10.Visibility = Visibility.Collapsed;
            RejectBox.Visibility = Visibility.Visible;
            ConfirmBtn.Visibility = Visibility.Collapsed;
            ConfirmReject.Visibility = Visibility.Visible;
            ConfirmReject.IsEnabled = true;
        }
        private void ConfirmReject_Click(object sender, RoutedEventArgs e)
        {   ConfirmReject.IsEnabled = false;
            label10.Visibility = Visibility.Visible;
            label10.Content = "This order has been rejected";
            OrderVm.orders[i - 1].RejectionReason = RejectBox.Text;
            OrderVm.orders[i-1].IsConsidered = true;
            Is_ApprovBox.Text = OrderVm.orders[i-1].IsApproved.ToString();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static void FastDriving(OrderVm order,ClientVm _client)
        {
            MessageBoxResult fastdriving = MessageBox.Show("You want to drive very fast?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (fastdriving == MessageBoxResult.Yes)
            {
                var carslist = FileExtension.GetCarFromFile("carlist.json");
                RentalCarVm machine = carslist.Find(m => m.CarID == order.CarID);
                machine.IsDamaged = true;
                fastdriving = MessageBox.Show("You crashed the car, pay the fine", "", MessageBoxButton.OK, MessageBoxImage.Information);
                if ((_client.Balance = _client.Balance - (order.TotalAmount * 2)) < 0)
                {
                    MessageBox.Show("You don't have enough funds, you are blacklisted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    blacklist = FileExtension.GetBlacklistFile();
                    blacklist.Add(_client);
                    FileExtension.AddInBlacklistFile(blacklist);
                }
            }
        }
        public void CrashInfo()
        {
            var carslist = FileExtension.GetCarFromFile("carlist.json");
            RentalCarVm machine = carslist.Find(m => m.CarID == OrderVm.orders[i - 1].CarID);
            if (machine.IsDamaged)
            {
                MessageBoxResult crashed = MessageBox.Show($"A client who has such an ID {OrderVm.orders[i - 1].ClientID} crashed the car",
                    "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
