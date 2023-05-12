using System.Windows;
using Lab__2_.Views;
using Lab__2_.Services;
using Lab__2_.Database;

namespace Lab__2_
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        int i = 1;
        private readonly ICarService _carService;
        private readonly IOrderService _orderService;
        private readonly ApplicationContext _applicationContext;
        public AdminForm(ICarService carService)
        {
            InitializeComponent();
            _carService = carService;
            _applicationContext = new ApplicationContext();
            _orderService = new OrderService(_applicationContext);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOrdersPage.Visibility = Visibility.Collapsed;
            ExitBtn.Visibility = Visibility.Visible;
            showorder.Visibility = Visibility.Visible;
        }
        private void showorder_Click(object sender, RoutedEventArgs e)
        {
            var OrderList = _orderService.GetAll();
            if (OrderList.Count > 0)
            {
                ShowOrdersPage.Navigate(new ShowOrdersPage(_carService));
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
            ShowCarsCatalogPage.Navigate(new ShowCarsCatalogPage(_carService));
            ShowOrdersPage.Visibility = Visibility.Visible;
            showocar.Visibility = Visibility.Collapsed;
            showorder.Visibility = Visibility.Collapsed;
        }
    }
}
