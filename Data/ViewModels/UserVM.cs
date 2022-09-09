using Movies_API.Data.Models;

namespace Movies_API.Data.ViewModels
{
    // Used as return type to not return users private data such as password hash or password salt
    public class UserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public static explicit operator UserVM(User u)
        {
            return new UserVM()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            };
        }
    }
}
