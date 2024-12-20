using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Users;

namespace Lab5.Tests.Moq;

public class MockUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public User? FindUserById(long id)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }

        return null;
    }

    public void InsertUser(long id, long password)
    {
        _users.Add(new User(id, password, 0));
    }

    public void AddMoney(long id, decimal amount)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                user.Balance += amount;
            }
        }
    }

    public void RemoveMoney(long id, decimal amount)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                user.Balance -= amount;
            }
        }
    }
}