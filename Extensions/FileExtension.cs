using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab_2.Models;

namespace Lab__2_.Extensions
{
    public static class FileExtension
    {
        public const string ORDERS_FILE_PATH = @"C:\\Users\\Артем\\source\\repos\\2 sem\\Lab_(2)\\orders.json";
        public const string RENTALCAR_FILE_PATH = @"C:\Users\Артем\source\repos\2 sem\Lab_(2)\bin\Debug\net7.0-windows\carlist.json";
        public const string BLACKLIST_FILE_PATH = @"C:\Users\Артем\source\repos\2 sem\Lab_(2)\bin\Debug\net7.0-windows\blacklist.json";
        //Task 2.11
        public static void SaveToFile<T>(List<T> obj)
        {
            string fileName = "";
            if (typeof(T) == typeof(ClientVm))
            {
                fileName = BLACKLIST_FILE_PATH;
            }
            else if (typeof(T) == typeof(RentalCarVm))
            {
                fileName = RENTALCAR_FILE_PATH;
            }
            else if (typeof(T) == typeof(RentalFormVm))
            {
                fileName = ORDERS_FILE_PATH;
            }
            // Використовуємо формат JSON для серіалізації об'єктів
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(fileName, jsonString);
        }
        public static List<ClientVm> GetBlacklistFile(string filename = "blacklist.json")
        {
            if (!File.Exists(filename))
            {
                // Якщо файл не існує, повертаємо порожній список
                return new List<ClientVm>();
            }
            string jsonString = File.ReadAllText(filename);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<ClientVm> clients = JsonSerializer.Deserialize<List<ClientVm>>(jsonString, options);
            return clients;
        }
    }
}
