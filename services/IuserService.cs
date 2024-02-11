using ju.entity;

namespace ju.services
{
    public interface IuserService
    {

        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int UserId);

        User ValidteUser(string email, string password);




    }
}
