namespace Geekhub.App.Modules.Users.Commands
{
    public class CreateUserCommand
    {
        public CreateUserCommand(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; private set; }
        public string Name { get; private set; }
    }
}