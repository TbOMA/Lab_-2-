using Lab__2_.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lab_2.Models
{
    public class AdministratorVm :IAdministrator
    {
        public int AdministratorID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        ///3.4
        private Dictionary<string, string> _users = new Dictionary<string, string>();
        private Dictionary<string, int> _userRoles = new Dictionary<string, int>();
        // Індексатор для доступу до ролі користувача за іменем
        public int this[string username]
        {
            get
            {
                int role;
                if (_userRoles.TryGetValue(username, out role))
                    return role;
                else
                    return -1; // Повернення значення за замовчуванням
            }
            set
            {
                _userRoles[username] = value;
            }
        }
        // Індексатор для доступу до паролю користувача за іменем
        public string this[int index]
        {
            get
            {
                return _users.ElementAt(index).Value;
            }
            set
            {
                _users[_users.ElementAt(index).Key] = value;
            }
        }
        ///
    }
}
