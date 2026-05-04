using System.Text.Json;

namespace InPostAdmin.Repositories;

public class UserRepository
{
    private readonly string _filePath = "users.json";
    private static readonly object _fileLock = new();

    public UserRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public List<User> GetAllUsers()
    {
        lock (_fileLock)
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
    }

    public User GetUserByEmail(string email)
    {
        var users = GetAllUsers();
        return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public void AddUser(User user)
    {
        lock (_fileLock)
        {
            var users = GetAllUsers();

            if (users.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("User with this email already exists.");
            }
            
            users.Add(user);
            
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}