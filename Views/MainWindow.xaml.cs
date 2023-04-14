using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Lab_2.Models;
using System.Text.Json;
using Lab__2_.Extensions;
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
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            if (!File.Exists("C:\\Users\\Артем\\source\\repos\\2 sem\\Lab_(2)\\bin\\Debug\\net7.0-windows\blacklist.json"))
            {
                string jsonString = File.ReadAllText(@"C:\Users\Артем\source\repos\2 sem\Lab_(2)\bin\Debug\net7.0-windows\blacklist.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var blacklist = JsonSerializer.Deserialize<List<ClientVm>>(jsonString, options);
                for (int i = 0; i < blacklist.Count; i++)
                {
                    if (blacklist[i].ClientID == int.Parse(IdBox.Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void SignUp_Client_Click(object sender, RoutedEventArgs e)
        {
            bool isblacklisted = Isblacklisted();
            if (isblacklisted)
            {
                MessageBoxResult result = MessageBox.Show("You are blacklisted!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
                carselection.Closed += (s, args) => Show();
                Hide();
                CancelMetod();
                carselection.ShowDialog();
            }
        }
        private void SignIn_Client_Click(object sender, RoutedEventArgs e)
        {
            if (Isblacklisted())
            {
                MessageBoxResult result = MessageBox.Show("You are blacklisted!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ClientVm client = ClientVm.clientlist.Find(m => m.ClientID == int.Parse(IdBox.Text));
            if (client != null)
            {
                CarSelection carselection = new CarSelection(client);
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
        private void sign_in_admin_Click(object sender, RoutedEventArgs e)
        {
            AdministratorVm admin = new AdministratorVm();
            string filepath = "admindata.json";
            admin.UserName = IdBox.Text;
            admin.Password = DepositBox.Text;
            string jsonString = File.ReadAllText(filepath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var admins = JsonSerializer.Deserialize<AdministratorVm>(jsonString, options);
            if (admins.Password == admin.Password && admins.UserName == admin.UserName)
            {
                AdminForm adminForm = new AdminForm();
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
        public void CancelMetod()
        {
            Labels.Visibility = Visibility.Collapsed;
            TextBoxs.Visibility= Visibility.Collapsed;
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
            label3.Margin = new Thickness(263,20,0,0);
            cancelButton.Margin = new Thickness(470, -20, 0, 0);
            IdBox.Margin = new Thickness(331, 25, 0, 0);
            DepositBox.Margin = new Thickness(331, 22, 0, 0);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelMetod();
        }
        public void CarsAvailable()
        {
            var carslist = FileExtension.GetCarFromFile("carlist.json");
            for (int i = 0; i < carslist.Count; i++) { carslist[i].IsAvailable = true; }
            FileExtension.WriteCarToFile(carslist, "carlist.json");
        }
        private void Window_Closed(object sender, System.EventArgs e)
        {
            CarsAvailable();
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
            SignUp_Client.Margin = new Thickness(255, -20, 0, 0);
            cancelButton.Margin = new Thickness(450, 300, 0, 0);
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
            SignIn_Client.Margin = new Thickness(270, -20, 0, 0);
            cancelButton.Margin = new Thickness(450, 250, 0, 0);
        }
    }
}
