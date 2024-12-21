using Itmo.Dev.Platform.Postgres.Connection;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Users;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindUserById(long id)
    {
        const string sql = """
                           select *
                           from Accounts
                           where AccountId = @id;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
        {
            return null;
        }

        return new User(
            UserRole.User,
            id: reader.GetInt64(0),
            password: reader.GetInt64(1),
            balance: reader.GetInt64(2));
    }

    public void InsertUser(long id, long password)
    {
        const string sql = """
                           insert into Accounts
                           values (@id, @password, 0)
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@password", password);

        command.ExecuteNonQuery();
    }

    public void AddMoney(long id, decimal amount)
    {
        User? user = FindUserById(id);

        if (user is null)
        {
            return;
        }

        const string sql = """
                           update Accounts
                           set AccountBalance = AccountBalance + @amount
                           where AccountId = @id
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }

    public void RemoveMoney(long id, decimal amount)
    {
        User? user = FindUserById(id);

        if (user is null)
        {
            return;
        }

        const string sql = """
                           UPDATE Accounts
                           SET AccountBalance = AccountBalance - @amount
                           WHERE AccountId = @id
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}