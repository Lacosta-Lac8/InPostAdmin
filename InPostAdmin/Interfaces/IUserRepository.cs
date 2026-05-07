namespace InPostAdmin.Interfaces;

public interface IUserRepository
{
    List<User> GetAllUsers();
    User GetUserByEmail(string email);
    void AddUser(User user);
}