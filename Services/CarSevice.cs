using Lab__2_.Database;
using Lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab__2_.Services
{
    public class CarSevice:ICarService
    {
        //Lab 4,5
        private readonly ApplicationContext _applicationContext;
        public CarSevice(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Create(RentalCarVm rentalCarVm)
        {
            _applicationContext.RentalCars.Add(rentalCarVm);
            _applicationContext.SaveChanges();
        }
        public bool Delete(int id) 
        {
            RentalCarVm dbrecord = _applicationContext.RentalCars.FirstOrDefault(x => x.Id == id);
            if (dbrecord == null) 
            { 
                return false;
            }
            _applicationContext.RentalCars.Remove(dbrecord);
            _applicationContext.SaveChanges();
            return true;
        }
        public bool UpDate(RentalCarVm rentalCarVm) 
        {
            try
            {
                RentalCarVm dbrecord = _applicationContext.RentalCars.FirstOrDefault(x => x.Id == rentalCarVm.Id);
                if (dbrecord ==null)
                {
                    return false;
                }
               // dbrecord.CarID = rentalCarVm.CarID;
                dbrecord.CarName = rentalCarVm.CarName;
                dbrecord.RentPrice = rentalCarVm.RentPrice;
                dbrecord.IsAvailable = rentalCarVm.IsAvailable;
                dbrecord.Description = rentalCarVm.Description;
                dbrecord.CarImagePath = rentalCarVm.CarImagePath;
                dbrecord.IsDamaged = rentalCarVm.IsDamaged;
                _applicationContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public RentalCarVm GetById(int id)
        {
            RentalCarVm dbrecord = _applicationContext.RentalCars.FirstOrDefault(x => x.Id == id);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
        public List<RentalCarVm> GetAll()
        {
            List<RentalCarVm> dbrecord = _applicationContext.RentalCars.ToList();
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
        //
        public RentalCarVm AddCarInCatalog(string ID,string Name,string Description,string Price,string Path)
        {
            var rental_car = new RentalCarVm
            {
                //CarID = int.Parse(ID),
                CarName = Name,
                Description = Description,
                RentPrice = int.Parse(Price),
                CarImagePath = Path,
                IsAvailable = true
            };
            return rental_car;
        }
    }
}
