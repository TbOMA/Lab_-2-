using Lab__2_.Services;
using Lab_2.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab__2_.Views
{
    /// <summary>
    /// Логика взаимодействия для ShowCarsCatalogPage.xaml
    /// </summary>
    public partial class ShowCarsCatalogPage : Page
    {
        public static List<RentalCarVm> CarsList;
        int current_page = 1;
        private readonly ICarService _carService;
        public ShowCarsCatalogPage(ICarService carService)
        {
            InitializeComponent();
            CarsList = carService.GetAll();
            _carService = carService;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Background = Brushes.LightGray;
            PrintCars(0);
            PrevBtn.IsEnabled = false;
            CarPathBox.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Collapsed;
            AddCarBtn.Visibility = Visibility.Collapsed;
        }
        public void PrintCars(int current_car)
        {
            //Task 3.5
            Action assignOrderValues = () =>
            {
                CarIdBox.Text = CarsList[current_car].CarID.ToString();
                CarNameBox.Text = CarsList[current_car].CarName.ToString();
                CarDesBox.Text = CarsList[current_car].Description.ToString();
                CarIsAvBox.Text = CarsList[current_car].IsAvailable.ToString();
                CarPriceBox.Text = CarsList[current_car].RentPrice.ToString();
                CarImage.Source = new BitmapImage(new Uri(CarsList[current_car].CarImagePath));
            };
            //
            assignOrderValues();
        }
        private void AddCarBtn_Click(object sender, RoutedEventArgs e)
        {
            RentalCarVm rentalCarVm;
            rentalCarVm = _carService.AddCarInCatalog(CarIdBox.Text, CarNameBox.Text, CarDesBox.Text, CarPriceBox.Text, CarPathBox.Text);
            CarsList.Add(rentalCarVm);
            _carService.Create(rentalCarVm);
            MessageBox.Show("The car was added to the catalog", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            --current_page;
            if (current_page < CarsList.Count) { NextBtn.IsEnabled = true; }
            if (current_page <= 1)
            {
                PrevBtn.IsEnabled = false;
            }
            PrintCars(current_page-1);
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (current_page > 0) { PrevBtn.IsEnabled = true; }
            if (current_page <= CarsList.Count - 1)
            {
                PrintCars(current_page);
                current_page++;
                if (current_page >= CarsList.Count)
                {
                    NextBtn.IsEnabled = false;
                }
            }
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Task 3.5
                int edit_car_index = CarsList.FindIndex(m => m.CarID == int.Parse(CarIdBox.Text));
                //
                var rentalCar = _carService.GetById(CarsList[edit_car_index].Id);
                rentalCar.CarID = int.Parse(CarIdBox.Text);
                rentalCar.CarName = CarNameBox.Text;
                rentalCar.Description = CarDesBox.Text;
                rentalCar.RentPrice = int.Parse(CarPriceBox.Text);
                rentalCar.IsAvailable = bool.Parse(CarIsAvBox.Text);
                rentalCar.CarImagePath = CarsList[edit_car_index].CarImagePath;
                rentalCar.Id = CarsList[edit_car_index].Id;
                CarsList[edit_car_index] = rentalCar;
                _carService.UpDate(rentalCar);
                MessageBox.Show("Changes have been saved", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Input format error", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            CarPathBox.Visibility = Visibility.Collapsed;
            CarImage.Visibility = Visibility.Visible;
            CarIdBox.Width = 120;
            CarNameBox.Width = 120;
            CarDesBox.Width = 120;
            CarPathBox.Width = 120;
            CarPriceBox.Width = 120;
            label4.Content = "Is available now : ";
            PrevBtn.Visibility = Visibility.Visible;
            NextBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Visible;
            BackBtn.Visibility = Visibility.Collapsed;
            AddCarBtn.Visibility = Visibility.Collapsed;
            PrevBtn.IsEnabled = false;
            AddCar.Visibility = Visibility.Visible;
            PrintCars(0);
        }
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            CarPathBox.Visibility = Visibility.Visible;
            CarImage.Visibility = Visibility.Collapsed;
            CarIdBox.Width = 400;
            CarNameBox.Width = 400;
            CarDesBox.Width = 400;
            CarPathBox.Width = 400;
            CarPriceBox.Width = 400;
            CarIdBox.Clear();
            CarNameBox.Clear();
            CarDesBox.Clear();
            CarIsAvBox.Clear();
            CarPriceBox.Clear();
            CarPathBox.Clear();
            label4.Content = "Car image path : ";
            PrevBtn.Visibility = Visibility.Collapsed;
            NextBtn.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Collapsed;
            AddCarBtn.Visibility = Visibility.Visible;
            BackBtn.Visibility = Visibility.Visible;
            AddCar.Visibility = Visibility.Collapsed;
        }
    }
}
