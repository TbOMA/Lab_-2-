using Lab_2.Models;
using System.Collections.Generic;

namespace Lab__2_.Services
{
    public interface IOrderService
    {
        void Create(OrderVm orderVm);
        bool Delete(int id);
        List<OrderVm> GetAll();
        OrderVm GetById(int id);
        bool UpDate(OrderVm orderVm);
    }
}
