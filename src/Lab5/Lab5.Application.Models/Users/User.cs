namespace Lab5.Application.Models.Users;

public class User
{
    public User(UserRole role, long id, long password, decimal balance)
    {
        Role = role;
        Id = id;
        Password = password;
        Balance = balance;
    }

    public UserRole Role { get; }

    public long Id { get; }

    public long Password { get; }

    public decimal Balance { get; set; }
}