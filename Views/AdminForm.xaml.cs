using Lab_2.Models;
using System.Windows;
using Lab__2_.Views;
using Lab__2_.Services;

namespace Lab__2_
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        int i = 1;
        private readonly ICarService carService;
        public AdminForm(ICarService carService)
        {
            InitializeComponent();
            this.carService = carService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOrdersPage.Visibility = Visibility.Collapsed;
            ExitBtn.Visibility = Visibility.Visible;
            showorder.Visibility = Visibility.Visible;
        }
        private void showorder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderVm.orders.Count > 0)
            {
                ShowOrdersPage.Navigate(new ShowOrdersPage(carService));
                ShowOrdersPage.Visibility = Visibility.Visible;
                showocar.Visibility = Visibility.Collapsed;
                showorder.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("There are no orders now", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showocar_Click(object sender, RoutedEventArgs e)
        {
            ShowCarsCatalogPage.Navigate(new ShowCarsCatalogPage(carService));
            ShowOrdersPage.Visibility = Visibility.Visible;
            showocar.Visibility = Visibility.Collapsed;
            showorder.Visibility = Visibility.Collapsed;
        }
    }
}
