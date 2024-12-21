using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;

namespace Lab5.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public UserService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public LoginResult Login(long id, long password)
    {
        User? user = _repository.FindUserById(id);

        if (user is null)
        {
            _currentUserManager.User = null;
            return new LoginResult.NotFound();
        }

        if (user.Password != password)
        {
            _currentUserManager.User = null;
            return new LoginResult.WrongPassword();
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }

    public LoginResult LoginAdmin(string name, string password)
    {
        if (name == "admin")
        {
            if (password == "123123")
            {
                _currentUserManager.User = new User(UserRole.Admin, 0, 123123, 1000);
                return new LoginResult.Success();
            }

            return new LoginResult.WrongPassword();
        }

        return new LoginResult.NotFound();
    }

    public void Logout()
    {
        _currentUserManager.User = null;
    }
}