using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Lab_2.Models;
using System.Text.Json;
namespace Lab__2_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            var carlist = new List<RentalCarVm>() {new RentalCarVm(0001,"BMW", "drift car", true,1200),
                                           new RentalCarVm(0002, "Mercedes_Benz", "business class", false, 4500),
                                           new RentalCarVm(0003,"Toyota_Land_Cruiser", "SUV", true,3000),
                                           new RentalCarVm(0004,"Daewoo_Lanos", "economy class", true,500) };
            if (RentalCarVm.carslist.Count<=0)
            {
                foreach (var car in carlist)
                {
                    RentalCarVm.carslist.Add(car);
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray;
            label1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            label3.Visibility = Visibility.Collapsed;
            label4.Visibility = Visibility.Collapsed;
            usernameTextBox.Visibility = Visibility.Collapsed;
            PassportBox.Visibility = Visibility.Collapsed;
            IdBox.Visibility = Visibility.Collapsed;
            DepositBox.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
            logIN_Admin.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            sign_in.Visibility = Visibility.Collapsed;
        }

        private void loginAdmin_Click(object sender, RoutedEventArgs e)
        {
            loginAdmin.Visibility = Visibility.Collapsed;
            loginClient.Visibility = Visibility.Collapsed;
            label1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            label3.Visibility = Visibility.Visible;
            label3.Content = "Login :";
            label4.Visibility = Visibility.Visible;
            label4.Content = "Password :";
            label4.Margin = new Thickness(255, 250, 0, 0);
            usernameTextBox.Visibility = Visibility.Collapsed;
            PassportBox.Visibility = Visibility.Collapsed;
            IdBox.Visibility = Visibility.Visible;
            DepositBox.Visibility = Visibility.Visible;
            SignUp_Client.Visibility = Visibility.Collapsed;
            logIN_Admin.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            SignIn.Visibility = Visibility.Collapsed;
            SignUp.Visibility = Visibility.Collapsed;
        }
        private void loginClient_Click(object sender, RoutedEventArgs e)
        {
            loginAdmin.Visibility = Visibility.Collapsed;
            loginClient.Visibility = Visibility.Collapsed;
            SignIn.Visibility = Visibility.Visible;
            SignUp.Visibility = Visibility.Visible;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            label1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            label3.Visibility = Visibility.Collapsed;
            label4.Visibility = Visibility.Collapsed;
            usernameTextBox.Visibility = Visibility.Collapsed;
            PassportBox.Visibility = Visibility.Collapsed;
            IdBox.Visibility = Visibility.Collapsed;
            DepositBox.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
            logIN_Admin.Visibility = Visibility.Collapsed;
            loginAdmin.Visibility = Visibility.Visible;
            loginClient.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            SignIn.Visibility = Visibility.Collapsed;
            SignUp.Visibility = Visibility.Collapsed;
            sign_in.Visibility = Visibility.Collapsed;
        }
        private void logIN_Admin_Click(object sender, RoutedEventArgs e)
        {
            AdministratorVm admin = new AdministratorVm();
            string filepath = @"C:\Users\Артем\source\repos\2 sem\Lab_(2)\admindata.json";
            admin.UserName = IdBox.Text; 
            admin.Password = DepositBox.Text;
            string jsonString = File.ReadAllText(filepath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var admins = System.Text.Json.JsonSerializer.Deserialize<AdministratorVm>(jsonString, options);
            if(admins.Password ==admin.Password &&admins.UserName==admin.UserName)
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Incorrect password or login", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            sign_in.Visibility = Visibility.Visible; 
            SignUp.Visibility = Visibility.Collapsed;
            SignIn.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Collapsed;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;
            usernameTextBox.Visibility = Visibility.Visible;
            PassportBox.Visibility = Visibility.Visible;
            IdBox.Visibility = Visibility.Visible;
            DepositBox.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            SignIn.Visibility = Visibility.Collapsed;
            SignUp.Visibility = Visibility.Collapsed;
            sign_in.Visibility = Visibility.Collapsed;
            SignUp_Client.Visibility = Visibility.Visible;


        }

        private void sign_in_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = File.ReadAllText(@"C:\Users\Артем\source\repos\2 sem\Lab_(2)\blacklist.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var blacklist = JsonSerializer.Deserialize<List<ClientVm>>(jsonString, options);
            for (int i = 0; i < blacklist.Count; i++)
            {
                if (blacklist[i].ClientID == int.Parse(IdBox.Text))
                {
                    MessageBoxResult result = MessageBox.Show("You are blacklisted!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ClientVm client = ClientVm.clientlist.Find(m => m.ClientID == int.Parse(IdBox.Text));
            if (client != null)
            {
                CarSelection carselection1 = new CarSelection(client);
                carselection1.Show();
                Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Such a client is not yet registered", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp_Client_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = File.ReadAllText(@"C:\Users\Артем\source\repos\2 sem\Lab_(2)\blacklist.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var blacklist = JsonSerializer.Deserialize<List<ClientVm>>(jsonString, options);
            for (int i = 0; i < blacklist.Count; i++)
            {
                if (blacklist[i].ClientID == int.Parse(IdBox.Text))
                {
                    MessageBoxResult result = MessageBox.Show("You are blacklisted!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ClientVm client = ClientVm.clientlist.Find(m => m.ClientID == int.Parse(IdBox.Text));
            if (client != null)
            {
                MessageBoxResult result = MessageBox.Show("Such a client is already registered", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ClientVm client_ = new ClientVm();
                client_.FullName = usernameTextBox.Text;
                client_.PassportNumber = PassportBox.Text;
                client_.ClientID = int.Parse(IdBox.Text);
                client_.Balance = decimal.Parse(DepositBox.Text);
                ClientVm.clientlist.Add(client_);
                CarSelection carselection = new CarSelection(client_);
                carselection.Show();
                Close();
            }
        }
    }
}
