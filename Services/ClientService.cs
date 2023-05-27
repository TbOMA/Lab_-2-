using Lab__2_.Database;
using Lab_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab__2_.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationContext _applicationContext;
        public ClientService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Create(ClientVm clientVm)
        {
            _applicationContext.Clients.Add(clientVm);
            _applicationContext.SaveChanges();
        }
        public bool Delete(int id)
        {
            ClientVm dbrecord = _applicationContext.Clients.Include(a => a.ClientOrders).FirstOrDefault(x => x.Id == id);
            if (dbrecord == null)
            {
                return false;
            }
            _applicationContext.Clients.Remove(dbrecord);
            _applicationContext.SaveChanges();
            return true;
        }
        public bool UpDate(ClientVm clientVm)
        {
            try
            {
                ClientVm dbrecord = _applicationContext.Clients.Include(a => a.ClientOrders).FirstOrDefault(x => x.Id == clientVm.Id);
                if (dbrecord == null)
                {
                    return false;
                }
                //dbrecord.ClientID = clientVm.ClientID;
                dbrecord.Username = clientVm.Username;
                dbrecord.PassportNumber = clientVm.PassportNumber;
                dbrecord.Balance = clientVm.Balance;
                _applicationContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ClientVm GetById(int id)
        {
            ClientVm dbrecord = _applicationContext.Clients.Include(a => a.ClientOrders).FirstOrDefault(x => x.Id == id);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
        public List<ClientVm> GetAll()
        {
            List<ClientVm> dbrecord = _applicationContext.Clients.Include(a=>a.ClientOrders).ToList();
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
    }
}
