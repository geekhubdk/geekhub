namespace Geekhub.Modules.Users.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ValidationCode { get; set; }
        public int InvalidLoginAttempts { get; set; }
    }
}