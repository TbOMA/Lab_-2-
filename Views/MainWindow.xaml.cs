using System.IO;
using System.Windows;
using Lab_2.Models;
using System.Text.Json;
using Lab__2_.Extensions;
using System;
using Lab__2_.Services;
using System.Windows.Media;
using Lab__2_.Database;
using System.Collections.Generic;

namespace Lab__2_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly ApplicationContext _applicationContext;
        public List<ClientVm> ClientsList;
        public MainWindow()
        {
            InitializeComponent();
            //Task 2.10
            object[] objects = new object[6];
            // Створення об'єктів різних класів і запис їх в масив
            objects[0] = new ClientVm();
            objects[1] = new CarsVm();
            objects[2] = new RentalCarVm();
            objects[3] = new RentalFormVm();
            objects[4] = new OrderVm();
            objects[5] = new AdministratorVm();
            //

            _applicationContext = new ApplicationContext();
            _carService = new CarSevice(_applicationContext);
            _clientService = new ClientService(_applicationContext);
            _orderService = new OrderService(_applicationContext);
            ClientsList = _clientService.GetAll();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*var client_ = new ClientVm { ClientID = 11, Balance = 5000 };
            CarSelection carselection = new CarSelection(client_, _carService, _clientService);
            //Task 3.2
            carselection.Closed += (s, args) => Show();
            //
            Hide();
            CancelMetod();
            carselection.ShowDialog();*/

            Background = Brushes.LightGray;
            Labels.Visibility = Visibility.Collapsed;
            TextBoxs.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            sign_in_admin.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            SignUpClientChoose.Visibility = Visibility.Collapsed;
            SignInClientChoose.Visibility = Visibility.Collapsed;
            SignIn_Client.Visibility = Visibility.Collapsed;
        }
        public bool Isblacklisted()
        {
            if (File.Exists(FileExtension.BLACKLIST_FILE_PATH))
            {
                var blacklist =  FileExtension.GetBlacklistFile();
                for (int i = 0; i < blacklist.Count; i++)
                {
                    if (blacklist[i].PassportNumber == PassportBox.Text)
                    {
                        MessageBox.Show("You are blacklisted!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                }
            }
            return false;
        }
        private void SignUp_Client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Isblacklisted()) return;
                ClientVm client = ClientsList.Find(m => m.PassportNumber == PassportBox.Text);
                if (client != null)
                {
                    MessageBoxResult result = MessageBox.Show("Such a client is already registered", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ClientVm client_ = new ClientVm();
                    client_.Username = usernameTextBox.Text;
                    client_.PassportNumber = PassportBox.Text;
                    //client_.ClientID = int.Parse(IdBox.Text);
                    client_.Balance = decimal.Parse(DepositBox.Text);
                    ClientsList.Add(client_);
                    _clientService.Create(client_);
                    CarSelection carselection = new CarSelection(client_,_carService, _clientService);
                    //Task 3.2
                    carselection.Closed += (s, args) => Show();
                    //
                    Hide();
                    CancelMetod();
                    carselection.ShowDialog();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Input format error", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void SignIn_Client_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Isblacklisted()) return;
                //Task 3.5
                ClientVm client = ClientsList.Find(m => m.PassportNumber == IdBox.Text);
                //
                if (client != null)
                {
                    CarSelection carselection = new CarSelection(client, _carService, _clientService);
                    carselection.Closed += (s, args) => Show();
                    Hide();
                    CancelMetod();
                    carselection.ShowDialog();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Such a client is not yet registered", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Input format error", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void sign_in_admin_Click(object sender, RoutedEventArgs e)
        {
            AdministratorVm admin = new AdministratorVm();
            string filepath = "admindata.json";
            admin.Username = IdBox.Text;
            admin.Password = DepositBox.Text;
            string jsonString = File.ReadAllText(filepath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var admins_data = JsonSerializer.Deserialize<AdministratorVm>(jsonString, options);
            if (admins_data.Password == admin.Password && admins_data.Username == admin.Username)
            {
                AdminForm adminForm = new AdminForm(_carService);
                adminForm.Closed += (s, args) => Show();
                Hide();
                CancelMetod();
                adminForm.ShowDialog();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Incorrect password or login", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelMetod();
        }
        public void CarsAvailable()
        {
            List<RentalCarVm> rentalCarList = _carService.GetAll();
            for (int i = 0; i < rentalCarList.Count; ++i)
            {   
                rentalCarList[i].IsAvailable = true;
                rentalCarList[i].IsDamaged = false;
                _carService.UpDate(rentalCarList[i]);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            CarsAvailable();
        }
        public void CancelMetod()
        {
            Labels.Visibility = Visibility.Collapsed;
            TextBoxs.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
            sign_in_admin.Visibility = Visibility.Collapsed;
            SignIn_Client.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
            SignInClientChoose.Visibility = Visibility.Collapsed;
            SignUpClientChoose.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            Administrator.Visibility = Visibility.Visible;
            Client.Visibility = Visibility.Visible;
            IdBox.Text = "";
            usernameTextBox.Text = "";
            PassportBox.Text = "";
            DepositBox.Text = "";
            label3.Margin = new Thickness(263, 20, 0, 0);
            cancelButton.Margin = new Thickness(470, -20, 0, 0);
            IdBox.Margin = new Thickness(331, 25, 0, 0);
            DepositBox.Margin = new Thickness(331, 22, 0, 0);
        }

        private void Administrator_Click(object sender, RoutedEventArgs e)
        {
            Labels.Visibility = Visibility.Visible;
            TextBoxs.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            label3.Visibility = Visibility.Visible;
            label3.Content = "Login :";
            label4.Visibility = Visibility.Visible;
            label4.Content = "Password :";
            label3.Margin = new Thickness(255, 170, 0, 0);
            label4.Margin = new Thickness(235, 20, 0, 0);
            usernameTextBox.Visibility = Visibility.Collapsed;
            PassportBox.Visibility = Visibility.Collapsed;
            IdBox.Visibility = Visibility.Visible;
            DepositBox.Visibility = Visibility.Visible;
            SignUp_Client.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            SignUp_Client.Visibility = Visibility.Collapsed;
            sign_in_admin.Visibility = Visibility.Visible;
            Administrator.Visibility = Visibility.Collapsed;
            Client.Visibility = Visibility.Collapsed;
            sign_in_admin.Margin = new Thickness(255, 300, 0, 0);
            cancelButton.Margin = new Thickness(470, -24, 0, 0);
            IdBox.Margin = new Thickness(300, 175, 0, 0);
            DepositBox.Margin = new Thickness(300,20,0,0);

        }
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Administrator.Visibility = Visibility.Collapsed;
            Client.Visibility = Visibility.Collapsed;
            SignInClientChoose.Visibility = Visibility.Visible;
            SignUpClientChoose.Visibility = Visibility.Visible;
        } 
        private void SignUpClientChoose_Click(object sender, RoutedEventArgs e)
        {
            Labels.Visibility = Visibility.Visible;
            TextBoxs.Visibility = Visibility.Visible;
            usernameTextBox.Visibility = Visibility.Visible;
            PassportBox.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            label3.Content = "Client ID:";
            label4.Visibility = Visibility.Visible;
            label4.Content = "Deposit amount:";
            SignUpClientChoose.Visibility = Visibility.Collapsed;
            SignInClientChoose.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            SignUp_Client.Visibility = Visibility.Visible;
            SignUp_Client.Margin = new Thickness(255, -25, 0, 0);
            cancelButton.Margin = new Thickness(470, 310, 0, 0);
            label4.Margin = new Thickness(223, 20, 0, 0);
            DepositBox.Visibility = Visibility.Visible;
            DepositBox.Margin = new Thickness(331, 20, 0, 0);
        }
        private void SignInClientChoose_Click(object sender, RoutedEventArgs e)
        {
            Labels.Visibility = Visibility.Visible;
            TextBoxs.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            label4.Visibility = Visibility.Collapsed;
            label3.Content = "Client ID:";
            usernameTextBox.Visibility = Visibility.Collapsed;
            PassportBox.Visibility = Visibility.Collapsed;
            DepositBox.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            SignIn_Client.Visibility = Visibility.Visible;
            SignInClientChoose.Visibility = Visibility.Collapsed;
            SignUpClientChoose.Visibility = Visibility.Collapsed;
            label3.Margin = new Thickness(240, 175, 0, 0);
            IdBox.Margin = new Thickness(300, 175, 0, 0);
            SignIn_Client.Margin = new Thickness(270, -25, 0, 0);
            cancelButton.Margin = new Thickness(450, 250, 0, 0);
        }
    }
}
