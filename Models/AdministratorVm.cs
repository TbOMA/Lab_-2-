namespace Lab_2.Models
{
    public class AdministratorVm
    {
        public int AdministratorID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AdministratorVm() { }
        public AdministratorVm(int administratorID, string userName, string password)
        {
            AdministratorID = administratorID;
            UserName = userName;
            Password = password;
        }   
    }
}
