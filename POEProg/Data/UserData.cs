using POEProg.Models;

namespace POEProg.Data
{
    public static class UserData
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        public static List<User> GetAllUsers() => _users;

        public static User? GetUserByEmail(string email)
            => _users.FirstOrDefault(u => u.Email == email);

        public static void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public static void UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.Name = user.Name;
                existing.Surname = user.Surname;
                existing.Email = user.Email;
                existing.HourlyRate = user.HourlyRate;
                existing.Role = user.Role;
                existing.Password = user.Password;
            }
        }
    }
}
