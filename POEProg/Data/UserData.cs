using System.Collections.Generic;
using System.Linq;

namespace POEProg.Models
{
    public static class UserData
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "HR", LastName = "Admin", Email = "hr@university.edu", HourlyRate = 0, Role = Role.HR, Password = "password" },
            new User { Id = 2, FirstName = "John", LastName = "Doe", Email = "john@university.edu", HourlyRate = 50, Role = Role.Lecturer, Password = "password" }
        };

        public static List<User> GetAllUsers() => _users;

        public static User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public static void AddUser(User user)
        {
            user.Id = _users.Max(u => u.Id) + 1;
            _users.Add(user);
        }

        public static void UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.Email = user.Email;
                existing.HourlyRate = user.HourlyRate;
                existing.Role = user.Role;
                existing.Password = user.Password;
            }
        }
    }
}
