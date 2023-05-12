using Lab_2.Models;
using System.Collections.Generic;

namespace Lab__2_.Services
{
    public interface IClientService
    {
        void Create(ClientVm clientVm);
        bool Delete(int id);
        List<ClientVm> GetAll();
        ClientVm GetById(int id);
        bool UpDate(ClientVm clientVm);
    }
}
