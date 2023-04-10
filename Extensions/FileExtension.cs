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
    }
}
