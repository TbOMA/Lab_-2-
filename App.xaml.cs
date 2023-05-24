using Lab__2_.Database;
using Lab__2_.Services;
using Lab_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab__2_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationContext _applicationContext;
            ICarService _carService;
            IClientService _clientService;
            IOrderService _orderService;
            _applicationContext = new ApplicationContext();
            _clientService = new ClientService(_applicationContext);
            _carService = new CarSevice(_applicationContext);
            _orderService = new OrderService(_applicationContext);
            RentalCarVm car = new RentalCarVm
            {
                IsDamaged = false,
                CarName = "ggr",
                Description = "grgrt",
                CarImagePath = "C:\\Users\\Артем\\source\\repos\\2 sem\\Lab_(2)\\Images\\bmw.jpg",
                IsAvailable = true,
                RentPrice = 500
            };
            //_carService.Create(car);
            ClientVm clientVm = new ClientVm
            {
                Username = "fgr",
                PassportNumber = "65460",
                Balance = 1500
            };
            //_clientService.Create(clientVm);
            OrderVm orderVm = new OrderVm
            {
                RentalTime = 2,
                TotalAmount = 1000,
                IsPaid = false,
                IsApproved = false,
                Car = car,
                CarID = car.Id,
                Client = clientVm,
                ClientID = clientVm.Id,
                RejectionReason = "",
                IsConsidered = false
            };
            _orderService.Create(orderVm);
            base.OnStartup(e);  

        }*/
        
    }
}
