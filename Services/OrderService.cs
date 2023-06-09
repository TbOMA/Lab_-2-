﻿using Lab__2_.Database;
using Lab_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab__2_.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _applicationContext;
        public OrderService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Create(OrderVm orderVm)
        {
            _applicationContext.Order.Add(orderVm);
            _applicationContext.SaveChanges();
        }
        public bool Delete(int id)
        {
            OrderVm dbrecord = _applicationContext.Order.Include(a => a.Car).Include(c => c.Client).FirstOrDefault(x => x.RentalFormID == id);
            if (dbrecord == null)
            {
                return false;
            }
            _applicationContext.Order.Remove(dbrecord);
            _applicationContext.SaveChanges();
            return true;
        }
        public bool UpDate(OrderVm orderVm)
        {
            try
            {
                OrderVm dbrecord = _applicationContext.Order.Include(a => a.Car).Include(c => c.Client).FirstOrDefault(x => x.RentalFormID == orderVm.RentalFormID);
                if (dbrecord == null)
                {
                    return false;
                }
                dbrecord.IsApproved = orderVm.IsApproved;
                dbrecord.TotalAmount = orderVm.TotalAmount;
                dbrecord.IsPaid = orderVm.IsPaid;
                dbrecord.IsConsidered = orderVm.IsConsidered;
                dbrecord.RejectionReason = orderVm.RejectionReason;
                dbrecord.RentalTime = orderVm.RentalTime;
                dbrecord.Client.Username = orderVm.Client.Username;
                dbrecord.Client.PassportNumber = orderVm.Client.PassportNumber;
                _applicationContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public OrderVm GetById(int id)
        {
            OrderVm dbrecord = _applicationContext.Order.Include(a => a.Car).Include(c => c.Client).FirstOrDefault(x => x.RentalFormID == id);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
        public List<OrderVm> GetAll()
        {
            List<OrderVm> dbrecord = _applicationContext.Order.Include(a=>a.Car).Include(c=>c.Client).ToList();
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
    }
}
