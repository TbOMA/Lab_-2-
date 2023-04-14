using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab_2.Models;
namespace Lab__2_.Extensions
{
    public static class FileExtension
    {
        //Task 2.11
        public static void SaveToFile(List<RentalFormVm> clients, string fileName = @"C:\Users\Артем\source\repos\2 sem\Lab_(2)\orders.json")
        {
            // Використовуємо формат JSON для серіалізації об'єктів
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(clients, options);
            File.WriteAllText(fileName, jsonString);
        }
        public static List<RentalFormVm> LoadFromFile(string fileName = @"C:\Users\Артем\source\repos\2 sem\Lab_(2)\orders.json")
        {
            if (!File.Exists(fileName))
            {
                // Якщо файл не існує, повертаємо порожній список
                return new List<RentalFormVm>();
            }
            string jsonString = File.ReadAllText(fileName);
            // Десеріалізуємо рядок у список об'єктів
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<RentalFormVm> orders = JsonSerializer.Deserialize<List<RentalFormVm>>(jsonString, options);
            return orders;
        }
        public static List<RentalCarVm> GetCarFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                // Якщо файл не існує, повертаємо порожній список
                return new List<RentalCarVm>();
            }
            string jsonString = File.ReadAllText(filename);
            // Десеріалізуємо рядок у список об'єктів
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<RentalCarVm> cars = JsonSerializer.Deserialize<List<RentalCarVm>>(jsonString, options);
            return cars;
        }
        public static void WriteCarToFile(List<RentalCarVm> cars,string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(cars, options);
            File.WriteAllText(filename, jsonString);
        }
        public static void AddInBlacklistFile(List<ClientVm> clientVms, string filename = "blacklist.json")
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(clientVms, options);
            File.WriteAllText(filename, jsonString);
        }
        public static List<ClientVm> GetBlacklistFile(string filename = "blacklist.json")
        {
            if (!File.Exists(filename))
            {
                // Якщо файл не існує, повертаємо порожній список
                return new List<ClientVm>();
            }
            string jsonString = File.ReadAllText(filename);
            // Десеріалізуємо рядок у список об'єктів
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<ClientVm> clients = JsonSerializer.Deserialize<List<ClientVm>>(jsonString, options);
            return clients;
        }
    }
}
